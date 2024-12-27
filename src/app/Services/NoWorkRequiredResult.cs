namespace Solutionhead.Services
{
    public sealed class NoWorkRequiredResult : IResult
    {
        #region Constructors.

        public NoWorkRequiredResult() { }

        public NoWorkRequiredResult(string message)
        {
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public ResultState State { get { return ResultState.NoWorkRequired; } }
        public bool Success { get { return true; } }
    }

    public sealed class NoWorkRequiredResult<TObject> : IResult<TObject>
    {
        #region Constructors.

        public NoWorkRequiredResult() { }

        public NoWorkRequiredResult(TObject resultingObject)
            : this(resultingObject, string.Empty)
        { }

        public NoWorkRequiredResult(TObject resultingObject, string message)
        {
            ResultingObject = resultingObject;
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public TObject ResultingObject { get; set; }
        public ResultState State { get { return ResultState.NoWorkRequired; } }
        public bool Success { get { return true; } }
    }

    public sealed class NoWorkRequiredResult<TResultingObject, TParameterObject> : IResult<TResultingObject, TParameterObject>
    {
        #region Constructors.

        public NoWorkRequiredResult() { }

        public NoWorkRequiredResult(TResultingObject resultingObject, TParameterObject parameterObject)
            : this(resultingObject, parameterObject, string.Empty)
        { }

        public NoWorkRequiredResult(TResultingObject resultingObject, TParameterObject parameterObject, string message)
        {
            ResultingObject = resultingObject;
            ParameterObject = parameterObject;
            Message = message;
        }

        #endregion

        public string Message { get; set; }
        public TResultingObject ResultingObject { get; set; }
        public TParameterObject ParameterObject { get; set; }
        public ResultState State { get { return ResultState.NoWorkRequired; } }
        public bool Success { get { return true; } }
    }
}
