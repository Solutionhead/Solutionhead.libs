using System;
using System.Data.Entity;
using System.Reflection;
using Solutionhead.Core;
using Solutionhead.Factories;
using Solutionhead.DataAnnotations;
using Solutionhead.Extensions;
using Solutionhead.Helpers;

namespace Solutionhead.Data.EntityFramework
{
    public class DatabaseIndexBuilder<TSource> where TSource : class
    {
        private readonly SingleColumnIndexSqlCommandFactory<TSource> _indexSqlCommandFactory;

        public DatabaseIndexBuilder(ITableNameIdentifier tableNameIdentifier, ISqlCommandFactory sqlCommandFactory)
        {
            _indexSqlCommandFactory = new SingleColumnIndexSqlCommandFactory<TSource>(tableNameIdentifier, sqlCommandFactory);
        }

        public void CreateAllIndices()
        {
            var sourceObject = ReflectionHelpers.ConstructDefaultInstance<TSource>();
            var indexProperties = sourceObject.GetPropertiesWithAttribute<TSource, SingleColumnIndexAttribute>();

            foreach (var indexProperty in indexProperties)
            {
                var indexAttribute = Attribute.GetCustomAttribute(indexProperty, (typeof (SingleColumnIndexAttribute))) as SingleColumnIndexAttribute;

                var sqlCommand = _indexSqlCommandFactory.BuildSqlCommand(indexProperty, indexAttribute);
                sqlCommand.Execute();
            }
        }
    }
}
