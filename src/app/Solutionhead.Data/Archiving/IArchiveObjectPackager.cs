namespace Solutionhead.Data.Archiving
{
    public interface IArchiveObjectPackager<TObject> where TObject : class
    {
        IArchiveObject<TObject> PackageObjectForArchiving(TObject objectToArchive);
    }
}