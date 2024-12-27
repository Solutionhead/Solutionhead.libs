using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solutionhead.EntityKey
{
    public abstract class EntityKey<TKeyInterface> : EntityKeyBase.Of<TKeyInterface>
        where TKeyInterface : class
    {
        public override string GetParseFailMessage(string inputValue = null)
        {
            return string.Format("Invalid '{0}' key, received '{1}'", GetType().Name, inputValue);
        }

        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as TKeyInterface);
        }

        protected abstract bool Equals(TKeyInterface other);

        #region Protected Virtual Parts

        protected virtual bool TryParseInt(string s, out object result)
        {
            int intResult;
            var tryParse = int.TryParse(s, out intResult);
            result = intResult;
            return tryParse;
        }

        protected virtual bool TryParseDateTime(string s, out object result)
        {
            DateTime dateTimeResult;
            var tryParse = DateTime.TryParse(s, out dateTimeResult);
            result = dateTimeResult;
            return tryParse;
        }

        protected virtual bool TryParseString(string s, out object result)
        {
            result = s;
            return true;
        }

        protected virtual string IntToString(int i)
        {
            return i.ToString(CultureInfo.InvariantCulture);
        }

        protected virtual string DateTimeToString(DateTime d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        protected virtual string StringToString(string s)
        {
            return s.ToString(CultureInfo.InvariantCulture);
        }

        #endregion

        #region Protected Parts

        private EntityKey() { } 

        protected void ParseField<T>(string s, out T field)
        {
            object result;
            switch(GetSupportedType(typeof(T)))
            {
                case SupportedTypes.Int:
                    if(!TryParseInt(s, out result))
                    {
                        throw new FormatException();
                    }
                    break;

                case SupportedTypes.DateTime:
                    if(!TryParseDateTime(s, out result))
                    {
                        throw new FormatException();
                    }
                    break;

                case SupportedTypes.String:
                    if(!TryParseString(s, out result))
                    {
                        throw new FormatException();
                    }
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
            field = (T)result;
        }

        protected string FieldToString(object field)
        {
            switch(GetSupportedType(field.GetType()))
            {
                case SupportedTypes.Int: return IntToString((int)field);
                case SupportedTypes.DateTime: return DateTimeToString((DateTime)field);
                case SupportedTypes.String: return StringToString((string)field);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private static SupportedTypes GetSupportedType(Type type)
        {
            if(type == typeof(int))
            {
                return SupportedTypes.Int;
            }

            if(type == typeof(DateTime))
            {
                return SupportedTypes.DateTime;
            }

            if(type == typeof(string))
            {
                return SupportedTypes.String;
            }

            throw new NotSupportedException(string.Format("Type '{0}' is not supported.", type.Name));
        }

        #endregion

        public enum SupportedTypes
        {
            Int,
            DateTime,
            String
        }

        public abstract class With<T0> : EntityKey<TKeyInterface>
        {
            protected T0 Field0;

            protected With()
            {
                Field0 = default(T0);
            }

            protected With(TKeyInterface keyInterface)
            {
                var key = DeconstructKey(keyInterface);
                Field0 = key.Field0;
            }

            protected With(T0 field0)
            {
                Field0 = field0;
            }

            protected abstract TKeyInterface ConstructKey(T0 field0);

            protected abstract With<T0> DeconstructKey(TKeyInterface key);

            public override TKeyInterface Default
            {
                get { return ConstructKey(default(T0)); }
            }

            protected override TKeyInterface Key
            {
                get { return ConstructKey(Field0); }
            }

            protected override string BuildKeyValueImplementation(TKeyInterface key)
            {
                if(key == null) { throw new ArgumentNullException("key"); }
                var deconstructedKey = DeconstructKey(key);
                return string.Format("{0}", FieldToString(deconstructedKey.Field0));
            }

            protected override TKeyInterface ParseImplementation(string keyValue)
            {
                T0 field0;
                ParseField(keyValue, out field0);

                return ConstructKey(field0);
            }

            #region Equality Overrides.

            protected override bool Equals(TKeyInterface other)
            {
                if(other == null) { return false; }
                var key = DeconstructKey(other);
                return Field0.Equals(key.Field0);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Field0.GetHashCode();
                    return hashCode;
                }
            }

            #endregion
        }

        public abstract class With<T0, T1> : EntityKey<TKeyInterface>
        {
            protected virtual string Separator { get { return "-"; } }

            protected T0 Field0;
            protected T1 Field1;

            protected With()
            {
                Field0 = default(T0);
                Field1 = default(T1);
            }

            protected With(TKeyInterface keyInterface)
            {
                var key = DeconstructKey(keyInterface);
                Field0 = key.Field0;
                Field1 = key.Field1;
            }

            protected With(T0 field0, T1 field1)
            {
                Field0 = field0;
                Field1 = field1;
            }

            protected abstract TKeyInterface ConstructKey(T0 field0, T1 field1);

            protected abstract With<T0, T1> DeconstructKey(TKeyInterface key);

            public override TKeyInterface Default
            {
                get { return ConstructKey(default(T0), default(T1)); }
            }

            protected override TKeyInterface Key
            {
                get { return ConstructKey(Field0, Field1); }
            }

            protected override string BuildKeyValueImplementation(TKeyInterface key)
            {
                if(key == null) { throw new ArgumentNullException("key"); }
                var deconstructedKey = DeconstructKey(key);
                return string.Format("{1}{0}{2}", Separator, FieldToString(deconstructedKey.Field0), FieldToString(deconstructedKey.Field1));
            }

            protected override TKeyInterface ParseImplementation(string keyValue)
            {
                var split = Regex.Split(keyValue, Separator).ToList();
                if(split.Count != 2)
                {
                    throw new FormatException();
                }

                T0 field0;
                ParseField(split[0], out field0);

                T1 field1;
                ParseField(split[1], out field1);

                return ConstructKey(field0, field1);
            }

            #region Equality Overrides.

            protected override bool Equals(TKeyInterface other)
            {
                if(other == null) { return false; }
                var key = DeconstructKey(other);
                return Field0.Equals(key.Field0) && Field1.Equals(key.Field1);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Field0.GetHashCode();
                    hashCode = (hashCode * 397) ^ Field1.GetHashCode();
                    return hashCode;
                }
            }

            #endregion
        }

        public abstract class With<T0, T1, T2> : EntityKey<TKeyInterface>
        {
            protected virtual string Separator { get { return "-"; } }

            protected T0 Field0;
            protected T1 Field1;
            protected T2 Field2;

            protected With()
            {
                Field0 = default(T0);
                Field1 = default(T1);
                Field2 = default(T2);
            }

            protected With(TKeyInterface keyInterface)
            {
                var key = DeconstructKey(keyInterface);
                Field0 = key.Field0;
                Field1 = key.Field1;
                Field2 = key.Field2;
            }

            protected With(T0 field0, T1 field1, T2 field2)
            {
                Field0 = field0;
                Field1 = field1;
                Field2 = field2;
            }

            protected abstract TKeyInterface ConstructKey(T0 field0, T1 field1, T2 field2);

            protected abstract With<T0, T1, T2> DeconstructKey(TKeyInterface key);

            public override TKeyInterface Default
            {
                get { return ConstructKey(default(T0), default(T1), default(T2)); }
            }

            protected override TKeyInterface Key
            {
                get { return ConstructKey(Field0, Field1, Field2); }
            }

            protected override string BuildKeyValueImplementation(TKeyInterface key)
            {
                if(key == null) { throw new ArgumentNullException("key"); }
                var deconstructedKey = DeconstructKey(key);
                return string.Format("{1}{0}{2}{0}{3}", Separator, FieldToString(deconstructedKey.Field0), FieldToString(deconstructedKey.Field1), FieldToString(deconstructedKey.Field2));
            }

            protected override TKeyInterface ParseImplementation(string keyValue)
            {
                var split = Regex.Split(keyValue, Separator).ToList();
                if(split.Count != 3)
                {
                    throw new FormatException();
                }

                T0 field0;
                ParseField(split[0], out field0);

                T1 field1;
                ParseField(split[1], out field1);

                T2 field2;
                ParseField(split[2], out field2);

                return ConstructKey(field0, field1, field2);
            }

            #region Equality Overrides.

            protected override bool Equals(TKeyInterface other)
            {
                if(other == null) { return false; }
                var key = DeconstructKey(other);
                return Field0.Equals(key.Field0) && Field1.Equals(key.Field1) && Field2.Equals(key.Field2);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Field0.GetHashCode();
                    hashCode = (hashCode * 397) ^ Field1.GetHashCode();
                    hashCode = (hashCode * 397) ^ Field2.GetHashCode();
                    return hashCode;
                }
            }

            #endregion
        }
    }
}
