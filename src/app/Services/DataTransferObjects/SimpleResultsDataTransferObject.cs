namespace Solutionhead.Services.DataTransferObjects
{
    /// <summary>
    /// Container for returning the resulting state of a procedure. The ResultingObject property will be of type EmptyDataTransferObject.
    /// </summary>
    public sealed class SimpleResultsDataTransferObject : ResultsDataTransferObjectBase<EmptyDataTransferObject>
    {
        public SimpleResultsDataTransferObject(ResultState CurrentState, string Message)
            : base(new EmptyDataTransferObject(), CurrentState, Message) { }
    }

    /// <summary>
    /// Container for returning the resulting state of a procedure. 
    /// </summary>
    /// <typeparam name="TObject">The type of object to be returned as the ResultingObject property.</typeparam>
    public sealed class SimpleResultsDataTransferObject<TObject> : ResultsDataTransferObjectBase<TObject> where TObject : IDataTransferObject
    {
        #region constructors

        public SimpleResultsDataTransferObject(TObject ResultingObject, ResultState CurrentState, string Message)
            : base(ResultingObject, CurrentState, Message) { }

        #endregion
    }
}
