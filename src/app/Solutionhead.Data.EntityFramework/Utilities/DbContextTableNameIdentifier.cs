using System.Data.Entity;
using Solutionhead.Extensions;

namespace Solutionhead.Data.EntityFramework.Utilities
{
    public class DbContextTableNameIdentifier : ITableNameIdentifier
    {
        private readonly DbContext _dbContext;

        public DbContextTableNameIdentifier(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GetTableName<TEntity>() where TEntity : class 
        {
            return _dbContext.GetTableName<TEntity>();
        }
    }
}