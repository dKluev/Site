using System;
using System.Data.Linq;

namespace Specialist.Services.Core.Interface
{
    public interface IContextProvider
    {
        Type GetTypeByTableName(string tableName);
        DataContext Get(Type entityType);
    }
}