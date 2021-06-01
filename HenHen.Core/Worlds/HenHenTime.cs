// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;

namespace HenHen.Core.Worlds
{
    public readonly struct HenHenTime
    {
        public const double season_at_year_start = 0.5;
        public const int SEASONS_IN_YEAR = 4;
        public const int MONTHS_IN_SEASON = 2;
        public const int MONTHS_IN_YEAR = 8;
        public const int WEEKS_IN_MONTH = 4;
        public const int DAYS_IN_WEEK = 6;
        public const int DAYS_IN_MONTH = DAYS_IN_WEEK * WEEKS_IN_MONTH;
        public const int HOURS_IN_DAY = 24;
        public const int MINUTES_IN_HOUR = 60;
        public const int SECONDS_IN_MINUTE = 60;
        private const int ticks_in_second = 1000;
        private const long ticks_in_minute = ticks_in_second * SECONDS_IN_MINUTE;
        private const long ticks_in_hour = ticks_in_minute * MINUTES_IN_HOUR;
        private const long ticks_in_day = ticks_in_hour * HOURS_IN_DAY;
        private const long ticks_in_week = ticks_in_day * DAYS_IN_WEEK;
        private const long ticks_in_month = ticks_in_day * DAYS_IN_MONTH;
        private const long ticks_in_year = ticks_in_month * MONTHS_IN_YEAR;
        private readonly long ticks;

        public double Years => GetFromBeginning(Months, MONTHS_IN_YEAR);

        public int Year => (int)(Years + 1);

        public double Months => GetFromBeginning(Weeks, WEEKS_IN_MONTH);

        public int Month => GetInSurroundingUnit(Months, MONTHS_IN_YEAR, 1);

        public int Season
        {
            get
            {
                var seasons = Months / MONTHS_IN_SEASON;
                var offsetSeasons = seasons + season_at_year_start;
                return GetInSurroundingUnit(offsetSeasons, SEASONS_IN_YEAR, 1);
            }
        }

        public double Weeks => GetFromBeginning(Days, DAYS_IN_WEEK);

        public int Week => GetInSurroundingUnit(Weeks, WEEKS_IN_MONTH, 1);

        public double Days => GetFromBeginning(Hours, HOURS_IN_DAY);

        /// <summary>
        /// Day of month.
        /// </summary>
        public int Day => GetInSurroundingUnit(Days, DAYS_IN_MONTH, 1);

        public int WeekDay => GetInSurroundingUnit(Days, DAYS_IN_WEEK, 1);

        public double Hours => GetFromBeginning(Minutes, MINUTES_IN_HOUR);

        public int Hour => GetInSurroundingUnit(Hours, HOURS_IN_DAY, 0);

        public double Minutes => GetFromBeginning(Seconds, SECONDS_IN_MINUTE);

        public int Minute => GetInSurroundingUnit(Minutes, MINUTES_IN_HOUR, 0);

        public double Seconds => GetFromBeginning(ticks, ticks_in_second);

        public int Second => GetInSurroundingUnit(Seconds, SECONDS_IN_MINUTE, 0);

        public HenHenTime(int year, int month, int day)
        {
            if (year is <= 0)
                throw new ArgumentOutOfRangeException(nameof(year), "Cannot be lower than or equal to 0.");
            if (month is <= 0 or > MONTHS_IN_YEAR)
                throw new ArgumentOutOfRangeException(nameof(month), $"Must be higher than 0 and lower than the amount of months in year ({MONTHS_IN_YEAR}).");
            if (day is <= 0 or > DAYS_IN_MONTH)
                throw new ArgumentOutOfRangeException(nameof(day), $"Must be higher than 0 and lower than the amount of days in a month ({DAYS_IN_MONTH}).");
            ticks = ((year - 1) * ticks_in_year) + ((month - 1) * ticks_in_month) + ((day - 1) * ticks_in_day);
        }

        public HenHenTime(int year, int month, int day, int hour, int minute, int second) : this(year, month, day)
        {
            if (hour is < 0 or >= HOURS_IN_DAY)
                throw new ArgumentOutOfRangeException(nameof(hour), $"Must be higher than or equal to 0 and lower than the amount of hours in a day ({HOURS_IN_DAY}).");
            if (minute is < 0 or >= MINUTES_IN_HOUR)
                throw new ArgumentOutOfRangeException(nameof(minute), $"Must be higher than or equal to 0 and lower than the amount of minutes in an hour ({MINUTES_IN_HOUR}).");
            if (second is < 0 or >= SECONDS_IN_MINUTE)
                throw new ArgumentOutOfRangeException(nameof(second), $"Must be higher than or equal to 0 and lower than the amount of seconds in a minute ({SECONDS_IN_MINUTE}).");
            ticks += (hour * ticks_in_hour) + (minute * ticks_in_minute) + (second * ticks_in_second);
        }

        private HenHenTime(long ticks) => this.ticks = ticks;

        public static HenHenTime FromYears(double years) => new(checked((long)(years * ticks_in_year)));

        public static HenHenTime FromMonths(double months) => new(checked((long)(months * ticks_in_month)));

        public static HenHenTime FromDays(double days) => new(checked((long)(days * ticks_in_day)));

        public static HenHenTime FromWeeks(double weeks) => new(checked((long)(weeks * ticks_in_week)));

        public static HenHenTime FromHours(double hours) => new(checked((long)(hours * ticks_in_hour)));

        public static HenHenTime FromMinutes(double minutes) => new(checked((long)(minutes * ticks_in_minute)));

        public static HenHenTime FromSeconds(double seconds) => new(checked((long)(seconds * ticks_in_second)));

        private static int GetInSurroundingUnit(double unit, int surroundingUnit, int numericBase) => checked(((int)unit % surroundingUnit) + numericBase);

        private static double GetFromBeginning(double unit, int surroundingUnit) => checked(unit / surroundingUnit);
    }
}