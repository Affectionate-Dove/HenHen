// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

namespace HenHen.Core.Worlds
{
    public struct DateTime
    {
        public int ticks;
        private const int months_in_year = 8;
        private const int weeks_in_month = 4;
        private const int days_in_week = 6;
        private const int hours_in_day = 24;
        private const int minutes_in_hour = 60;
        private const int seconds_in_minute = 60;
        private const int ticks_in_second = 60;

        public int Years
        {
            get => Get(Months, months_in_year);
            set => Months = Set(value, months_in_year);
        }

        public int Months
        {
            get => Get(Weeks, weeks_in_month);
            set => Weeks = Set(value, weeks_in_month);
        }

        public int Month => Months % months_in_year;

        public int Seasons => (Months + 1) / 2;
        public int Season => Seasons % 4;

        public int Weeks
        {
            get => Get(Days, days_in_week);
            set => Days = Set(value, days_in_week);
        }

        public int Week => Weeks % weeks_in_month;

        public int Days
        {
            get => Get(Hours, hours_in_day);
            set => Hours = Set(value, hours_in_day);
        }

        /// <summary>
        /// Day in month.
        /// </summary>
        public int Day => Days % (days_in_week * weeks_in_month);

        public int WeekDay => Days % days_in_week;

        public int Hours
        {
            get => Get(Minutes, minutes_in_hour);
            set => Minutes = Set(value, minutes_in_hour);
        }

        public int Hour => Hours % hours_in_day;

        public int Minutes
        {
            get => Get(Seconds, seconds_in_minute);
            set => Seconds = Set(value, seconds_in_minute);
        }

        public int Minute => Minutes % minutes_in_hour;

        public int Seconds
        {
            get => Get(ticks, ticks_in_second);
            set => ticks = Set(value, ticks_in_second);
        }

        public int Second => Seconds % seconds_in_minute;

        private static int Get(int unit, int surroundingUnit) => unit / surroundingUnit;

        private static int Set(int unit, int surroundingUnit) => checked(unit * surroundingUnit);
    }
}