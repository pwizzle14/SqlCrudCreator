using SqlCrudCreatorCore.CRUD_Templates.SQL;
using SqlCrudCreatorCore.DAL;

namespace SqlCrudCreatorCore
{
    public class Update_Template: TemplateBase, iTemplate
	{
		public Update_Template(List<DataTableProperties> tableData, string tableName) : base(tableData, tableName)
		{
			SprocName = GetSprocName(tableName);
		}

		public string CreateSproc()
		{
			var text = $"{ BeginningOfSprocText}{ LINE_BREAK}" +
				$"{Parameters}" +
				$"{LINE_BREAK}{SetNoCount}" +
				$"{LINE_BREAK}UPDATE {TableName} {LINE_BREAK}" +
				$"SET {LINE_BREAK}{LINE_BREAK}" +
				$"{UpdateValues}" +
				$"{LINE_BREAK}" +
				$"{LINE_BREAK}WHERE {PrimaryKey} = @{PrimaryKey}" +
				$"{LINE_BREAK}{LINE_BREAK}END{LINE_BREAK}{LINE_BREAK}";


			return text;
		}

		public string GetSprocName(string tableName)
		{
			return $"{tableName}_Update";

		}
	}
}
