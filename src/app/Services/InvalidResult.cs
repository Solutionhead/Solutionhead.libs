namespace Solutionhead.Services
{
    public sealed class InvalidResult : IResult
    {
        #region Constructors.

        public InvalidResult() { }

        public InvalidResult(string message)
        {
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public ResultState State { get { return ResultState.Invalid; } }
        public bool Success { get { return false; } }
    }

    public sealed class InvalidResult<TObject> : IResult<TObject>
    {
        #region Constructors.

        public InvalidResult()
        {
        }

        public InvalidResult(TObject resultingObject)
            : this(resultingObject, string.Empty)
        {
        }

        public InvalidResult(TObject resultingObject, string message)
        {
            ResultingObject = resultingObject;
            Message = message;
        }

        #endregion

        public string Message { get; set; } 
        public TObject ResultingObject { get; set; } 
        public ResultState State { get { return ResultState.Invalid; } }
        public bool Success { get { return false; } }
    }

    public sealed class InvalidResult<TResultingObject, TParameterObject> : IResult<TResultingObject, TParameterObject>
    {
        #region Constructors.

        public InvalidResult() { }

        public InvalidResult(TResultingObject resultingObject, TParameterObject parameterObject)
            : this(resultingObject, parameterObject, string.Empty)
        { }

        public InvalidResult(TResultingObject resultingObject, TParameterObject parameterObject, string message)
        {
            ResultingObject = resultingObject;
            ParameterObject = parameterObject;
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public TResultingObject ResultingObject { get; set; }
        public TParameterObject ParameterObject { get; set; }
        public ResultState State { get { return ResultState.Invalid; } }
        public bool Success { get { return false; } }
    }
}
