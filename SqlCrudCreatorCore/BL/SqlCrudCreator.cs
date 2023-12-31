﻿using SqlCrudCreatorCore.CRUD_Templates.SQL;
using SqlCrudCreatorCore.DAL;
using SqlCrudCreatorCore.Utilites;
using System.Text;

namespace SqlCrudCreatorCore.BL
{
    public class SqlCrudCreator : iSqlCrudCreator
    {

        private IDatabaseService? _databaseService = null;

        private string _tableName = "";
        private string _objectName = "";
        private string _className = "";
        private List<DataTableProperties>? _colData = null;
        private SqlCrudCreatorResults _results = new SqlCrudCreatorResults();


        public SqlCrudCreatorResults CreateAllClassObjAndSQL(IDatabaseService databaseService, string tableName, string objectName, string className)
        {
            try
            {
              
                _databaseService = databaseService;
                _tableName = tableName;
                _objectName = objectName;
                _className = className;
                

                _colData = DBTableHelper.ReadPropertiesFromTable(_tableName, _databaseService);


                //create class objects
                _results.ClassObjects = CreateClassObjs();

                //CreateSQL Scripts
                _results.SqlQuries = CreateSqlScripts();

                return _results;

            }

            catch(Exception ex)
            {
                throw new SqlCrudCreatorException($"Sql Crud Creator fatal error!", ex);
            }
        }

        private string CreateClassObjs()
        {
            ClassGenerator gen = new ClassGenerator(_colData, _tableName, _className, _objectName);

            return gen.CreateClassFromTemplate();
        }

       

        private string CreateSqlScripts()
        {
            StringBuilder sb = new StringBuilder();
          
            var lstOfTemplates = new List<iTemplate>
            {
                new Select_Template(_colData, _tableName),
                new Insert_Template(_colData, _tableName),
                new Delete_Template(_colData, _tableName),
                new Update_Template(_colData, _tableName)
            };


            foreach (var template in lstOfTemplates)
            {
                sb.Append(template.CreateSproc());
            }

            return sb.ToString();
        }
    }
}
