namespace Solutionhead.Data.Archiving
{
    public interface IArchiveObjectIdentity
    {
        string ObjectTypeId { get; }

        string ObjectReferenceId { get; }

        string RevisionId { get; }
    }
}