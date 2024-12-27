using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Solutionhead.Data
{
    public interface IKey<TObject> where TObject : class
    {
        Expression<Func<TObject, bool>> FindByPredicate { get; }
    }
}
