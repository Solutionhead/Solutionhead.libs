namespace Solutionhead.Services.DataTransferObjects
{
    public abstract class DataTransferObjectBase<TObject> : Solutionhead.Services.DataTransferObjects.IDataTransferObject
    {

        #region IDataTransferObject Members

        public object[] Keys
        {
            get { return GetKeys(); }
        }

        #endregion

        protected abstract object[] GetKeys();
    }
}
