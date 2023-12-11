using SqlCrudCreatorCore.DAL;
using static SqlCrudCreatorCore.DBTableHelper;

namespace SqlCrudCreatorCore
{
    internal class ClassGenerator
    {

        public static string[] StringDBTypes = new string[] { "nvarchar", "varchar" };
        public static string[] IntDBTypes = new string[] { "int" };
        public static string[] DecimalDBTypes = new string[] { "decimal" };
        public static string BoolDBType = "bit";
        public List<ClassGeneratorProperties> PropInfo;

        public string ClassName = string.Empty;
        public string TableName = string.Empty;
        public string ObjectName = string.Empty;
        public string PrimaryKey = string.Empty;
        
        private string Line_Break = "\r\n";
        private string TAB = "\t";
        private string DOUBLETAB = "\t\t";

        private string _namespace = "GoldenvaleDAL.ClassObjects";
        
        public ClassGenerator(List<DataTableProperties> tableData, string tableName, string className, string objectName)
        {

            PropInfo = ClassGeneratorProperties.ConvertProperites(tableData);
            TableName = tableName;
            ClassName = className;
            ObjectName = objectName;

            var pkData = tableData.Where(x => x.IsIdentity == true).FirstOrDefault();

            PrimaryKey = pkData.ColumnName;
        }


        public string GetPublicProperties()
        {
            string result = string.Empty;

            foreach (var prop in PropInfo)
            {
                string res = $"{DOUBLETAB}public {prop.PropertyType} {prop.PropertyName} " +
                            " { get; set; }\r\n";


                result += res;
            }


            result += $"{Line_Break}{Line_Break}";

            return result;
        }

        public string GetUsingStatements()
        {
            return 
                   $"namespace {_namespace}{Line_Break}" +
                   "{" +
                   $"{Line_Break}{TAB}public class {ClassName}: iDataLayerObj{Line_Break}" +
                   $"{TAB}" + "{" +
                   $"{Line_Break}{Line_Break}";


                   
        }

        public string GetSprocNames(SQL_FUNCTION_TYPE type)
        {
            var result = string.Empty;
            var nameOfFunction = Enum.GetName(type);

            result += $"{DOUBLETAB}public string SprocName{nameOfFunction}(){Line_Break}{DOUBLETAB}" +
                     "{" + $"{Line_Break}" +
                     $"{DOUBLETAB}{TAB} return \"{TableName}_{nameOfFunction}\";" +
                     $"{Line_Break}{DOUBLETAB}" +
                     "}" +
                     $"{Line_Break}";



            return result;
        }

        public string GetCreateMethod()
        {
            return GetSprocNames(SQL_FUNCTION_TYPE.Create);
        }

        public string GetUpdateMethod()
        {
            return GetSprocNames(SQL_FUNCTION_TYPE.Update);
        }

        public string GetDeleteMethod()
        {
            return GetSprocNames(SQL_FUNCTION_TYPE.Delete);
        }

        public string GetFetchByIdMethod()
        {
            return GetSprocNames(SQL_FUNCTION_TYPE.Fetch);
        }

        public string GetCloseClass()
        {
            return $"{Line_Break}{TAB}" +
                    "}" +
                    $"{Line_Break}" +
                    "}";
        }

        public class ClassGeneratorProperties
        {
            public string PropertyName = "";
            public string PropertyType = "";
            public string ObjectValue = "";

            public string PrivatePropertyName
            {
                get
                {
                    return $"_{PropertyName}";
                }
            }

            public static List<ClassGeneratorProperties> ConvertProperites(List<DataTableProperties> tableData)
            {
                
                var result = new List<ClassGeneratorProperties>();

                foreach (var col in tableData)
                {
                    var tempRes = new ClassGeneratorProperties();

                    tempRes.PropertyName = col.ColumnName;

                    if (StringDBTypes.Contains(col.DataTypeName.ToLower()))
                    {
                        tempRes.PropertyType = "string";
                        tempRes.ObjectValue = "string.Empty";
                    }
                    else if (IntDBTypes.Contains(col.DataTypeName.ToLower()))
                    {
                        tempRes.PropertyType = "int";
                        tempRes.ObjectValue = "0";
                    }
                    else if (col.DataTypeName.ToLower() == BoolDBType)
                    {
                        tempRes.PropertyType = "bool";
                        tempRes.ObjectValue = "false";
                    }
                    else
                    {
                        tempRes.PropertyType = col.DataTypeName;
                        tempRes.ObjectValue = "";
                    }


                    result.Add(tempRes);
                }

                return result;
            }

            
        }
    }
}
