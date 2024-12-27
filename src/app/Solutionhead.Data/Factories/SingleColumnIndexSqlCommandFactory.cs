using System;
using System.Reflection;
using Solutionhead.Core;
using Solutionhead.Data;
using Solutionhead.DataAnnotations;

namespace Solutionhead.Factories
{
    public class SingleColumnIndexSqlCommandFactory<TEntity> where TEntity : class 
    {
        private readonly ITableNameIdentifier _tableNameIdentifier;

        private readonly ISqlCommandFactory _sqlCommandFactory;

        public SingleColumnIndexSqlCommandFactory(ITableNameIdentifier tableNameIdentifier, ISqlCommandFactory sqlCommandFactory)
        {
            if(tableNameIdentifier == null) { throw new ArgumentNullException("tableNameIdentifier"); }
            if(sqlCommandFactory == null) { throw new ArgumentNullException("sqlCommandFactory"); }

            _tableNameIdentifier = tableNameIdentifier;
            _sqlCommandFactory = sqlCommandFactory;
        }

        public ICommand BuildSqlCommand(PropertyInfo propertyInfo, SingleColumnIndexAttribute indexAttribute) 
        {
            if (propertyInfo == null) { throw new ArgumentNullException("propertyInfo"); }
            if (indexAttribute == null) { throw new ArgumentNullException("indexAttribute"); }

            var propertyName = propertyInfo.Name;

            var tableName = _tableNameIdentifier.GetTableName<TEntity>();

            var sqlCommand = string.Format("CREATE {2} IX_{0} ON {1} ({0} {3})",
                propertyName,
                tableName,
                indexAttribute.IsUnique ? "UNIQUE INDEX" : "INDEX",
                indexAttribute.Ordering == IndexOrdering.Ascending ? "ASC" : "DESC");

            return _sqlCommandFactory.BuildCommand(sqlCommand);
        }
    }
}
