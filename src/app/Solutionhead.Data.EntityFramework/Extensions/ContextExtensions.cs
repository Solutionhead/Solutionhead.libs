using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Text.RegularExpressions;

namespace Solutionhead.Extensions
{
    public static class ContextExtensions
    {
        public static string GetTableName<TEntity>(this DbContext context) where TEntity : class
        {
            var objectContextAdapter = context as IObjectContextAdapter;
            var objectContext = objectContextAdapter.ObjectContext;

            return objectContext.GetTableName<TEntity>();
        }

        public  static string GetTableName<TEntity>(this ObjectContext context) where TEntity : class
        {
            var sql = context.CreateObjectSet<TEntity>().ToTraceString();
            var regex = new Regex("FROM (?<table>.*) AS", RegexOptions.IgnoreCase);
            var match = regex.Match(sql);

            var tableName = match.Groups["table"].Value;
            return tableName;
        }
    }
}
