using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Solutionhead.Commands;

namespace Solutionhead.Data.EntityFramework.Utilities
{
    public class DbContextSqlCommandFactory : ISqlCommandFactory
    {
        private readonly DbContext _dbContext;

        public DbContextSqlCommandFactory(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SqlCommand BuildCommand(string sqlCommand)
        {
            return new DbContextSqlCommand(_dbContext, sqlCommand);
        }
    }
}
