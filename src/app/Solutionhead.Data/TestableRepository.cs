using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solutionhead.Data
{
    public class TestableRepository<TObject> : RepositoryBase<TObject> where TObject : class
    {
        public bool IsDisposed { get; private set; }

        public TObject LastChangesDropped { get; private set; }

        public List<Expression<Func<TObject, object>>> LastFilterParams { get; private set; }

        private readonly IList<TObject> _itemsSource; 

        public TestableRepository(IList<TObject> itemsSource)
        {
            if(itemsSource == null) { throw new ArgumentNullException("itemsSource"); }

            _itemsSource = itemsSource;
        }

        #region Overrides of RepositoryBase<TObject>

        public override void Dispose()
        {
            IsDisposed = true;
        }

        public override IQueryable<TObject> SourceQuery
        {
            get { return _itemsSource.AsQueryable(); }
        }

        public override TObject Add(TObject entity)
        {
            ensureNotDisposed();
            _itemsSource.Add(entity);
            return entity;
        }

        public override void Remove(TObject entity)
        {
            ensureNotDisposed();
            _itemsSource.Remove(entity);
        }

        public override void DropChanges(TObject entity)
        {
            LastChangesDropped = entity;
        }

        public override IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate, params Expression<Func<TObject, object>>[] includePaths)
        {
            ensureNotDisposed();
            LastFilterParams = new List<Expression<Func<TObject, object>>>(includePaths);
            return SourceQuery.Where(predicate);
        }

        #region virtual method overrides

        public override IQueryable<TObject> All()
        {
            ensureNotDisposed();
            return base.All();
        }
        public override bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            ensureNotDisposed();
            return base.Contains(predicate);
        }
        public override bool Contains(IKey<TObject> key)
        {
            ensureNotDisposed();
            return base.Contains(key);
        }
        public override int Count
        {
            get
            {
                ensureNotDisposed();
                return base.Count;
            }
        }
        public override int CountOf(Expression<Func<TObject, bool>> predicate)
        {
            ensureNotDisposed();
            return base.CountOf(predicate);
        }
        public override IQueryable<TObject> FilterByKey(IKey<TObject> key, params Expression<Func<TObject, object>>[] includePaths)
        {
            ensureNotDisposed();
            return base.FilterByKey(key, includePaths);
        }
        public override TObject FindBy(Expression<Func<TObject, bool>> predicate, params Expression<Func<TObject, object>>[] includePaths)
        {
            ensureNotDisposed();
            return base.FindBy(predicate, includePaths);
        }
        public override TObject FindByKey(IKey<TObject> key, params Expression<Func<TObject, object>>[] includePaths)
        {
            ensureNotDisposed();
            return base.FindByKey(key, includePaths);
        }
        public override void Remove(Expression<Func<TObject, bool>> predicate)
        {
            ensureNotDisposed();
            base.Remove(predicate);
        }

        #endregion

        #endregion

        private void ensureNotDisposed()
        {
            if(IsDisposed) { throw new ObjectDisposedException("SourceQuery"); }
        }
    }
}
