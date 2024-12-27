namespace Solutionhead.Services.DataTransferObjects
{
    public abstract class ResultsDataTransferObjectBase<TObject> : IResultsDataTransferObject<TObject> where TObject : IDataTransferObject
    {
        #region fields

        private readonly TObject _resultingObject;

        private readonly ResultState _state;

        private readonly string _message;

        #endregion

        #region properties

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public ResultState State
        {
            get
            {
                return _state;
            }
        }

        public TObject ResultingObject
        {
            get
            {
                return _resultingObject;
            }
        }

        #endregion

        #region constructors

        public ResultsDataTransferObjectBase(TObject ResultingObject, ResultState CurrentState)
            : this(ResultingObject, CurrentState, string.Empty) { }

        public ResultsDataTransferObjectBase(TObject ResultingObject, ResultState CurrentState, string Message)
        {
            _resultingObject = ResultingObject;
            _state = CurrentState;
            _message = Message;
        }

        #endregion
    }
}
