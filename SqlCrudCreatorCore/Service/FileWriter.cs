using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCrudCreatorCore.Service
{
    internal class FileWriter : iFileWriter
    {
        public void WriteToText(string text, string tableName, string directory, bool buildClass = false)
        {
            string filePath = string.Empty;

            if (buildClass)
            {
                filePath = $@"{directory}\{tableName}.cs";
            }
            else
            {
                filePath = $@"{directory}\{tableName}_SQLScripts.sql";
            }


            //check for directory
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine(text);
            }
        }
    }
}
