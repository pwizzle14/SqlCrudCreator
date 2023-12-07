using SqlCrudCreatorCore.CRUD_Templates.SQL;
using SqlCrudCreatorCore.DAL;


namespace SqlCrudCreatorCore
{
    public class Insert_Template: TemplateBase, iTemplate
	{
		public Insert_Template(List<DataTableProperties> tableData, string tableName): base(tableData, tableName)
		{
			SprocName = GetSprocName(tableName);
		}

		public string CreateSproc()
        {
			var text = $"{ BeginningOfSprocText}{ LINE_BREAK}" +
				$"{Parameters}" +
				$"{LINE_BREAK}{SetNoCount}" +
				$"{LINE_BREAK}INSERT INTO {TableName} {LINE_BREAK}" +
				$"({ColumNames}){LINE_BREAK}" +
				$"VALUES({ParameterVariables})" +
				$"{LINE_BREAK}END{LINE_BREAK}{LINE_BREAK}";
			

			return text;
        }
		 public string GetSprocName(string tableName)
        {
			return $"{tableName}_Create";
        }
		

	}
}
