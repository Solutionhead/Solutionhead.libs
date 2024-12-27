namespace Solutionhead.Services
{
    public interface IResult
    {
        string Message { get; }
        ResultState State { get; }
        bool Success { get; }
    }

    public interface IResult<out TObject> : IResult
    {
        TObject ResultingObject { get; }
    }

    public interface IResult<out TResultingObject, out TSourceObject> : IResult
    {
        TResultingObject ResultingObject { get; }
        TSourceObject ParameterObject { get; }
    }
}
