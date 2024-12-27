using Solutionhead.DataAnnotations;

namespace Solutionhead.Data.Tests.TestClasses
{
    public class UniqueDescendingSingleColumnIndex
    {
        [SingleColumnIndex(true, IndexOrdering.Descending)]
        public string Prop1 { get; set; }
    }
}