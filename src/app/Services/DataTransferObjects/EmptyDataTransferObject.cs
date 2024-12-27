namespace Solutionhead.Services.DataTransferObjects
{
    public sealed class EmptyDataTransferObject : IDataTransferObject
    {
        #region IDataTransferObject Members

        public object[] Keys
        {
            get { return null; }
        }

        #endregion
    }
}
