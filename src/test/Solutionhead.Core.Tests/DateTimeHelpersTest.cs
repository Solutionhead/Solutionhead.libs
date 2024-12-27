using System;
using NUnit.Framework;
using Solutionhead.Extensions;

namespace Solutionhead.Core.Tests
{
    [TestFixture]
    public class DateTimeHelpersTest
    {
        #region DifferenceInDays tests

        [Test]
        public void ComparingTodayToTomorrowReturnsOne()
        {
            var date = DateTime.Today; 
            var dateToCompare = DateTime.Today.AddDays(1);
            const int expected = 1;

            var actual = date.DifferenceInDays(dateToCompare);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ComparingTodayToYesterdayReturnsOne()
        {
            var date = DateTime.Today;
            var dateToCompare = DateTime.Today.AddDays(-1);
            const int expected = 1;

            var actual = date.DifferenceInDays(dateToCompare);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ComparingDecember31ToJanuary1OfNextYearReturnsOne()
        {
            var date = new DateTime(2010, 12, 31);
            var dateToCompare = new DateTime(2011, 1, 1);
            const int expected = 1;

            var actual = date.DifferenceInDays(dateToCompare);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ComparingDecember1ToJanuary1OfNextYearReturns31()
        {
            var date = new DateTime(2010, 12, 1);
            var dateToCompare = new DateTime(2011, 1, 1);
            const int expected = 31;

            var actual = date.DifferenceInDays(dateToCompare);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ComparingDecember1ToJanuary2OfNextYearReturns32()
        {
            var date = new DateTime(2010, 12, 1);
            var dateToCompare = new DateTime(2011, 1, 2);
            const int expected = 32;

            var actual = date.DifferenceInDays(dateToCompare);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ComparingFirstDayOfYearToLastDayOfPreviousYearReturnsOne()
        {
            var date = new DateTime(2011, 1, 1);
            var dateToCompare = new DateTime(2010, 12, 31);
            const int expected = 1;

            var actual = date.DifferenceInDays(dateToCompare);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ComparingJanuary1ToDecember1OfPrevYearReturns31()
        {
            var date = new DateTime(2011, 1, 1);
            var dateToCompare = new DateTime(2010, 12, 1);
            const int expected = 31;

            var actual = date.DifferenceInDays(dateToCompare);

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
