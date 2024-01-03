using SqlCrudCreatorCore.CRUD_Templates.SQL;
using SqlCrudCreatorCore.DAL;


namespace SqlCrudCreatorCore
{
    public class Select_Template: TemplateBase, iTemplate
	{
		
		public Select_Template(List<DataTableProperties> tableData, string tableName): base(tableData, tableName)
        {
			SprocName = GetSprocName(tableName);
			Parameters = CreateParameters(tableData, true);
		}

        public string CreateSproc()
        {
			var text =

			$"{BeginningOfSprocText}" +
			$"{Parameters}{LINE_BREAK}" +
			$"{SetNoCount}" +
			$"{LINE_BREAK}" +
			$"If(@{PrimaryKey} IS NULL){LINE_BREAK}" +
			$"BEGIN{LINE_BREAK}" +
			$"{TAB}SELECT {LINE_BREAK}" +
			$"{ColumNames}" +
			$"{TAB}FROM {TableName} WITH(NOLOCK) {LINE_BREAK}" +
			$"END{LINE_BREAK}" +
			$"ELSE{LINE_BREAK}" +
			$"BEGIN{LINE_BREAK}" +
			$"{TAB}" +
			$"SELECT {LINE_BREAK}" +
			$"{ColumNames}" +
			$"{TAB}FROM {TableName} WITH(NOLOCK) {LINE_BREAK}" +
			$"{TAB}WHERE{LINE_BREAK}{TAB} {PrimaryKey} = @{PrimaryKey}" +
			$"{LINE_BREAK}END{LINE_BREAK}" +
			$"END{LINE_BREAK}{LINE_BREAK}";


			return text;

		}

		public string GetSprocName(string tableName)
        {
			return $"{tableName}_Fetch";
		}
    }
}
