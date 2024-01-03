using SqlCrudCreatorCore.DAL;

public class ClassGeneratorProperties
{
    public static readonly string[] StringDBTypes = ["nvarchar", "varchar"];
    public static readonly string[] IntDBTypes = ["int"];
    public static readonly string[] DecimalDBTypes = ["decimal"];
    public static readonly string BoolDBType = "bit";


    public string PropertyName = "";
    public string PropertyType = "";
    public string ObjectValue = "";
    public string NullableChar = "";

    public string PrivatePropertyName
    {
        get
        {
            return $"_{PropertyName.ToLower()}";
        }
    }

    public static List<ClassGeneratorProperties> ConvertProperites(List<DataTableProperties> tableData)
    {
        var result = new List<ClassGeneratorProperties>();

        foreach (var col in tableData)
        {
            var tempRes = new ClassGeneratorProperties();

            tempRes.PropertyName = col.ColumnName;

            if(col.AllowDBNull)
                tempRes.NullableChar = "?";
            

            switch (col.DataTypeName.ToLower())
            {
                case var strType when StringDBTypes.Contains(strType):
                    tempRes.PropertyType = "string";
                    tempRes.ObjectValue = "string.Empty";
                    break;

                case var intType when IntDBTypes.Contains(intType):
                    tempRes.PropertyType = "int";
                    tempRes.ObjectValue = "0";
                    break;

                case var decimalType when IntDBTypes.Contains(decimalType):
                    tempRes.PropertyType = "double";
                    tempRes.ObjectValue = "0";
                    break;

                case var boolType when boolType == BoolDBType:
                    tempRes.PropertyType = "bool";
                    tempRes.ObjectValue = "false";
                    break;

                case var datetimeType when DateTimeDBType.Contains(datetimeType):
                    tempRes.PropertyType = "DateTime";
                    tempRes.ObjectValue = null;
                    break;

                default:
                    tempRes.PropertyType = col.DataTypeName;
                    tempRes.ObjectValue = null;
                    break;
            }


            tempRes.PropertyType += tempRes.NullableChar;

            result.Add(tempRes);
        }

        return result;
    }
}