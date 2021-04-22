using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar.Services
{
    public interface ITUICalendarInteropService
    {
        ValueTask ChangeView(TUICalendarViewName viewName);
        ValueTask CreateSchedulesAsync(IEnumerable<TUISchedule> schedules);
        ValueTask InitCalendarAsync(DotNetObjectReference<TUICalendar> objectReference, TUICalendarOptions calendarOptions);
        ValueTask MoveCalendar(CalendarMove moveTo);
        ValueTask SetCalendars(IEnumerable<TUICalendarProps> calendars);
        TUISchedule UpdateSchedule(TUISchedule scheduleToModify, JsonElement changedSchedule);
        ValueTask HideShowCalendar(string calendarId, bool hide);
        ValueTask SetDate(DateTimeOffset? dateToDisplay);
        ValueTask<DateTimeOffset?> GetDateRangeStart();
        ValueTask<DateTimeOffset?> GetDateRangeEnd();
        ValueTask SetCalendarOptionsAsync(TUICalendarOptions calendarOptions);
    }
}