using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AgentSmith.SpellCheck.NetSpell
{
    /// <summary>
    /// Summary description for <see cref="OpenOfficeDictionaryImporter"/>.
    /// </summary>
    public class OpenOfficeDictionaryImporter
    {
        private string _tryChars = "";
        private string _prefix = "";
        private string _suffix = "";
        private string _replace = "";
        private Encoding _encoding = Encoding.UTF7;
        private readonly IDictionary<string, string> _words = new Dictionary<string, string>();

        public static void Import(string affixFile, string wordFile, string outFile)
        {
            OpenOfficeDictionaryImporter importer = new OpenOfficeDictionaryImporter();
            importer.loadAffix(affixFile);
            importer.loadWords(wordFile);
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                importer.saveDictionary(sw);
                importer._words.Clear();
                ms.Seek(0, SeekOrigin.Begin);
                new WordDictionary(new StreamReader(ms));
                File.WriteAllText(outFile, sw.ToString());
            }
        }

        private void loadAffix(string fileName)
        {
            _encoding = Encoding.UTF7;
            using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                string tempLine = sr.ReadLine();
                if (tempLine != null && tempLine.Length > 4)
                {
                    _encoding = Encoding.GetEncoding(tempLine.Substring(4));
                }
            }
            
            using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open), _encoding))
            {
                sr.ReadLine();
                
                while (sr.Peek() >= 0)
                {
                    string tempLine = sr.ReadLine().Trim();
                    if (tempLine.Length > 3)
                    {
                        switch (tempLine.Substring(0, 3))
                        {            
                            case "TRY":
                                _tryChars = tempLine.Substring(4);
                                break;
                            case "PFX":
                                _prefix += tempLine.Substring(4) + "\n";
                                break;
                            case "SFX":
                                _suffix += tempLine.Substring(4) + "\n";
                                break;
                            case "REP":
                                if (!char.IsNumber(tempLine.Substring(4)[0]))
                                {
                                    _replace += tempLine.Substring(4) + "\n";
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void loadWords(string fileName)
        {                        
            using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open), _encoding))
            {
                sr.ReadLine();
                // read line by line
                while (sr.Peek() >= 0)
                {
                    string tempLine = sr.ReadLine().Trim();
                    
                    if (!char.IsNumber(tempLine[0]))
                    {
                        string[] parts = tempLine.Split('/');
                        string word = parts[0];
                        string affixKeys = "";
                        if (parts.Length > 1)
                        {
                            affixKeys = parts[1];
                        }

                        // look for duplicate words
                        if (_words.ContainsKey(word))
                        {
                            // merge affix keys on duplicate words
                            string[] tempParts = _words[word].Split('/');
                            string oldKeys = "";
                            if (tempParts.Length > 1)
                            {
                                oldKeys = tempParts[1];
                            }

                            foreach (char key in oldKeys)
                            {
                                // if the new affix keys do not contain old key, add old key to new keys
                                if (affixKeys.IndexOf(key) == -1)
                                {
                                    affixKeys += key.ToString();
                                }
                            }
                            // only update if have keys
                            if (affixKeys.Length > 0)
                            {
                                _words[word] = string.Format("{0}/{1}", word, affixKeys);
                            }
                        }
                        else
                        {
                            _words.Add(word, tempLine);
                        }
                    }
                }
            }
        }

        private void saveDictionary(StreamWriter sw)
        {
            sw.NewLine = "\n";

            sw.WriteLine("[Try]");
            sw.WriteLine(_tryChars);
            sw.WriteLine();

            sw.WriteLine("[Replace]");
            sw.WriteLine(_replace);

            sw.WriteLine("[Prefix]");
            sw.WriteLine(_prefix);

            sw.WriteLine("[Suffix]");
            sw.WriteLine(_suffix);

            sw.WriteLine("[Words]");
            foreach (string tempWord in _words.Values)
            {
                sw.WriteLine(tempWord);
            }
        }
    }
}