using Schedulist.Business.Actions;
using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    internal interface IManageCalendarEvent
    {
        void ShowCalendarEvent(User user, DateOnly date);
        void ShowUserCalendarEvent(User user);

        void CreateCalendarEvent(User user);
        CalendarEvent GetCurrentCalendarEvent(User user);

        void ModifyCurrentCalendarEvent(User user);

        void ModifyCalendarEventsAdmin();

        void DeleteCurrentCalendarEvent(User user);


        void DeleteCalendarEventAdmin();
        void DisplayCalendarEvents(List<CalendarEvent> calendarEvents);
        void CurrentUserCalendarEventsSelection(User user);
        void CalendarEventsSelection();
        WorkModesToUser? GetWorkModesToUserForDate(User user, DateOnly calendarEventDate);
        DateOnly CalendarEventDateRelatedToWorkModeValidation(User user, DateOnly calendarEventDate);

        DateOnly CalendarEventDateWeekendValidation(DateOnly calendarEventDate);

        DateOnly CalendarEventDateMinMaxValidation(DateOnly calendarEventDate);

        DateOnly CalendarEventDateAddValidation(out string dateValue);

        void CalendarEventAvailabilityCheck(User user, CalendarEvent? calendarEventAvailable,
            DateOnly calendarEventDate);

        string EndTimeEmptinessValidation(string? endTime);
        string StartTimeEmptinessValidation(string? startTime);

        TimeOnly CalendarEventStartTimeOverlappingValidation(CalendarEvent? validatedStartTime,
            TimeOnly calendarEventStartTime, List<CalendarEvent> calendarEvents, DateOnly calendarEventDate,
            User user);

        string DateValueEmptinessValidation(string? dateValue);

        TimeOnly CalendarEventEndTimeValidation(TimeOnly calendarEventEndTime,
            TimeOnly calendarEventStartTime);

        TimeOnly CalendarEventStartTimeParseValidation();

#endregion
    }
}