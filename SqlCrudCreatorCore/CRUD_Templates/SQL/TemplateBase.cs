using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Text;
using System.Linq;
using SqlCrudCreatorCore.DAL;

namespace SqlCrudCreatorCore
{
    public class TemplateBase
    {
        public const string LINE_BREAK = "\r\n";
        public const string TAB = "\t";
        public string SprocName = "";
        public string ColumNames = "";
        public string Parameters = "";
        public string TableName = "";
        public string PrimaryKey = "";
        public List<DataTableProperties> ColumData;
        public List<string> LstColumnNames = new List<string>();

        public TemplateBase(List<DataTableProperties> tableData, string tableName)
        {
            ColumData = tableData;

            ColumNames = CreateColumnNames(ColumData);
            
            TableName = tableName;
            Parameters = CreateParameters(ColumData, false);
            PrimaryKey = ColumData.FirstOrDefault(x => x.IsIdentity == true).ColumnName;
        }

        private string GetSprocName(string tableName)
        {
            return "DEFAULT_SPROC_NAME";
        }

        public string SetNoCount
        {
            get
            {
                return $"AS {LINE_BREAK}" +
                $"BEGIN {LINE_BREAK}" +
                $"SET NOCOUNT ON;{LINE_BREAK}";
            }
        }



        public string BeginningOfSprocText
        {
            get
            {
                return BeginningOfSproc();
            }
        }

        public string UpdateValues
        {
            get
            {
                return CreateUpDateValues();
            }
        }

        public string CreateColumnNames(List<DataTableProperties> colData,bool excludePk = false )
        {
            StringBuilder stb = new StringBuilder();
            var res = colData.ToList();
            if(excludePk)
            {
                res = colData.Where(x => x.IsIdentity == false).ToList();
            }

            foreach (var col in res)
            {
                stb.Append($"{TAB},{col.ColumnName}{LINE_BREAK}");

                LstColumnNames.Add($"@{col.ColumnName}");
            }

            //remove te first ","
            stb.Remove(1, 1);

            return stb.ToString();
        }

        public string CreateParameters(List<DataTableProperties> colData, bool primaryKeyOnly = false, bool excludePk = false)
        {
            var res = colData.ToList();

            if (primaryKeyOnly)
            {
                //Find the PK
                res = colData.Where(x => x.IsIdentity == true).ToList();
            }
            else if (excludePk)
            {
                res = colData.Where(x => x.IsIdentity == false).ToList();
            }

            return CreateParameters(res);
        }
        private string CreateParameters(List<DataTableProperties> colData)
        {

            StringBuilder stb = new StringBuilder();

            foreach (var col in colData)
            {
                var colWidth = "";

                if(ClassGeneratorProperties.StringDBTypes.Contains(col.DataTypeName))
                    colWidth = $"({col.ColumnSize})";

                stb.Append($",@{col.ColumnName} {col.DataTypeName}{colWidth}{LINE_BREAK}");
            }

            //remove the first ","
            stb.Remove(0, 1);

            return stb.ToString();
        }

        private string BeginningOfSproc()
        {
            string text =

                $"GO{LINE_BREAK}" +
                $"DROP PROCEDURE IF EXISTS dbo.{SprocName}; {LINE_BREAK}GO{LINE_BREAK}" +
                $"CREATE PROCEDURE[dbo].[{SprocName}] {LINE_BREAK}";

            return text;
        }

        internal string CreateLisOfColumnsForInsert(bool excludePk = false)
        {
            StringBuilder stb = new StringBuilder();
            var colNames = LstColumnNames;

            if(excludePk)
            {
                colNames.Remove(colNames.FirstOrDefault());
            }

            foreach (var col in colNames)
            {
                stb.Append($",{col}");
            }

            //remove the first ","
            stb.Remove(0, 1);

            return stb.ToString();
        }

        private string CreateUpDateValues()
        {
            StringBuilder stb = new StringBuilder();

            foreach (var col in ColumData)
            {
                if (!col.IsIdentity)
                {
                    stb.Append($",{col.ColumnName} = @{col.ColumnName}{LINE_BREAK}");
                }
            }

            //remove the first ","
            stb.Remove(0, 1);

            return stb.ToString();
        }
    }
}
