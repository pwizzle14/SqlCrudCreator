using System.Data;

using SqlCrudCreatorCore.DAL;
using Newtonsoft.Json;

namespace SqlCrudCreatorCore
{
    internal class DBTableHelper
    {
        private static IDatabaseService _databaseService = null;

        public static List<DataTableProperties> ReadPropertiesFromTable(string tableName, IDatabaseService databaseService)
        {
            _databaseService = databaseService;

            var dt = _databaseService.GetTableInfo(tableName);

            return ProcessTableSchema(dt); 
        }

        private static List<DataTableProperties> ProcessTableSchema(DataTable dt)
        {
            var jsonResult = JsonConvert.SerializeObject(dt);

            return JsonConvert.DeserializeObject<List<DataTableProperties>>(jsonResult);
        }

    }
}
