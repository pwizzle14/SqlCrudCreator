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
        SqlCrudCreatorResults CreateAllClassObjAndSQL(IDatabaseService databaseService, string tableName, string objectName, string className);
    }
}
