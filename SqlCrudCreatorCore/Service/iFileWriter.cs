using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCrudCreatorCore.Service
{
     public interface iFileWriter
    {
          public void WriteToText(string text, string tableName, string directory, bool buildClass = false);
    }
}
