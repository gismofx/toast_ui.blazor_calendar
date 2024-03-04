using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
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
        ValueTask SetCalendars(IEnumerable<CalendarInfo> calendars);
        TUIEvent UpdateEvent(TUIEvent eventToModify, string changedEvent);
        ValueTask HideShowCalendar(string calendarId, bool hide);
        ValueTask SetDate(DateTimeOffset? dateToDisplay);
        ValueTask<DateTimeOffset?> GetDateRangeStart();
        ValueTask<DateTimeOffset?> GetDateRangeEnd();
        ValueTask SetCalendarOptions(TUICalendarOptions calendarOptions);
        ValueTask ScrollToNow();
        ValueTask SetTheme(TUITheme theme);

    }
}
