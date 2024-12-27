using System;

namespace Solutionhead.EntityKey
{
    public abstract class EntityKeyBase
    {
        public abstract string GetParseFailMessage(string inputValue = null);

        public abstract string KeyValue { get; }

        public override string ToString()
        {
            return KeyValue;
        }

        public static implicit operator string(EntityKeyBase key)
        {
            return key == null ? null : key.KeyValue;
        }
        
        private EntityKeyBase() { }

        public abstract class Of<TKeyInterface> : EntityKeyBase, IKeyParser<TKeyInterface>, IKeyStringBuilder<TKeyInterface>
            where TKeyInterface : class
        {
            #region Implementation of IKeyParser<TKeyInterface>

            public TKeyInterface Parse(string keyValue)
            {
                try
                {
                    return ParseImplementation(keyValue);
                }
                catch(Exception ex)
                {
                    throw new FormatException(string.Format("The keyValue '{0}' was in an invalid format. See inner exception for details.", keyValue), ex);
                }
            }

            public bool TryParse(string keyValue, out TKeyInterface key)
            {
                try
                {
                    key = Parse(keyValue);
                }
                catch
                {
                    key = null;
                    return false;
                }

                return true;
            }

            #endregion

            #region Implementation of IKeyStringBuilder<in TKeyInterface>

            public string BuildKeyValue(TKeyInterface key)
            {
                return BuildKeyValueImplementation(key);
            }

            #endregion

            protected abstract TKeyInterface ParseImplementation(string keyValue);

            public override string KeyValue { get { return BuildKeyValue(Key); } }

            public abstract TKeyInterface Default { get; }

            protected abstract TKeyInterface Key { get; }

            protected abstract string BuildKeyValueImplementation(TKeyInterface key);
        }
    }
}
