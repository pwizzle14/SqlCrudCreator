using System.Data;
using System.Data.SqlClient;
using Dapper;
using static GoldenvaleDAL.Utilities;

namespace GoldenvaleDAL.DataLayerWorker
{
    public class DataLayerWorker: IDataLayerWorker
    {
        private string _connectionString = "server=Doms-Laptop;initial catalog=Goldenvale;trusted_connection=true";

        #region CRUD
        public async Task<IDataLayerObj> Create<IDataLayerObj>(IDataLayerObj obj)
        {
            //get parmeters 
            var id = await PreExecute<IDataLayerObj>(obj, SQL_FUNCTION_TYPE.CREATE);

            return id.FirstOrDefault();
             
        }

        public async Task DeleteById(int id, IDataLayerObj obj)
        {
            await PreExecuteById<IDataLayerObj>(id, obj, SQL_FUNCTION_TYPE.DELETE);
            return; 
        }

        public async Task<IDataLayerObj?> SelectById<IDataLayerObj>(int id, IDataLayerObj objType)
        {
            var results =  await PreExecuteById<IDataLayerObj>(id, objType, SQL_FUNCTION_TYPE.SELECT);

            return results.FirstOrDefault();
        }

        public async Task Update<IDataLayerObj>(IDataLayerObj obj)
        {
            await PreExecute<IDataLayerObj>(obj, SQL_FUNCTION_TYPE.UPDATE);
            return;
        }
        #endregion

        #region CustomSprocs

        public async Task<List<IDataLayerObj>>ExecuteSproc<IDataLayerObj>(string storedProcName, Dictionary<string, object> parms)
        {
            return await Execute<IDataLayerObj>(storedProcName, parms);
        }

        #endregion

        #region PreExecute

        private async Task<List<IDataLayerObj>> PreExecuteById<IDataLayerObj>(int id, IDataLayerObj objType, SQL_FUNCTION_TYPE funcType)
        {
            var obj = (GoldenvaleDAL.IDataLayerObj)objType;
            var primarykeyName = obj.GetPrimaryKey();
            var sprocName = GetSprocName(obj, funcType);
            
            var parm = GetPrimaryKeyAsParameter(primarykeyName, id);

            return await Execute<IDataLayerObj>(sprocName, parm);
        }
        private async Task<List<IDataLayerObj>> PreExecute<IDataLayerObj>(IDataLayerObj obj, SQL_FUNCTION_TYPE funcType)
        {
            var selectSproc = GetSprocName((GoldenvaleDAL.IDataLayerObj)obj, funcType);

            var parms = Utilities.ToDictionary(obj, funcType);

            return await Execute<IDataLayerObj>(selectSproc, parms);
        }

        #endregion

        #region Execute

        private async Task<List<IDataLayerObj>> Execute<IDataLayerObj>(string sprocName, Dictionary<string, object> parms)
        {
            
            SqlConnection sqlCon = new SqlConnection(_connectionString);
           
            var results = sqlCon.Query<IDataLayerObj>(sprocName,parms,commandType: CommandType.StoredProcedure).ToList();

            return results;
        }
        #endregion

        #region Utilities
        private string GetSprocName(IDataLayerObj data, SQL_FUNCTION_TYPE type)
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

        #endregion
    }
}