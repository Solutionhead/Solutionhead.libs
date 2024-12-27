using Solutionhead.DataAnnotations;

namespace Solutionhead.Data.Tests.TestClasses
{
    public class UniqueSingleColumnIndex
    {
        [SingleColumnIndex(true)]
        public string Prop1 { get; set; }
    }
}