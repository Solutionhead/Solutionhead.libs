namespace Solutionhead.Services
{
    public sealed class BasicResult : IResult
    {
        #region Constructors.

        public BasicResult() { }

        public BasicResult(ResultState state, string message)
        {
            Message = message;
            State = state;
        }

        public BasicResult(IResult fromResult)
            :this(fromResult.State, fromResult.Message) { }

        #endregion

        public string Message { get; set; }
        public ResultState State { get; private set; }
        public bool Success { get { return State == ResultState.Success; } }
    }

    public class BasicResult<TObject> : IResult<TObject>
    {
        #region Constructors.

        public BasicResult() { }

        public BasicResult(TObject resultingObject, ResultState state)
            : this(resultingObject, state, string.Empty)
        { }

        public BasicResult(TObject resultingObject, ResultState state, string message)
        {
            ResultingObject = resultingObject;
            Message = message;
            State = state;
        }

        public BasicResult(TObject resultingObject, IResult fromResult, string message = null)
            : this(resultingObject, fromResult.State, message == null ? fromResult.Message : message + " Inner message: " + fromResult) { }

        #endregion

        public string Message { get; set; }
        public TObject ResultingObject { get; private set; }
        public ResultState State { get; private set; }
        public bool Success { get { return State == ResultState.Success;  } }

        public static BasicResult<TObject> FromResult(IResult result, TObject transformedObject)
        {
            return new BasicResult<TObject>(transformedObject, result.State, result.Message);
        }
    }

    public sealed class BasicResult<TResultingObject, TParameterObject> : IResult<TResultingObject, TParameterObject>
    {
        #region Constructors.

        public BasicResult() { }

        public BasicResult(TResultingObject resultingObject, TParameterObject parameterObject, ResultState state)
            : this(resultingObject, parameterObject, state, string.Empty)
        { }

        public BasicResult(TResultingObject resultingObject, TParameterObject parameterObject, ResultState state, string message)
        {
            ResultingObject = resultingObject;
            ParameterObject = parameterObject;
            Message = message;
            State = state;
        }

        #endregion

        public string Message { get; set; }
        public TResultingObject ResultingObject { get; private set; }
        public TParameterObject ParameterObject { get; private set; }
        public ResultState State { get; private set; }
        public bool Success { get { return State == ResultState.Success; } }
    }
}
