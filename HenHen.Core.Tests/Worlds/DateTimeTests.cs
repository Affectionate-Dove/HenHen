// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Worlds;
using NUnit.Framework;

namespace HenHen.Core.Tests.Worlds
{
    public class DateTimeTests
    {
        [TestCase(6, 0.75, 1)]
        [TestCase(20, 2.5, 3)]
        public void YearsTest(double months, double expectedYears, int expectedYear)
        {
            var dateTime = new HenHenTime
            {
                Months = months
            };
            Assert.AreEqual(expectedYears, dateTime.Years);
            // TODO:
            // Assert.AreEqual(expectedYear, dateTime.Year);
        }

        [TestCase(7, 1.75, 2)]
        [TestCase(4, 1, 2)]
        [TestCase(16, 4, 5)]
        [TestCase(1, 0.25, 1)]
        public void MonthsTest(double weeks, double expectedMonths, int expectedMonth)
        {
            var dateTime = new HenHenTime
            {
                Weeks = weeks
            };
            Assert.AreEqual(expectedMonths, dateTime.Months);
            Assert.AreEqual(expectedMonth, dateTime.Month);
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(7, 1)]
        [TestCase(8, 1)]
        [TestCase(18, 2)]
        [TestCase(23, 1)]
        [TestCase(24, 1)]
        [TestCase(25, 2)]
        [TestCase(26, 2)]
        [TestCase(27, 3)]
        public void SeasonTest(double months, int expectedSeason)
        {
            var dateTime = new HenHenTime
            {
                Months = months
            };
            Assert.AreEqual(expectedSeason, dateTime.Season);
        }

        [TestCase(15, 2.5, 3)]
        [TestCase(27, 4.5, 1)]
        [TestCase(51, 8.5, 1)]
        public void WeeksTest(double days, double expectedWeeks, int expectedWeek)
        {
            var dateTime = new HenHenTime
            {
                Days = days
            };
            Assert.AreEqual(expectedWeeks, dateTime.Weeks);
            Assert.AreEqual(expectedWeek, dateTime.Week);
        }

        [TestCase(0, 0, 1, 1)]
        [TestCase(6, 0.25, 1, 1)]
        [TestCase(36, 1.5, 2, 2)]
        [TestCase(144, 6, 7, 1)]
        [TestCase(150, 6.25, 7, 1)]
        [TestCase(570, 23.75, 24, 6)]
        [TestCase(576, 24, 1, 1)]
        public void DaysTest(double hours, double expectedDays, int expectedDayOfMonth, int expectedWeekDay)
        {
            var dateTime = new HenHenTime
            {
                Hours = hours
            };
            Assert.AreEqual(expectedDays, dateTime.Days);
            Assert.AreEqual(expectedDayOfMonth, dateTime.Day);
            Assert.AreEqual(expectedWeekDay, dateTime.WeekDay);
        }

        [TestCase(0, 0, 0)]
        [TestCase(30, 0.5, 0)]
        [TestCase(45, 0.75, 0)]
        [TestCase(90, 1.5, 1)]
        [TestCase(1410, 23.5, 23)]
        [TestCase(1470, 24.5, 0)]
        public void HoursTest(double minutes, double expectedHours, int expectedHour)
        {
            var dateTime = new HenHenTime
            {
                Minutes = minutes
            };
            Assert.AreEqual(expectedHours, dateTime.Hours);
            Assert.AreEqual(expectedHour, dateTime.Hour);
        }

        [TestCase(0, 0, 0)]
        [TestCase(30, 0.5, 0)]
        [TestCase(45, 0.75, 0)]
        [TestCase(90, 1.5, 1)]
        [TestCase(1410, 23.5, 23)]
        [TestCase(1470, 24.5, 24)]
        public void MinutesTest(double seconds, double expectedMinutes, int expectedMinute)
        {
            var dateTime = new HenHenTime
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
        public void SecondsTest(double seconds, int expectedSecond)
        {
            var dateTime = new HenHenTime { Seconds = seconds };
            Assert.AreEqual(dateTime.Second, expectedSecond);
        }

        [Test]
        public void SetAndGetTest()
        {
            var dateTime = new HenHenTime();
            IsGetSameAsSet(() => dateTime.Seconds, v => dateTime.Seconds = v);
            IsGetSameAsSet(() => dateTime.Minutes, v => dateTime.Minutes = v);
            IsGetSameAsSet(() => dateTime.Hours, v => dateTime.Hours = v);
            IsGetSameAsSet(() => dateTime.Days, v => dateTime.Days = v);
            IsGetSameAsSet(() => dateTime.Weeks, v => dateTime.Weeks = v);
            IsGetSameAsSet(() => dateTime.Months, v => dateTime.Months = v);
            IsGetSameAsSet(() => dateTime.Years, v => dateTime.Years = v);
        }

        [TestCase(1, 4, 8)]
        [TestCase(3, 7, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(8, 8, 3)]
        public void YearMonthDayConstructorTest(int year, int month, int day)
        {
            var dateTime = new HenHenTime(year, month, day);
            Assert.AreEqual(year, dateTime.Year);
            Assert.AreEqual(month, dateTime.Month);
            Assert.AreEqual(day, dateTime.Day);
        }

        [TestCase(1, 4, 8, 0, 0, 0)]
        [TestCase(3, 7, 1, 10, 0, 0)]
        [TestCase(1, 1, 1, 0, 2, 0)]
        [TestCase(8, 8, 3, 0, 0, 5)]
        [TestCase(1, 4, 8, 1, 2, 3)]
        [TestCase(3, 7, 1, 4, 5, 6)]
        public void YearToSecondConstructorTest(int year, int month, int day, int hour, int minute, int second)
        {
            var dateTime = new HenHenTime(year, month, day, hour, minute, second);
            Assert.AreEqual(year, dateTime.Year);
            Assert.AreEqual(month, dateTime.Month);
            Assert.AreEqual(day, dateTime.Day);
        }

        private static void IsGetSameAsSet(System.Func<double> getter, System.Action<double> setter)
        {
            for (var i = 0; i < 200; i++)
            {
                setter(i);
                Assert.AreEqual(i, getter());
            }
        }
    }
}