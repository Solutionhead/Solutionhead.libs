namespace Solutionhead.Services
{
    public sealed class FailureResult : IResult
    {
        #region Constructors.

        public FailureResult() { }

        public FailureResult(string message)
        {
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public ResultState State { get { return ResultState.Failure; } }
        public bool Success { get { return false; } }
    }

    public sealed class FailureResult<TObject> : IResult<TObject>
    {
        #region Constructors.

        public FailureResult() { }
        
        public FailureResult(TObject resultingObject)
            : this(resultingObject, string.Empty) { }

        public FailureResult(TObject resultingObject, string message)
        {
            ResultingObject = resultingObject;
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public TObject ResultingObject { get; set; }
        public ResultState State { get { return ResultState.Failure; } }
        public bool Success { get { return false; } }
    }

    public sealed class FailureResult<TResultingObject, TParameterObject> : IResult<TResultingObject, TParameterObject>
    {
        #region Constructors.

        public FailureResult() { }

        public FailureResult(TResultingObject resultingObject, TParameterObject parameterObject)
            : this(resultingObject, parameterObject, string.Empty)
        { }

        public FailureResult(TResultingObject resultingObject, TParameterObject parameterObject, string message)
        {
            ResultingObject = resultingObject;
            ParameterObject = parameterObject;
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public TResultingObject ResultingObject { get; set; }
        public TParameterObject ParameterObject { get; set; }
        public ResultState State { get { return ResultState.Failure; } }
        public bool Success { get { return false; } }
    }
}