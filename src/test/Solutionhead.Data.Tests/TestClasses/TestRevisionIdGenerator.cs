namespace Solutionhead.Data.Tests.TestClasses
{
    internal class TestRevisionIdGenerator : IRevisionIdGenerator
    {
        private readonly string _revisionId;

        public TestRevisionIdGenerator(string revisionIdToReturn)
        {
            _revisionId = revisionIdToReturn;
        }

        public string GenerateRevisionId()
        {
            return _revisionId;
        }
    }
}
