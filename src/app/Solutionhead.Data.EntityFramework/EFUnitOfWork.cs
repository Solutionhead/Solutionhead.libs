using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Solutionhead.Data.EntityFramework
{
    public class EFUnitOfWork : IUnitOfWork
    {
        #region members

        protected readonly DbContext Context;

        #endregion

        #region constructors

        public EFUnitOfWork(DbContext context)
        {
            Context = context;
            LazyLoadingEnabled = false;
            AutoDetectChangesInDataContext = true;
        }

        #endregion

        #region properties

        #region configuration properties

        public bool AutoDetectChangesInDataContext
        {
            get { return Context.Configuration.AutoDetectChangesEnabled; }
            set { Context.Configuration.AutoDetectChangesEnabled = value; }
        }

        public bool LazyLoadingEnabled
        {
            get { return Context.Configuration.LazyLoadingEnabled; }
            set { Context.Configuration.LazyLoadingEnabled = value; }
        }

        #endregion

        #endregion

        #region methods

        public void Commit()
        {
            Context.SaveChanges();
        }

        #endregion
    }
}
