using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutionhead.Extensions
{
    public static class DateTimeExtensions
    {
        public static int DifferenceInDays(this DateTime date, DateTime dateToCompare)
        {
            var dayOfYear1 = dateToCompare.DayOfYear;
            var dayOfYear2 = date.DayOfYear;

            if (date.Year.CompareTo(dateToCompare.Year) != 0)
            {
                var yearOffset = dateToCompare.Year - date.Year;
                dayOfYear1 += yearOffset * 365;
            }

            return Math.Abs(dayOfYear1 - dayOfYear2);
        }
    }
}
