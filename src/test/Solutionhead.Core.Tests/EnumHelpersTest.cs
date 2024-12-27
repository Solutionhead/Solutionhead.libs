using NUnit.Framework;
using System;
using Solutionhead.Extensions;

namespace Solutionhead.Core.Tests
{
    [TestFixture]
    public class EnumHelpersTest
    {
        #region test objects

        private enum OrderedEnum
        {
            Zero = 0,
            One = 1,
            Two = 2
        }
        
        private enum OutOfOrderEnum
        {
            One = 1,
            Zero = 0,
            Two = 2
        }

        private enum NegativeEnum
        {
            Less = -1,
            Equal = 0,
            Greater = 1
        }

        private class FalseEnumClass : IComparable, IFormattable, IConvertible
        {
            #region IConvertible Members

            public TypeCode GetTypeCode()
            {
                throw new NotImplementedException();
            }

            public bool ToBoolean(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public byte ToByte(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public char ToChar(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public DateTime ToDateTime(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public decimal ToDecimal(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public double ToDouble(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public short ToInt16(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public int ToInt32(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public long ToInt64(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public sbyte ToSByte(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public float ToSingle(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public string ToString(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public object ToType(Type conversionType, IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public ushort ToUInt16(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public uint ToUInt32(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            public ulong ToUInt64(IFormatProvider provider)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IFormattable Members

            public string ToString(string format, IFormatProvider formatProvider)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IComparable Members

            public int CompareTo(object obj)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion

        [Test]
        public void GetMaxValueTest()
        {
            var actual = EnumHelpers.GetMaxValue<OrderedEnum>();
            Assert.AreEqual(OrderedEnum.Two, actual);
        }

        [Test]
        public void GetMinValueTest()
        {
            var actual = EnumHelpers.GetMinValue<OrderedEnum>();
            Assert.AreEqual(OrderedEnum.Zero, actual);
        }

        [Test]
        public void GetMaxValueFromOutOfOrderEnum()
        {
            var actual = EnumHelpers.GetMaxValue<OutOfOrderEnum>();
            Assert.AreEqual(OutOfOrderEnum.Two, actual);
        }
        
        [Test]
        public void GetMinValueFromOutOfOrderEnum()
        {
            var actual = EnumHelpers.GetMinValue<OutOfOrderEnum>();
            Assert.AreEqual(OutOfOrderEnum.Zero, actual);
        }
        
        [Test]
        public void GetMaxValueFromNegativeEnum()
        {
            var actual = EnumHelpers.GetMaxValue<NegativeEnum>();
            Assert.AreEqual(NegativeEnum.Greater, actual);
        }

        [Test]
        public void GetMinValueFromNegativeEnum()
        {
            var actual = EnumHelpers.GetMinValue<NegativeEnum>();
            Assert.AreEqual(NegativeEnum.Less, actual);
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetMinValueThrowsExceptionIfTypeDoesNotInheritFromEnum()
        {
            EnumHelpers.GetMinValue<FalseEnumClass>();
        }
    }
}
