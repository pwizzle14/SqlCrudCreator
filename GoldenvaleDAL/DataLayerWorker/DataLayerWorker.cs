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
        public iDataLayerObj Create<iDataLayerObj>(iDataLayerObj obj)
        {
            //get parmeters 
            var id = PreExecute(obj, SQL_FUNCTION_TYPE.CREATE).FirstOrDefault();

            return id;
             
        }

        public void Delete<iDataLayerObj>(iDataLayerObj obj)
        {
            PreExecute(obj, SQL_FUNCTION_TYPE.DELETE);
            return;
        }

        public void DeleteById(int id, iDataLayerObj obj)
        {
           PreExecuteById(id, obj, SQL_FUNCTION_TYPE.DELETE);
        }

        public List<iDataLayerObj> Select<iDataLayerObj>(iDataLayerObj obj)
        {
            return PreExecute(obj,SQL_FUNCTION_TYPE.SELECT);
        }

        public iDataLayerObj? SelectById<iDataLayerObj>(int id, iDataLayerObj objType)
        {
            return PreExecuteById<iDataLayerObj>(id, objType, SQL_FUNCTION_TYPE.SELECT).FirstOrDefault();
        }

        public void Update<iDataLayerObj>(iDataLayerObj obj)
        {
            PreExecute(obj, SQL_FUNCTION_TYPE.UPDATE);
            return;
        }
        #endregion

        #region Custom

        private List<iDataLayerObj> PreExecuteById<iDataLayerObj>(int id, iDataLayerObj objType, SQL_FUNCTION_TYPE funcType)
        {
            var obj = (GoldenvaleDAL.iDataLayerObj)objType;
            var primarykeyName = obj.GetPrimaryKey();
            var sprocName = GetSprocName(obj, funcType);
            
            var parm = GetPrimaryKeyAsParameter(primarykeyName, id);

            return Execute<iDataLayerObj>(sprocName, parm);
        }
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

        private Dictionary<string, object> GetPrimaryKeyAsParameter(string primaryKey, int id)
        {
            var parm = new Dictionary<string, object>();
            parm.Add($"@{primaryKey}", id);

            return parm;
        }

        


    }
}