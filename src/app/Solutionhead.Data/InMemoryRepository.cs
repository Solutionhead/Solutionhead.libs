using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Solutionhead.Data
{
    public class InMemoryRepository<TObject> : RepositoryBase<TObject> where TObject : class
    {
        private List<TObject> _items;

        protected List<TObject> Items
        {
            get { return _items ?? (_items = new List<TObject>()); }
        }

        #region Overrides of RepositoryBase<TObject>

        public override void Dispose()
        {
            _items.Clear();
        }

        public override IQueryable<TObject> SourceQuery
        {
            get { return Items.AsQueryable(); }
        }

        public override TObject Add(TObject entity)
        {
            SourceQuery.ToList().Add(entity);
            return entity;
        }

        public override void Remove(TObject entity)
        {
            SourceQuery.ToList().Remove(entity);
        }

        public override void DropChanges(TObject entity)
        {
            throw new NotSupportedException("The in-memory repository does not support the DropChanges method.");
        }

        public override IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate, params Expression<Func<TObject, object>>[] includePaths)
        {
            return SourceQuery.Where(predicate);
        }

        #endregion
    }
}