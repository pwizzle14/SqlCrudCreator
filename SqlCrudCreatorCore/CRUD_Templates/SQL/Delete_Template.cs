using SqlCrudCreatorCore.CRUD_Templates.SQL;
using SqlCrudCreatorCore.DAL;


namespace SqlCrudCreatorCore
{
    public class Delete_Template: TemplateBase, iTemplate
    {
		public Delete_Template(List<DataTableProperties> tableData, string tableName): base(tableData,tableName)
		{
			SprocName = GetSprocName(tableName);
			Parameters = CreateParameters(tableData, true);
		}

        public string CreateSproc()
		{
			var text = $"{ BeginningOfSprocText}{ LINE_BREAK}" +
				$"{Parameters}" +
				$"{LINE_BREAK}{SetNoCount}" +
				$"{LINE_BREAK}DELETE {TableName} {LINE_BREAK}" +
				$"WHERE {PrimaryKey} = @{PrimaryKey}" +
				$"{LINE_BREAK}{LINE_BREAK}END{LINE_BREAK}{LINE_BREAK}";


			return text;
		}

		public string GetSprocName(string tableName)
        {
			return $"{tableName}_Delete";

		}

	}
}
