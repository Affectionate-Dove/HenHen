// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Worlds;
using NUnit.Framework;

namespace HenHen.Core.Tests.Worlds
{
    public class DateTimeTests
    {
        [TestCase(6, 0, 1)]
        [TestCase(19, 2, 3)]
        public void YearsTest(int months, int expectedYears, int expectedYear)
        {
            var dateTime = new DateTime
            {
                Months = months
            };
            Assert.AreEqual(expectedYears, dateTime.Years);
            // TODO:
            // Assert.AreEqual(expectedYear, dateTime.Year);
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

        [TestCase(3, 2, 3)]
        [TestCase(7, 3, 4)]
        [TestCase(8, 4, 1)]
        [TestCase(18, 9, 2)]
        public void SeasonsTest(int months, int expectedSeasons, int expectedSeason)
        {
            var dateTime = new DateTime
            {
                Months = months
            };
            Assert.AreEqual(expectedSeasons, dateTime.Seasons);
            Assert.AreEqual(expectedSeason, dateTime.Season);
        }

        [TestCase(15, 2, 3)]
        [TestCase(27, 4, 1)]
        [TestCase(51, 8, 1)]
        public void WeeksTest(int days, int expectedWeeks, int expectedWeek)
        {
            var dateTime = new DateTime
            {
                Days = days
            };
            Assert.AreEqual(expectedWeeks, dateTime.Weeks);
            Assert.AreEqual(expectedWeek, dateTime.Week);
        }

        [TestCase(6, 0, 1, 1)]
        [TestCase(25, 1, 2, 2)]
        [TestCase(150, 6, 7, 1)]
        public void DaysTest(int hours, int expectedDays, int expectedDayOfMonth, int expectedWeekDay)
        {
            var dateTime = new DateTime
            {
                Hours = hours
            };
            Assert.AreEqual(expectedDays, dateTime.Days);
            Assert.AreEqual(expectedDayOfMonth, dateTime.Day);
            Assert.AreEqual(expectedWeekDay, dateTime.WeekDay);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(70, 1, 1)]
        [TestCase(1442, 24, 0)]
        public void HoursTest(int minutes, int expectedHours, int expectedHour)
        {
            var dateTime = new DateTime
            {
                Minutes = minutes
            };
            Assert.AreEqual(expectedHours, dateTime.Hours);
            Assert.AreEqual(expectedHour, dateTime.Hour);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(70, 1, 1)]
        [TestCase(1442, 24, 0)]
        public void MinutesTest(int seconds, int expectedMinutes, int expectedMinute)
        {
            var dateTime = new DateTime
            {
                Seconds = seconds
            };
            Assert.AreEqual(expectedMinutes, dateTime.Minutes);
            Assert.AreEqual(expectedMinute, dateTime.Minute);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(59, 59)]
        [TestCase(60, 0)]
        [TestCase(61, 1)]
        public void SecondsTest(int seconds, int expectedSecond)
        {
            var dateTime = new DateTime { Seconds = seconds };
            Assert.AreEqual(dateTime.Second, expectedSecond);
        }

        [Test]
        public void SetAndGetTest()
        {
            var dateTime = new DateTime();
            IsGetSameAsSet(() => dateTime.Seconds, v => dateTime.Seconds = v);
            IsGetSameAsSet(() => dateTime.Minutes, v => dateTime.Minutes = v);
            IsGetSameAsSet(() => dateTime.Hours, v => dateTime.Hours = v);
            IsGetSameAsSet(() => dateTime.Days, v => dateTime.Days = v);
            IsGetSameAsSet(() => dateTime.Weeks, v => dateTime.Weeks = v);
            IsGetSameAsSet(() => dateTime.Months, v => dateTime.Months = v);
            IsGetSameAsSet(() => dateTime.Years, v => dateTime.Years = v);
        }

        private static void IsGetSameAsSet(System.Func<int> getter, System.Action<int> setter)
        {
            for (var i = 0; i < 200; i++)
            {
                setter(i);
                Assert.AreEqual(i, getter());
            }
        }
    }
}