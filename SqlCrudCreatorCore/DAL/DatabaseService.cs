﻿using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace SqlCrudCreatorCore.DAL
{
    internal class DatabaseService : IDatabaseService
    {
        private string _connectionString = "";
        

        private void GetConfigSettings()
        {
            _connectionString = "";
        }
        public ReadOnlyCollection<DbColumn> ReadPropertiesFromTable(string tableName)
        {
            GetConfigSettings();

            string strSQL = $"SELECT TOP 1 * FROM {tableName}";

            // Assumes connectionString is a valid connection string.  
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                SqlCommand command = new SqlCommand(strSQL, connection);

                connection.Open();
                var reader = command.ExecuteReader();
                var dt = reader.GetColumnSchema();

                return dt;
            }
        }
    }
}
