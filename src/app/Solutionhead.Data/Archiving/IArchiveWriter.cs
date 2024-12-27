namespace Solutionhead.Data.Archiving
{
    public interface IArchiveWriter<TObject> where TObject : class
    {
        IArchiveObject<TObject> WriteToArchive(TObject objectToArchive);
    }
}
