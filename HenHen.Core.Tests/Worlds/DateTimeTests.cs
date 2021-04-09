// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Worlds;
using NUnit.Framework;

namespace HenHen.Core.Tests.Worlds
{
    public class DateTimeTests
    {
        [TestCase(6, 0)]
        [TestCase(19, 2)]
        public void YearsTest(int months, int expectedYears)
        {
            var dateTime = new DateTime
            {
                Months = months
            };
            Assert.AreEqual(expectedYears, dateTime.Years);
        }

        [TestCase(7, 1, 2)]
        [TestCase(16, 4, 4)]
        [TestCase(1, 0, 1)]
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

        [TestCase(3, 3)]
        [TestCase(15, 15)]
        [TestCase(78, 78)]
        public void SecondsTest(int seconds, int expectedSeconds)
        {
            var dateTime = new DateTime
            {
                Seconds = seconds
            };
            Assert.AreEqual(expectedSeconds, dateTime.Seconds);
        }

        [TestCase(0, 0)]
        [TestCase(30, 30)]
        [TestCase(68, 68)]
        [TestCase(131, 131)]
        public void MinutesTest(int minutes, int expectedMinutes)
        {
            var dateTime = new DateTime
            {
                Minutes = minutes
            };
            Assert.AreEqual(expectedMinutes, dateTime.Minutes);
        }

        [Test]
        public void HoursTest()
        {
            var dateTime = new DateTime();
            for (var i = 0; i < 130; i++)
            {
                dateTime.Hours = i;
                Assert.AreEqual(i, dateTime.Hours);
            }
        }

        [Test]
        public void DaysTest()
        {
            var dateTime = new DateTime();
            for (var i = 0; i < 130; i++)
            {
                dateTime.Days = i;
                Assert.AreEqual(i, dateTime.Days);
            }
        }

        [TestCase(6, 0)]
        [TestCase(121, 2)]
        public void SecondsTest2(int seconds, int expectedMinutes)
        {
            var dateTime = new DateTime
            {
                Seconds = seconds
            };
            Assert.AreEqual(expectedMinutes, dateTime.Minutes);
        }

        [TestCase(6, 1)]
        [TestCase(19, 3)]
        public void DaysTest2(int days, int expectedWeeks)
        {
            var dateTime = new DateTime
            {
                Days = days
            };
            Assert.AreEqual(expectedWeeks, dateTime.Weeks);
        }
    }
}