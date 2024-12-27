using System;
using System.Linq;
using System.Linq.Expressions;

namespace Solutionhead.Data
{
    public abstract class RepositoryBase<TObject> : IRepository<TObject> where TObject : class
    {
        public abstract void Dispose();

        public virtual int Count
        {
            get { return SourceQuery.Count(); }
        }

        public abstract IQueryable<TObject> SourceQuery { get; }

        public abstract TObject Add(TObject entity);

        public virtual IQueryable<TObject> All()
        {
            return SourceQuery;
        }

        public virtual bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return SourceQuery.Any(predicate.Compile());
        }

        public virtual bool Contains(IKey<TObject> key)
        {
            return Contains(key.FindByPredicate);
        }

        public virtual int CountOf(Expression<Func<TObject, bool>> predicate)
        {
            return SourceQuery.Count(predicate.Compile());
        }

        public abstract void Remove(TObject entity);

        public virtual void Remove(Expression<Func<TObject, bool>> predicate)
        {
            var items = Filter(predicate);
            foreach (var item in items)
            {
                Remove(item);
            }
        }

        public abstract void DropChanges(TObject entity);

        public abstract IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate,
                                                   params Expression<Func<TObject, object>>[] includePaths);

        public virtual IQueryable<TObject> FilterByKey(IKey<TObject> key, params Expression<Func<TObject, object>>[] includePaths)
        {
            return Filter(key.FindByPredicate, includePaths);
        }

        public virtual TObject FindByKey(IKey<TObject> key, params Expression<Func<TObject, object>>[] includePaths)
        {
            if (key == null) { throw new ArgumentNullException("key"); }

            return FindBy(key.FindByPredicate, includePaths);
        }

        public virtual TObject FindBy(Expression<Func<TObject, bool>> predicate, params Expression<Func<TObject, object>>[] includePaths)
        {
            return Filter(predicate, includePaths).FirstOrDefault();
        }
    }
}