﻿using SqlCrudCreatorCore.CRUD_Templates.SQL;
using SqlCrudCreatorCore.DAL;
using SqlCrudCreatorCore.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
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
        private ReadOnlyCollection<DbColumn> _colData = null;

        public SqlCrudCreator(IDatabaseService databaseService, iFileWriter fileWriter, string tableName, string objectName, string className, string outputDir)
        {
            _databaseService = databaseService;
            _tableName = tableName;
            _objectName = objectName;
            _className = className;
            _fileWriter = fileWriter;
            _outputDir = outputDir;

            _colData = DBTableHelper.ReadPropertiesFromTable(_tableName, _databaseService);

        }

        public void CreateAllClassObjAndSQL()
        {
           
            //create class objects
            CreateClassObjs();

            //CreateSQL Scripts
            CreateSqlScripts();
        }

        private void CreateClassObjs()
        {
            ClassGenerator gen = new ClassGenerator(_colData, _tableName, _className, _objectName);

            var result = string.Empty;

            result += gen.GetUsingStatements();
            result += gen.GetPrivateProperities();
            result += gen.GetPublicProperties();
            result += gen.GetCreateMethod();
            result += gen.GetUpdateMethod();
            result += gen.GetDeleteMethod();
            result += gen.GetFetchByIdMethod();
            result += gen.GetCloseClass();

            _fileWriter.WriteToText(result, _tableName, _outputDir, true);

        }

        private void CreateSqlScripts()
        {
            StringBuilder sb = new StringBuilder();
          
            var lstOfTemplates = new ArrayList();

            lstOfTemplates.Add(new Fetch_Template(_colData, _tableName));
            lstOfTemplates.Add(new Insert_Template(_colData, _tableName));
            lstOfTemplates.Add(new Delete_Template(_colData, _tableName));
            lstOfTemplates.Add(new Update_Template(_colData, _tableName));


            foreach (var template in lstOfTemplates)
            {
                iTemplate temp = (iTemplate)template;

                sb.Append(temp.CreateSproc());
            }

            _fileWriter.WriteToText(sb.ToString(),_tableName, _outputDir, false);
        }
    }
}