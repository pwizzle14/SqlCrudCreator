using SqlCrudCreatorCore.Utilites;

namespace SqlCrudCreatorCore.Services
{
    internal class FileWriter : iFileWriter
    {
        private string _filePath = "";

        public void WriteToText(string text, string tableName, string directory, bool buildClass = false)
        {
            try
            {
               _filePath = string.Empty;

                if (buildClass)
                {
                    _filePath = $@"{directory}\{tableName}.cs";
                }
                else
                {
                    _filePath = $@"{directory}\{tableName}_SQLScripts.sql";
                }


                //check for directory
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }

                using (StreamWriter sw = File.CreateText(_filePath))
                {
                    sw.WriteLine(text);
                }
            }

            catch (Exception ex)
            {
                throw new SqlCrudCreatorExecption($"Error when creating file {_filePath}", ex);
            }
        }
    }
}
