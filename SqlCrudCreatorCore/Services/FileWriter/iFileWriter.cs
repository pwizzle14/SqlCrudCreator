﻿
namespace SqlCrudCreatorCore.Services
{
     public interface iFileWriter
    {
          public void WriteToText(string text, string tableName, string directory, bool buildClass = false);
    }
}
