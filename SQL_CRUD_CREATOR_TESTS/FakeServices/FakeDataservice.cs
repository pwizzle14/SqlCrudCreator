using SqlCrudCreatorCore.DAL;
using NSubstitute;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SQL_CRUD_CREATOR_TESTS.Services
{
    internal class FakeDataService : IDatabaseService
    {
        public DataTable GetTableInfo(string tableName)
        {
            //use the Datatables properties class to create data for

            var col1 = new DataTableProperties() //ID
            {
                AllowDBNull = false,
                IsIdentity = true,
                ColumnName = "ID",
                DataType = "System.Int32, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
                IsAutoIncrement = true,
                ColumnOrdinal = 0,
                DataTypeName = "int"
            };

            var col2 = new DataTableProperties() //Name
            {
                AllowDBNull = false,
                IsIdentity = false,
                ColumnName = "Name",
                DataType = "System.String, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
                IsAutoIncrement = false,
                ColumnOrdinal = 1,
                DataTypeName = "nvarchar"
            };

            var lstData = new List<DataTableProperties>();
            lstData.Add(col1);
            lstData.Add(col2);

            var dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(lstData));

            return dt;  
        }
    }
}
