using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Solutionhead.Data.EntityFramework
{
    public class EFRepository<TObject> : RepositoryBase<TObject> 
                                         where TObject : class
    {   
        #region members

        private readonly DbContext _context;
        private readonly DbSet<TObject> _dbSet;

        #endregion

        #region constructors

        public EFRepository(DbContext context)
        {
            _context = context;
            LazyLoadingEnabled = false;
            AutoDetectChangesInDataContext = true;

            try
            {
                _dbSet = _context.Set<TObject>();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("TObject is an invalid type for the DbContext.", ex);
            }
        }

        #endregion

        #region properties

        #region configuration properties

        public bool AutoDetectChangesInDataContext
        {
            get { return _context.Configuration.AutoDetectChangesEnabled; }
            set { _context.Configuration.AutoDetectChangesEnabled = value; }
        }

        public bool LazyLoadingEnabled
        {
            get { return _context.Configuration.LazyLoadingEnabled; }
            set { _context.Configuration.LazyLoadingEnabled = value; }
        }

        #endregion

        protected DbSet<TObject> DbSet
        {
            get { return _dbSet; }
        }

        public override IQueryable<TObject> SourceQuery
        {
            get { return DbSet; }
        }

        #endregion

        #region methods

        public override TObject Add(TObject entity)
        {
            return DbSet.Add(entity);
        }

        public override void Remove(TObject entity)
        {
            DbSet.Remove(entity);
        }

        public override void DropChanges(TObject entity)
        {
            if(entity == null) throw new ArgumentNullException("entity");

            var entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Detached;
        }

        public override IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate, params Expression<Func<TObject, object>>[] includePaths)
        {
            var query = SourceQuery;

            EFRepository<TObject>.includePaths(ref query, includePaths);

            return query.Where(predicate);
        }

        public override void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        #endregion

        private static void includePaths(ref IQueryable<TObject> query, params Expression<Func<TObject, object>>[] includePaths)
        {
            if (includePaths == null) return;

            query = includePaths.Aggregate(query, (current, includePath) => current.Include(includePath));
        }
    }
}