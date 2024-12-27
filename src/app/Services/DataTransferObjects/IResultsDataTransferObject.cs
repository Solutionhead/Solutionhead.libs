namespace Solutionhead.Services.DataTransferObjects
{
    public interface IResultsDataTransferObject<TObject> where TObject : IDataTransferObject
    {
        string Message { get; }
        TObject ResultingObject { get; }
        ResultState State { get; }
    }
}
