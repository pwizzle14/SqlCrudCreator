using log4net.Core;
using SqlCrudCreatorCore.CRUD_Templates.SQL;
using SqlCrudCreatorCore.DAL;
using SqlCrudCreatorCore.Services;
using SqlCrudCreatorCore.Utilites;
using System.Collections;
using System.Text;

namespace SqlCrudCreatorCore.BL
{
    public class SqlCrudCreator : iSqlCrudCreator
    {

        private IDatabaseService _databaseService = null;
        private iFileWriter _fileWriter = null;

        private string _tableName = "";
        private string _objectName = "";
        private string _className = "";
        private string _outputDir = "";
        private List<DataTableProperties> _colData = null;
        private SqlCrudCreatorResults _results = new SqlCrudCreatorResults();


        public SqlCrudCreatorResults CreateAllClassObjAndSQL(IDatabaseService databaseService, iFileWriter fileWriter, string tableName, string objectName, string className, string outputDir)
        {
            try
            {
              
                _databaseService = databaseService;
                _tableName = tableName;
                _objectName = objectName;
                _className = className;
                _fileWriter = fileWriter;
                _outputDir = outputDir;

                _colData = DBTableHelper.ReadPropertiesFromTable(_tableName, _databaseService);


                //create class objects
                _results.ClassObjects = CreateClassObjs();

                //CreateSQL Scripts
                _results.SqlQuries = CreateSqlScripts();

                return _results;

            }

            catch(Exception ex)
            {
                throw new SqlCrudCreatorExecption($"Fatal error!", ex);
            }
        }

        private string CreateClassObjs()
        {
            ClassGenerator gen = new ClassGenerator(_colData, _tableName, _className, _objectName);

            var result = string.Empty;

            result += gen.GetUsingStatements();
            result += gen.GetPublicProperties();
            result += gen.GetPrimaryKeyFunction(gen.PrimaryKey);
            result += gen.GetCreateMethod();
            result += gen.GetUpdateMethod();
            result += gen.GetDeleteMethod();
            result += gen.GetFetchByIdMethod();
            result += gen.GetCloseClass();


            return result;
        }

        private string CreateSqlScripts()
        {
            StringBuilder sb = new StringBuilder();
          
            var lstOfTemplates = new List<iTemplate>();

            
            lstOfTemplates.Add(new Select_Template(_colData, _tableName));
            lstOfTemplates.Add(new Insert_Template(_colData, _tableName));
            lstOfTemplates.Add(new Delete_Template(_colData, _tableName));
            lstOfTemplates.Add(new Update_Template(_colData, _tableName));


            foreach (var template in lstOfTemplates)
            {
                iTemplate temp = template;

                sb.Append(temp.CreateSproc());
            }

            return sb.ToString();
        }
    }
}
