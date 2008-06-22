using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AgentSmith.Options
{
    internal class DicUtil
    {
        public static string GetUserDicDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                "Agent Smith\\dic");
        }

        private static string getDefaultDicDirectory()
        {
            string assemblyDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            return Path.Combine(assemblyDir, "dic");
        }    

        public static List<string> LoadDictionaries()
        {
            List<string> list = new List<string>();
            string dicDirectory = GetUserDicDirectory();
            if (Directory.Exists(dicDirectory))
            {
                foreach (string file in Directory.GetFiles(dicDirectory))
                {
                    list.Add(Path.GetFileNameWithoutExtension(file));
                }
            }

            dicDirectory = getDefaultDicDirectory();
            if (Directory.Exists(dicDirectory))
            {
                foreach (string file in Directory.GetFiles(dicDirectory))
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    if (!list.Contains(name))
                    {
                        list.Add(name);
                    }
                }
            }
            return list;
        }
    }
}
