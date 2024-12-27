using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solutionhead.Data.Archiving;

namespace Solutionhead.Archive.EntityFramework
{
    public class EFArchiveWriter<TObject> : IArchiveWriter<TObject> where TObject : class
    {
        public void ArchiveObject(TObject objectToArchive)
        {
        }
        
        public void InsertObjectIntoArchive<TIncludeProperty>(TObject objectToArchive, Func<TObject, TIncludeProperty> include)
        {
            // build archive object for objectToArchive

            var includedProperty = include.Invoke(objectToArchive);
            // build archive object for includedProperty
            // what if includedProperty is a collection?
        }

        /*public virtual TObject RetrieveObjectFromArchive(string primaryKey)
        {
            return TObject;
        }*/

        public IArchiveObject<TObject> WriteToArchive(TObject objectToArchive)
        {
            throw new NotImplementedException();
        }
    }
}
