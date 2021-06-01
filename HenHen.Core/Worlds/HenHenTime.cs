// Copyright (c) Affectionate Dove <contact@affectionatedove.com>.
// Licensed under the Affectionate Dove Limited Code Viewing License.
// See the LICENSE file in the repository root for full license text.

using System;

namespace HenHen.Core.Worlds
{
    public struct HenHenTime
    {
        private const double season_at_year_start = 0.5;
        private const int seasons_in_year = 4;
        private const int months_in_season = 2;
        private const int months_in_year = 8;
        private const int weeks_in_month = 4;
        private const int days_in_week = 6;
        private const int hours_in_day = 24;
        private const int minutes_in_hour = 60;
        private const int seconds_in_minute = 60;
        private const int ticks_in_second = 1000;
        private const long ticks_in_minute = ticks_in_second * seconds_in_minute;
        private const long ticks_in_hour = ticks_in_minute * minutes_in_hour;
        private const long ticks_in_day = ticks_in_hour * hours_in_day;
        private const long ticks_in_month = ticks_in_day * days_in_week * weeks_in_month;
        private const long ticks_in_year = ticks_in_month * months_in_year;
        private long ticks;

        public double Years
        {
            get => GetFromBeginning(Months, months_in_year);
            set => Months = SetFromBeginning(value, months_in_year);
        }

        public int Year => (int)(Years + 1);

        public double Months
        {
            get => GetFromBeginning(Weeks, weeks_in_month);
            set => Weeks = SetFromBeginning(value, weeks_in_month);
        }

        public int Month => GetInSurroundingUnit(Months, months_in_year, 1);

        public int Season
        {
            get
            {
                var seasons = Months / months_in_season;
                var offsetSeasons = seasons + season_at_year_start;
                return GetInSurroundingUnit(offsetSeasons, seasons_in_year, 1);
            }
        }

        public double Weeks
        {
            get => GetFromBeginning(Days, days_in_week);
            set => Days = SetFromBeginning(value, days_in_week);
        }

        public int Week => GetInSurroundingUnit(Weeks, weeks_in_month, 1);

        public double Days
        {
            get => GetFromBeginning(Hours, hours_in_day);
            set => Hours = SetFromBeginning(value, hours_in_day);
        }

        /// <summary>
        /// Day of month.
        /// </summary>
        public int Day => GetInSurroundingUnit(Days, days_in_week * weeks_in_month, 1);

        public int WeekDay => GetInSurroundingUnit(Days, days_in_week, 1);

        public double Hours
        {
            get => GetFromBeginning(Minutes, minutes_in_hour);
            set => Minutes = SetFromBeginning(value, minutes_in_hour);
        }

        public int Hour => GetInSurroundingUnit(Hours, hours_in_day, 0);

        public double Minutes
        {
            get => GetFromBeginning(Seconds, seconds_in_minute);
            set => Seconds = SetFromBeginning(value, seconds_in_minute);
        }

        public int Minute => GetInSurroundingUnit(Minutes, minutes_in_hour, 0);

        public double Seconds
        {
            get => GetFromBeginning(ticks, ticks_in_second);
            set => ticks = checked((long)Math.Round(SetFromBeginning(value, ticks_in_second)));
        }

        public int Second => GetInSurroundingUnit(Seconds, seconds_in_minute, 0);

        public HenHenTime(int year, int month, int day) => ticks = ((year - 1) * ticks_in_year) + ((month - 1) * ticks_in_month) + ((day - 1) * ticks_in_day);

        public HenHenTime(int year, int month, int day, int hour, int minute, int second) : this(year, month, day) => ticks += (hour * hours_in_day) + (minute * minutes_in_hour) + (second * seconds_in_minute);

        private static int GetInSurroundingUnit(double unit, int surroundingUnit, int numericBase) => checked(((int)unit % surroundingUnit) + numericBase);

        private static double GetFromBeginning(double unit, int surroundingUnit) => checked(unit / surroundingUnit);

        private static double SetFromBeginning(double unit, int surroundingUnit) => checked(unit * surroundingUnit);
    }
}