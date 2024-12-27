using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solutionhead.Core;

namespace Solutionhead.Commands
{
    public abstract class SqlCommand : ICommand
    {
        protected readonly string SqlCommandText;

        protected SqlCommand(string sqlCommand)
        {
            SqlCommandText = sqlCommand;
        }

        public abstract void Execute();
    }
}
