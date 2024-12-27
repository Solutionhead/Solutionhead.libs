using Solutionhead.Data.Archiving;

namespace Solutionhead.Data.Tests
{
    public class ArchiveObjectIdentity : IArchiveObjectIdentity
    {
        public string ObjectTypeId { get; set; }

        public string ObjectReferenceId { get; set; }

        public string RevisionId { get; set; }
    }
}