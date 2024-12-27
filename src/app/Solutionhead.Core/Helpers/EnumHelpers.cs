using System;
using System.Linq;

namespace Solutionhead.Extensions
{
    public static class EnumHelpers
    {
        public static TEnum GetMaxValue<TEnum>() where TEnum : IComparable, IFormattable, IConvertible
        {
            Type type = typeof(TEnum);

            if (!type.IsSubclassOf(typeof(Enum)))
            {
                throw new InvalidCastException("Cannot cast '" + type.FullName + "' to System.Enum.");
            }

            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Max();
        }

        public static TEnum GetMinValue<TEnum>() where TEnum : IComparable, IFormattable, IConvertible
        {
            Type type = typeof(TEnum);

            if (!type.IsSubclassOf(typeof(Enum)))
            {
                throw new InvalidCastException("Cannot cast '" + type.FullName + "' to System.Enum.");
            }

            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Min();
        }
    }
}
