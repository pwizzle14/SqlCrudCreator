using log4net.Core;
using SqlCrudCreatorCore.DAL;
using SqlCrudCreatorCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlCrudCreatorCore.BL
{
    public interface iSqlCrudCreator
    {
        SqlCrudCreatorResults CreateAllClassObjAndSQL(IDatabaseService databaseService, iFileWriter fileWriter, string tableName, string objectName, string className, string outputDir);
    }
}
