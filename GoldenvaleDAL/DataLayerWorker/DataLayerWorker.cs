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
        public void Create<iDataLayerObj>(iDataLayerObj obj)
        {
            //get parmeters 
            PreExecute<iDataLayerObj>(obj, SQL_FUNCTION_TYPE.CREATE);

            return ;
        }

        public void Delete<iDataLayerObj>(iDataLayerObj obj)
        {
            PreExecute(obj, SQL_FUNCTION_TYPE.DELETE);
            return;
        }

        /// <summary>
        /// Fetches Records by ID
        /// </summary>
        /// <param name="obj">The ID of this field needs to be set</param>
        /// <returns></returns>
        public List<iDataLayerObj> Select<iDataLayerObj>(iDataLayerObj obj)
        {
            return PreExecute(obj,SQL_FUNCTION_TYPE.SELECT);
        }

        public void Update<iDataLayerObj>(iDataLayerObj obj)
        {
            PreExecute(obj, SQL_FUNCTION_TYPE.UPDATE);
            return;
        }
        #endregion

        #region Custom
        public List<iDataLayerObj> PreExecute<iDataLayerObj>(iDataLayerObj obj, SQL_FUNCTION_TYPE funcType)
        {
            var selectSproc = GetSprocName((GoldenvaleDAL.iDataLayerObj)obj, funcType);

            var parms = Utilities.ToDictionary(obj, funcType);

            return Execute<iDataLayerObj>(selectSproc, parms);
        }

        public List<iDataLayerObj> ExecuteSproc(string storedProcName, iDataLayerObj obj)
        {
            var parms = Utilities.ToDictionary(obj, SQL_FUNCTION_TYPE.SPROC);

            return Execute<iDataLayerObj>(storedProcName, parms);
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