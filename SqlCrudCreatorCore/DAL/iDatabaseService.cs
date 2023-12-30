using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;

namespace SqlCrudCreatorCore.DAL
{
    public interface IDatabaseService
    {
         DataTable GetTableInfo(string tableName);
    }
}
