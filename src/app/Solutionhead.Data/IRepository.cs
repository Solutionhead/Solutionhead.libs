using System;
using System.Linq;
using System.Linq.Expressions;

namespace Solutionhead.Data
{
    public interface IRepository<TObject> : IDisposable where TObject : class
    {
        [Obsolete("This property has some unintended consequences namely hidden method calls in the EF implementation. Use All.Count() instead.")]
        int Count { get; }

        IQueryable<TObject> SourceQuery { get; }
            
        TObject Add(TObject entity);

        IQueryable<TObject> All();

        bool Contains(Expression<Func<TObject, bool>> predicate);

        bool Contains(IKey<TObject> key);

        int CountOf(Expression<Func<TObject, bool>> predicate);

        void Remove(TObject entity);

        void Remove(Expression<Func<TObject, bool>> predicate);

        void DropChanges(TObject entity);

        IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate, params Expression<Func<TObject, object>>[] includePaths);

        IQueryable<TObject> FilterByKey(IKey<TObject> key, params Expression<Func<TObject, object>>[] includePaths);

        TObject FindByKey(IKey<TObject> key, params Expression<Func<TObject, object>>[] includePaths);
        
        /// <summary>
        /// Returns the first element of a sequence that satisfies the condition. If no elements match the condition, then null return is returned.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="includePaths">Optional expression tree paths to include in the query results</param>
        /// <returns></returns>
        TObject FindBy(Expression<Func<TObject, bool>> predicate, params Expression<Func<TObject, object>>[] includePaths);
    }
}