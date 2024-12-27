using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Solutionhead.Core;

namespace Solutionhead.Commands
{
    public class DbContextSqlCommand : SqlCommand
    {
        private readonly DbContext _context;

        public DbContextSqlCommand(DbContext context, string sqlCommand)
            : base(sqlCommand)
        {
            _context = context;
        }

        public override void Execute()
        {
            _context.Database.ExecuteSqlCommand(SqlCommandText);
        }
    }
}
