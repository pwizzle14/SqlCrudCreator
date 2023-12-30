using SqlCrudCreatorCore.CRUD_Templates.SQL;
using SqlCrudCreatorCore.DAL;


namespace SqlCrudCreatorCore
{
    public class Insert_Template: TemplateBase, iTemplate
	{
        private string _parameterVariables, _columnNames;

        public Insert_Template(List<DataTableProperties> tableData, string tableName): base(tableData, tableName)
		{
			SprocName = GetSprocName(tableName);

			//need to remove the first parameter for values

			_parameterVariables = CreateLisOfColumnsForInsert(true);

			Parameters = CreateParameters(tableData, false, true);

			ColumNames = CreateColumnNames(tableData, true);


		}

		public string CreateSproc()
        {
			var text = $"{ BeginningOfSprocText}{ LINE_BREAK}" +
				$"{Parameters}" +
				$"{LINE_BREAK}{SetNoCount}" +
				$"{LINE_BREAK}INSERT INTO {TableName} {LINE_BREAK}" +
				$"({ColumNames}){LINE_BREAK}" +
				$"VALUES({_parameterVariables})" +
				$"{LINE_BREAK}END{LINE_BREAK}{LINE_BREAK}";
			

			return text;
        }
		 public string GetSprocName(string tableName)
        {
			return $"{tableName}_Create";
        }
	}
}
