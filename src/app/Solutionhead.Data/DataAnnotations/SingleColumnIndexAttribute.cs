using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Solutionhead.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SingleColumnIndexAttribute : Attribute
    {
        private readonly bool _isUnique;

        private readonly IndexOrdering _ordering;

        public SingleColumnIndexAttribute(bool isUnique = false, IndexOrdering ordering = IndexOrdering.Ascending)
        {
            _isUnique = isUnique;
            _ordering = ordering;
        }

        public bool IsUnique { get { return _isUnique; } }

        public IndexOrdering Ordering { get { return _ordering;  } }
    }

    public enum IndexOrdering
    {
        Ascending = 0,
        Descending = 1
    }
}
