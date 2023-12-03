﻿namespace Schedulist.App.Models
{
    public class CalendarParams
    {
        public CalendarParams()
        {
            CurrentDate = DateTime.Now;
            FirstDayOfTheMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            LastDayOfTheMonth = FirstDayOfTheMonth.AddMonths(1).AddDays(-1);
            DaysToDraw = 42;
            if (LastDayOfTheMonth.DayOfWeek == DayOfWeek.Sunday) DaysToDraw -= 7;
            StartDate = FirstDayOfTheMonth.AddDays(-(int)FirstDayOfTheMonth.DayOfWeek + 1);
        }

        public DateTime CurrentDate { get; set; }
        public DateTime FirstDayOfTheMonth { get; private set; }
        public DateTime LastDayOfTheMonth { get; private set; }
        public int DaysToDraw { get; private set; }
        public DateTime StartDate { get; private set; }
    }
}
