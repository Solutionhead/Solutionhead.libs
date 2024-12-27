using System;

namespace Solutionhead.Services
{
    public static class IResultExtensions
    {
        public static IResult ChangeMessage(this IResult result, string newMessage)
        {
            return new BasicResult(result.State, newMessage);
        }

        public static IResult<TObject> ChangeMessage<TObject>(this IResult<TObject> result, string newMessage)
        {
            return result.ConvertTo(result.ResultingObject, newMessage);
        }

        public static IResult<TObject> ConvertTo<TObject>(this IResult result, TObject @object = default(TObject))
        {
            return new BasicResult<TObject>(@object, result.State, result.Message);
        }

        public static IResult<TObject> ConvertTo<TObject>(this IResult result, TObject @object, string message)
        {
            return new BasicResult<TObject>(@object, result.State, message);
        }

        public static IResult<TResultObject, TParameter> ConvertTo<TResultObject, TParameter>(this IResult result, TResultObject resultObject, TParameter parameterObject)
        {
            return new BasicResult<TResultObject, TParameter>(resultObject, parameterObject, result.State, result.Message);
        }

        public static IResult<TNewResult> ConvertTo<TSourceResult, TNewResult>(this IResult<TSourceResult> result, Func<TSourceResult, TNewResult> expression)
        {
            if(result == null) { throw new ArgumentNullException("result"); }
            if(expression == null) { throw new ArgumentNullException("expression"); }

            return result.ConvertTo(result.ResultingObject == null ? default(TNewResult) : expression(result.ResultingObject));
        }
    }
}