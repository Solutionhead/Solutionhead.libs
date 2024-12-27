using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutionhead.Core
{
    public class DateRange : IEquatable<DateRange>
    {
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public DateTime StartDate { get { return _startDate; } }
        public DateTime EndDate { get { return _endDate; } }

        public DateRange(DateTime date1, DateTime date2)
        {
            if(date1.CompareTo(date2) > 0)
            {
                _startDate = date2;
                _endDate = date1;
            }
            else
            {
                _startDate = date1;
                _endDate = date2;
            }
        }

        public bool ContainsDate(DateTime date, bool excludeBoundaries = false)
        {
            if (excludeBoundaries)
            {
                return _startDate < date && date < _endDate;
            }

            return _startDate <= date && date <= _endDate;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DateRange)) return false;
            return Equals((DateRange) obj);
        }

        public bool Equals(DateRange other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._startDate.Equals(_startDate) && other._endDate.Equals(_endDate);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_startDate.GetHashCode()*397) ^ _endDate.GetHashCode();
            }
        }
    }
}
