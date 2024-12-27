using Solutionhead.DataAnnotations;

namespace Solutionhead.Data.Tests.TestClasses
{
    public class DefaultSingleColumnIndex
    {
        [SingleColumnIndex]
        public string Prop1 { get; set; }
    }
}