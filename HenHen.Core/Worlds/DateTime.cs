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
        public int Years { get => Months / months_in_year; set => Months = value * months_in_year; }
        public int Months { get => Weeks / weeks_in_month; set => Weeks = value * weeks_in_month; }
        public int Seasons => (Months + 1) / 2;
        public int Weeks { get => Days / 6; set => Days = value * days_in_week; }
        public int Days { get => Hours / 24; set => Hours = value * hours_in_day; }
        public int Hours { get => Minutes / 60; set => Minutes = value * minutes_in_hour; }
        public int Minutes { get => Seconds / 60; set => Seconds = value * seconds_in_minute; }
        public int Seconds { get => ticks / 60; set => ticks = value * ticks_in_second; }
    }
}