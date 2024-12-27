using Solutionhead.Commands;

namespace Solutionhead.Data.Tests.TestClasses
{
    public class TestableCommand : SqlCommand
    {
        public bool Executed { get; private set; }

        public string PublicSqlCommand
        {
            get { return SqlCommandText; }
        }

        public TestableCommand(string sqlCommand)
            : base(sqlCommand) { }

        public override void Execute()
        {
            Executed = true;
        }
    }
}