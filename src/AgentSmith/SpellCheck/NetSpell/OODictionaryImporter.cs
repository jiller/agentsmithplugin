using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AgentSmith.SpellCheck.NetSpell
{
    /// <summary>
    /// Summary description for <see cref="OODictionaryImporter"/>.
    /// </summary>
    public class OODictionaryImporter
    {
        private string tryChars = "";
        private string prefix = "";
        private string suffix = "";
        private string replace = "";
        Encoding encoding = Encoding.UTF7;

        public void LoadAffix(string fileName)
        {
            Encoding encoding = Encoding.UTF7;
            using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open), Encoding.UTF7))
            {
                // read line by line
                while (sr.Peek() >= 0)
                {
                    string tempLine = sr.ReadLine().Trim();
                    if (tempLine.Length > 3)
                    {
                        switch (tempLine.Substring(0, 3))
                        {
                            case "SET":
                                encoding = Encoding.GetEncoding(tempLine.Substring(4));
                                break;
                            case "TRY":
                                tryChars = new string(encoding.GetChars(Encoding.UTF7.GetBytes(tempLine.Substring(4))));
                                break;
                            case "PFX":
                                prefix += new string(encoding.GetChars(Encoding.UTF7.GetBytes(tempLine.Substring(4)))) + "\n";
                                break;
                            case "SFX":
                                suffix += new string(encoding.GetChars(Encoding.UTF7.GetBytes(tempLine.Substring(4)))) + "\n";
                                break;
                            case "REP":
                                if (!char.IsNumber(tempLine.Substring(4)[0]))
                                {
                                    replace += new string(encoding.GetChars(Encoding.UTF7.GetBytes(tempLine.Substring(4)))) + "\n";
                                }
                                break;
                        }
                    }
                }
            }
        }

        private IDictionary<string, string> words = new Dictionary<string, string>();

        public void LoadWords(string fileName)
        {            
            bool first = true;
            using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open), encoding))
            {
                // read line by line
                while (sr.Peek() >= 0)
                {
                    string tempLine = sr.ReadLine().Trim();
                    if (first)
                    {
                        //encoding = Encoding.GetEncoding(int.Parse(tempLine));
                    }
                    else
                    {
                        //tempLine = new string(encoding.GetChars(Encoding.UTF7.GetBytes(tempLine)));
                    }
                    first = false;
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
                        if (words.ContainsKey(word))
                        {
                            // merge affix keys on duplicate words
                            string[] tempParts = words[word].Split('/');
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
                                words[word] = string.Format("{0}/{1}", word, affixKeys);
                            }
                        }
                        else
                        {
                            words.Add(word, tempLine);
                        }
                    }
                }
            }
        }

        public void SaveDictionary(string fileName)
        {            
            // save dictionary file
            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.NewLine = "\n"; // unix line ends

                // copyright
                //sw.WriteLine("[Copyright]");
                //sw.WriteLine(this.txtCopyright.Text.Replace("\r\n", "\n")); // unix line ends
                // try
                sw.WriteLine("[Try]");
                sw.WriteLine(tryChars);
                sw.WriteLine();
                // replace
                sw.WriteLine("[Replace]");
                sw.WriteLine(replace); // unix line ends
                // prefix
                sw.WriteLine("[Prefix]");
                sw.WriteLine(prefix); // unix line ends
                // suffix
                sw.WriteLine("[Suffix]");
                sw.WriteLine(suffix); // unix line ends

                // words
                sw.WriteLine("[Words]");
                foreach (string tempWord in words.Keys)
                {
                    sw.WriteLine(tempWord);
                }
            }
        }
    }
}