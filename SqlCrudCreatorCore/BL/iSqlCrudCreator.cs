using SqlCrudCreatorCore.DAL;
using SqlCrudCreatorCore.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlCrudCreatorCore.BL
{
    public interface iSqlCrudCreator
    {
        void CreateAllClassObjAndSQL(IDatabaseService databaseService, iFileWriter fileWriter, string tableName, string objectName, string className, string outputDir);
    }
}
