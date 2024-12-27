using System;

namespace Solutionhead.Services
{
    public sealed class ExceptionResult : IResult
    {
        #region Constructors.

        public ExceptionResult(Exception exception, string message)
        {
            Exception = exception;
            Message = message;
        }

        public ExceptionResult(Exception exception) 
            : this(exception, "Exception occurred: " + exception.Message) { }

        #endregion

        #region Implementation of IResult

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public ResultState State
        {
            get { return ResultState.Failure; }
        }
        public bool Success
        {
            get { return false; }
        }

        #endregion
    }

    public sealed class ExceptionResult<TObject> : IResult<TObject>
    {
        #region Constructors.

        public ExceptionResult(TObject resultingObject, Exception exception, string message)
        {
            ResultingObject = resultingObject;
            Exception = exception;
            Message = message;
        }

        public ExceptionResult(TObject resultingObject, Exception exception)
            : this(resultingObject, exception, "Exception occurred: " + exception.Message) { }

        public ExceptionResult(Exception exception)
            : this(default(TObject), exception) { }

        #endregion

        #region Implementation of IResult

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public ResultState State
        {
            get { return ResultState.Failure; }
        }
        public bool Success
        {
            get { return false; }
        }

        #endregion

        #region Implementation of IResult<out TObject>

        public TObject ResultingObject { get; private set; }

        #endregion
    }
}