using System.Data;
using System.Data.SqlClient;
using Dapper;
using static GoldenvaleDAL.Utilities;

namespace GoldenvaleDAL.DataLayerWorker
{
    public class DataLayerWorker: iDataLayerWorker
    {
        private string _connectionString = "server=Doms-Laptop;initial catalog=Goldenvale;trusted_connection=true";

        #region CRUD
        public bool Create<iDataLayerObj>(iDataLayerObj obj)
        {
            //get parmeters 
            var selectSproc = GetSprocName((GoldenvaleDAL.iDataLayerObj)obj, SQL_FUNCTION_TYPE.CREATE);

            var parms = Utilities.ToDictionary(obj, SQL_FUNCTION_TYPE.CREATE);

            Execute<iDataLayerObj>(selectSproc, parms);

            return true;
        }

        public bool Delete<iDataLayerObj>(iDataLayerObj obj)
        {
            var parms = Utilities.ToDictionary(obj, SQL_FUNCTION_TYPE.SELECT);

            //get parmeters 
            var selectSproc = GetSprocName((GoldenvaleDAL.iDataLayerObj)obj, SQL_FUNCTION_TYPE.DELETE);

            Execute<iDataLayerObj>(selectSproc, parms);

            return true;
        }

        /// <summary>
        /// Fetches Records by ID
        /// </summary>
        /// <param name="obj">The ID of this field needs to be set</param>
        /// <returns></returns>
        public List<iDataLayerObj> Select<iDataLayerObj>(iDataLayerObj obj)
        {
            var parms = Utilities.ToDictionary(obj, SQL_FUNCTION_TYPE.SELECT);

            //get parmeters 
            var selectSproc = GetSprocName((GoldenvaleDAL.iDataLayerObj)obj, SQL_FUNCTION_TYPE.SELECT);

            var results = Execute<iDataLayerObj>(selectSproc, parms);

            return results;
        }

        public bool Update<iDataLayerObj>(iDataLayerObj obj)
        {
            //get parmeters 
            var selectSproc = GetSprocName((GoldenvaleDAL.iDataLayerObj)obj, SQL_FUNCTION_TYPE.UPDATE);

            var parms = Utilities.ToDictionary(obj, SQL_FUNCTION_TYPE.UPDATE);

            Execute<iDataLayerObj>(selectSproc, parms);

            return true;
        }
        #endregion

        private List<iDataLayerObj> Execute<iDataLayerObj>(string sprocName, Dictionary<string, object> parms)
        {
            
            SqlConnection sqlCon = new SqlConnection(_connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(sprocName, sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            
            if(parms != null)
            {
                foreach(KeyValuePair<string,object> param in parms)
                {
                      cmd.Parameters.Add(new SqlParameter($"@{param.Key}", param.Value));
                }
            }

            var results = sqlCon.Query<iDataLayerObj>(sprocName,parms,commandType: CommandType.StoredProcedure).ToList();

            return results;
        }

        private string GetSprocName(iDataLayerObj data, SQL_FUNCTION_TYPE type)
        {
            switch (type)
            {
                case SQL_FUNCTION_TYPE.CREATE:
                    {
                        return data.SprocNameCreate();
                    }
                case SQL_FUNCTION_TYPE.UPDATE:
                    {
                        return data.SprocNameUpdate();
                    }
                case SQL_FUNCTION_TYPE.DELETE:
                    {
                        return data.SprocNameDelete();
                    }
                case SQL_FUNCTION_TYPE.SELECT:
                    {
                        return data.SprocNameFetch();
                    }
                default:
                    throw new Exception("Cannot find requested Stored Procedure");
            }
        }
    }
}