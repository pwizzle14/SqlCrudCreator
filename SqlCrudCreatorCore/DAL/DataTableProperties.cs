using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCrudCreatorCore.DAL
{
    public sealed class DataTableProperties
    {
        public string ColumnName { get; set; }
        public int ColumnOrdinal { get; set; }
        public int ColumnSize { get; set; }
        public int NumericPrecision { get; set; }
        public int NumericScale { get; set; }
        public bool IsUnique { get; set; }
        public object IsKey { get; set; }
        public object BaseServerName { get; set; }
        public object BaseCatalogName { get; set; }
        public string BaseColumnName { get; set; }
        public object BaseSchemaName { get; set; }
        public object BaseTableName { get; set; }
        public string DataType { get; set; }
        public bool AllowDBNull { get; set; }
        public int ProviderType { get; set; }
        public object IsAliased { get; set; }
        public object IsExpression { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsAutoIncrement { get; set; }
        public bool IsRowVersion { get; set; }
        public object IsHidden { get; set; }
        public bool IsLong { get; set; }
        public bool IsReadOnly { get; set; }
        public string ProviderSpecificDataType { get; set; }
        public string DataTypeName { get; set; }
        public object XmlSchemaCollectionDatabase { get; set; }
        public object XmlSchemaCollectionOwningSchema { get; set; }
        public object XmlSchemaCollectionName { get; set; }
        public object UdtAssemblyQualifiedName { get; set; }
        public int NonVersionedProviderType { get; set; }
        public bool IsColumnSet { get; set; }
    }

}
