// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Worlds;
using NUnit.Framework;

namespace HenHen.Core.Tests.Worlds
{
    public class DateTimeTests
    {
        [TestCase(6, 1)]
        [TestCase(19, 3)]
        public void YearsTest(int months, int expected)
        {
            var dateTime = new DateTime
            {
                Months = months
            };
            Assert.AreEqual(expected, dateTime.Years);
        }

        [TestCase(7, 2, 2)]
        [TestCase(16, 4, 4)]
        [TestCase(1, 1, 1)]
        public void MonthsTest(int weeks, int expectedMonths, int expectedMonth)
        {
            var dateTime = new DateTime
            {
                Weeks = weeks
            };
            Assert.AreEqual(expectedMonths, dateTime.Months);
            Assert.AreEqual(expectedMonth, dateTime.Month);
        }

        [TestCase(3, 2, 2)]
        [TestCase(7, 4, 4)]
        [TestCase(18, 9, 1)]
        public void SeasonsTest(int months, int expectedSeasons, int expectedSeason)
        {
            var dateTime = new DateTime
            {
                Months = months
            };
            Assert.AreEqual(expectedSeasons, dateTime.Seasons);
            Assert.AreEqual(expectedSeason, dateTime.Season);
        }

        [TestCase(15, 3, 3)]
        [TestCase(27, 5, 1)]
        [TestCase(51, 8, 4)]
        public void WeeksTest(int days, int expectedWeeks, int expectedWeek)
        {
            var dateTime = new DateTime
            {
                Days = days
            };
            Assert.AreEqual(expectedWeeks, dateTime.Weeks);
            Assert.AreEqual(expectedWeek, dateTime.Week);
        }
    }
}