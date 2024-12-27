using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutionhead.Data.Archiving.EntityFramework
{
    public class EFArchive<TObject> : IArchive<TObject> where TObject : class
    {
        #region members

        private readonly DbContext _context;

        #endregion

        #region constructors

        public EFArchive(DbContext context)
        {
            _context = context;
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

        #endregion

        public void InsertObjectIntoArchive(TObject objectToArchive)
        {
        }

        /*public virtual TObject RetrieveObjectFromArchive(string primaryKey)
        {
            return TObject;
        }*/
    }
}
