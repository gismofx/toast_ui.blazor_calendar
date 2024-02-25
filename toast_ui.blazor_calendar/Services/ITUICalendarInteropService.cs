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
        ValueTask Clear();
        ValueTask ChangeView(TUICalendarViewName viewName);
        ValueTask CreateEventsAsync(IEnumerable<TUIEvent> events);
        ValueTask InitCalendarAsync(DotNetObjectReference<TUICalendar> objectReference, TUICalendarOptions calendarOptions);
        ValueTask MoveCalendar(CalendarMove moveTo);
        ValueTask SetCalendars(IEnumerable<TUICalendarProps> calendars);
        TUIEvent UpdateEvent(TUIEvent eventToModify, JsonElement changedEvent);
        ValueTask HideShowCalendar(string calendarId, bool hide);
        ValueTask SetDate(DateTimeOffset? dateToDisplay);
        ValueTask<DateTimeOffset?> GetDateRangeStart();
        ValueTask<DateTimeOffset?> GetDateRangeEnd();
        ValueTask SetCalendarOptionsAsync(TUICalendarOptions calendarOptions);
        ValueTask ScrollToNow();
    }
}
