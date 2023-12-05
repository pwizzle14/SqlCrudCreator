﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Common;
using SqlCrudCreatorCore.DAL;

namespace SqlCrudCreatorCore
{
    internal class DBTableHelper
    {
        private static IDatabaseService _databaseService = null;  

        public static ReadOnlyCollection<DbColumn> ReadPropertiesFromTable(string tableName, IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _databaseService = databaseService;

            return _databaseService.ReadPropertiesFromTable(tableName);
        }
    }


}
