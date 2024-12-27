using System.Collections.Generic;
using Solutionhead.Commands;

namespace Solutionhead.Data.Tests.TestClasses
{
    public class TestableCommandFactory : ISqlCommandFactory
    {
        public List<TestableCommand> Commands { get; private set; }

        public TestableCommandFactory()
        {
            Commands = new List<TestableCommand>();
        }

        public SqlCommand BuildCommand(string sqlCommand)
        {
            var command = new TestableCommand(sqlCommand);
            Commands.Add(command);
            return command;
        }
    }
}