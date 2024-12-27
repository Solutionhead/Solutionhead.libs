namespace Solutionhead.Services
{
    public sealed class SuccessResult : IResult
    {
        #region Constructors.

        public SuccessResult() { }

        public SuccessResult(string message)
        {
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public ResultState State { get { return ResultState.Success; } }
        public bool Success { get { return true; } }
    }

    public sealed class SuccessResult<TObject> : IResult<TObject>
    {
        #region Constructors.

        public SuccessResult() { }

        public SuccessResult(TObject resultingObject)
            : this(resultingObject, string.Empty){ }

        public SuccessResult(TObject resultingObject, string message)
        {
            ResultingObject = resultingObject;
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public TObject ResultingObject { get; set; }
        public ResultState State { get { return ResultState.Success; } }
        public bool Success { get { return true; } }
    }

    public sealed class SuccessResult<TResultingObject, TParameterObject> : IResult<TResultingObject, TParameterObject>
    {
        #region Constructors.

        public SuccessResult() { }

        public SuccessResult(TResultingObject resultingObject, TParameterObject parameterObject)
            : this(resultingObject, parameterObject, string.Empty)
        { }

        public SuccessResult(TResultingObject resultingObject, TParameterObject parameterObject, string message)
        {
            ResultingObject = resultingObject;
            ParameterObject = parameterObject;
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public TResultingObject ResultingObject { get; set; }
        public TParameterObject ParameterObject { get; set; }
        public ResultState State { get { return ResultState.Success; } }
        public bool Success { get { return true; } }
    }
}