// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using HenHen.Core.Worlds;
using NUnit.Framework;
using System;

namespace HenHen.Core.Tests.Worlds
{
    public class DateTimeTests
    {
        [TestCase(6, 0.75, 1)]
        [TestCase(20, 2.5, 3)]
        public void YearsTest(double months, double expectedYears, int expectedYear)
        {
            var dateTime = HenHenTime.FromMonths(months);
            Assert.AreEqual(expectedYears, dateTime.Years);
            Assert.AreEqual(expectedYear, dateTime.Year);
        }

        [TestCase(7, 1.75, 2)]
        [TestCase(4, 1, 2)]
        [TestCase(16, 4, 5)]
        [TestCase(1, 0.25, 1)]
        public void MonthsTest(double weeks, double expectedMonths, int expectedMonth)
        {
            var dateTime = HenHenTime.FromWeeks(weeks);
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
            var dateTime = HenHenTime.FromMonths(months);
            Assert.AreEqual(expectedSeason, dateTime.Season);
        }

        [TestCase(15, 2.5, 3)]
        [TestCase(27, 4.5, 1)]
        [TestCase(51, 8.5, 1)]
        public void WeeksTest(double days, double expectedWeeks, int expectedWeek)
        {
            var dateTime = HenHenTime.FromDays(days);
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
            var dateTime = HenHenTime.FromHours(hours);
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
            var dateTime = HenHenTime.FromMinutes(minutes);
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
            var dateTime = HenHenTime.FromSeconds(seconds);
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
            var dateTime = HenHenTime.FromSeconds(seconds);
            Assert.AreEqual(dateTime.Second, expectedSecond);
        }

        [Test]
        public void SetAndGetTest()
        {
            IsGetSameAsStaticCtor(dateTime => dateTime.Seconds, HenHenTime.FromSeconds);
            IsGetSameAsStaticCtor(dateTime => dateTime.Minutes, HenHenTime.FromMinutes);
            IsGetSameAsStaticCtor(dateTime => dateTime.Hours, HenHenTime.FromHours);
            IsGetSameAsStaticCtor(dateTime => dateTime.Days, HenHenTime.FromDays);
            IsGetSameAsStaticCtor(dateTime => dateTime.Weeks, HenHenTime.FromWeeks);
            IsGetSameAsStaticCtor(dateTime => dateTime.Months, HenHenTime.FromMonths);
            IsGetSameAsStaticCtor(dateTime => dateTime.Years, HenHenTime.FromYears);
        }

        [TestCase(1, 4, 8)]
        [TestCase(3, 7, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(8, 8, 3)]
        [TestCase(0, 8, 3, true)]
        [TestCase(1, -2, 3, true)]
        public void YearMonthDayConstructorTest(int year, int month, int day, bool shouldThrow = false)
        {
            HenHenTime dateTime;
            try
            {
                dateTime = new HenHenTime(year, month, day);
                Assert.IsFalse(shouldThrow);
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(shouldThrow);
                return;
            }
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
        [TestCase(3, 7, 1, 4, 5, HenHenTime.SECONDS_IN_MINUTE, true)]
        public void YearToSecondConstructorTest(int year, int month, int day, int hour, int minute, int second, bool shouldThrow = false)
        {
            HenHenTime dateTime;
            try
            {
                dateTime = new HenHenTime(year, month, day, hour, minute, second);
                Assert.IsFalse(shouldThrow);
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(shouldThrow);
                return;
            }
            Assert.AreEqual(year, dateTime.Year);
            Assert.AreEqual(month, dateTime.Month);
            Assert.AreEqual(day, dateTime.Day);
            Assert.AreEqual(hour, dateTime.Hour);
            Assert.AreEqual(minute, dateTime.Minute);
            Assert.AreEqual(second, dateTime.Second);
        }

        private static void IsGetSameAsStaticCtor(Func<HenHenTime, double> getter, Func<double, HenHenTime> setter)
        {
            for (var i = 0; i < 200; i++)
            {
                var henHenTime = setter(i);
                Assert.AreEqual(i, getter(henHenTime));
            }
        }
    }
}