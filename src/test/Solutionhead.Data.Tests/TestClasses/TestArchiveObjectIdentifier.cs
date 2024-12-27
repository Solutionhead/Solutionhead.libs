using System.Linq;
using Solutionhead.Data.Archiving;

namespace Solutionhead.Data.Tests.TestClasses
{
    public class TestArchiveObjectIdentifier<TObject> : IArchiveObjectIdentityBuilder<TObject> where TObject : class
    {
        private string _archiveType;
        private string _objectReferenceId;

        public IArchiveObjectIdentity BuildArchiveObjectIdentity(TObject objectToArchive)
        {
            var firstProperty = typeof(TObject).GetProperties().Take(1).Select(p => p.GetValue(objectToArchive, null)).FirstOrDefault();

            return new ArchiveObjectIdentity
                       {
                           ObjectTypeId = typeof(TObject).FullName,
                           ObjectReferenceId = firstProperty == null ? string.Empty : firstProperty.ToString()
                       };
        }
    }
}