using SqlCrudCreatorCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_CRUD_CREATOR_TESTS.Services
{
    internal class FakeFileWriter : iFileWriter
    {
        public void WriteToText(string text, string tableName, string directory, bool buildClass = false)
        {
            Console.WriteLine(text);
        }
    }
}
