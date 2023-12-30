using SqlCrudCreatorCore.DAL;
using SqlCrudCreatorCore.Utilites;
using SqlCrudCreatorCore.Utilities;
using System.Runtime.CompilerServices;
using System.Text;
using static SqlCrudCreatorCore.DBTableHelper;

namespace SqlCrudCreatorCore
{
    internal class ClassGenerator
    {
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

        public string CreateClassFromTemplate()
        {
            try
            {
                System.IO.StreamReader file = new StreamReader(@"../../../ClassObjectTemplate.txt");

                string inputText = file.ReadToEnd();

                var replacers = new Dictionary<string, string>
            {
                { "{0}", _namespace },
                { "{1}", ClassName },
                { "{2}", GetPublicProperties() },
                { "{3}", GetPrimaryKeyFunction() },
                { "{4}", GetAllMethods() }
            };

                var parms = new TemplateFillerParms()
                {
                    InputText = inputText,
                    Replacers = replacers
                };

                var res = TemplateFiller.FillClassObjectTemplate(parms);

                return res;
            }

            catch (Exception ex)
            {
                throw new SqlCrudCreatorException("Error Creating Class Object", ex);
            }
            
        }

        private string GetPublicProperties()
        {
            string result = string.Empty;

            foreach (var prop in PropInfo)
            {
                string res = $"public {prop.PropertyType} {prop.PropertyName} " +
                            " { get; set; }" + $"{Line_Break}" +
                            $"{DOUBLETAB}";


                result += res;
            }


            result += $"{Line_Break}{Line_Break}";

            return result;
        }

        private string GetPrimaryKeyFunction()
        {
            string result = "";

            result += $"public string GetPrimaryKey()" +
                $"{Line_Break}{DOUBLETAB}" + "{" + $"{Line_Break}" + 
                
                $"{DOUBLETAB}{TAB}return \"{PrimaryKey}\";{Line_Break}{DOUBLETAB}" +
                "}" +
                $"{Line_Break}{Line_Break}";

            return result;

        }


        private string GetAllMethods()
        {
            var builder = new StringBuilder();

            builder.Append(GetCreateMethod());
            builder.Append(GetUpdateMethod());
            builder.Append(GetDeleteMethod());
            builder.Append(GetFetchByIdMethod());

            return builder.ToString();
        }

        private string GetCreateMethod()
        {
            return GetSprocNames(SQL_FUNCTION_TYPE.Create);
        }

        private string GetUpdateMethod()
        {
            return GetSprocNames(SQL_FUNCTION_TYPE.Update);
        }

        private string GetDeleteMethod()
        {
            return GetSprocNames(SQL_FUNCTION_TYPE.Delete);
        }

        private string GetFetchByIdMethod()
        {
            return GetSprocNames(SQL_FUNCTION_TYPE.Fetch);
        }
        private string GetSprocNames(SQL_FUNCTION_TYPE type)
        {
            var result = string.Empty;
            var nameOfFunction = Enum.GetName(type);

            result += $"public string SprocName{nameOfFunction}(){Line_Break}{DOUBLETAB}" +
                     "{" + $"{Line_Break}" +
                     $"{DOUBLETAB}{TAB} return \"{TableName}_{nameOfFunction}\";" +
                     $"{Line_Break}{DOUBLETAB}" +
                     "}" +
                     $"{Line_Break}{DOUBLETAB}";



            return result;
        }
            
        }
    }
