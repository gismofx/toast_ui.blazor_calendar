using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using toast_ui.blazor_calendar.Models;
using toast_ui.blazor_calendar.Models.Template;

namespace toast_ui.blazor_calendar.Services
{
    public interface ITUICalendarInteropService
    {
        ValueTask Clear();
        ValueTask ChangeView(TUICalendarViewName viewName);
        ValueTask CreateEventsAsync(IEnumerable<IEventObject> events);
        ValueTask InitCalendarAsync(DotNetObjectReference<TUICalendar> objectReference, TUICalendarOptions calendarOptions);
        ValueTask MoveCalendar(CalendarMove moveTo);
        ValueTask SetCalendars(IEnumerable<CalendarInfo> calendars);
        IEventObject UpdateEvent(IEventObject eventToModify, string changedEvent);
        ValueTask HideShowCalendar(string calendarId, bool hide);
        ValueTask SetDate(DateTimeOffset? dateToDisplay);
        ValueTask<DateTimeOffset?> GetDateRangeStart();
        ValueTask<DateTimeOffset?> GetDateRangeEnd();
        ValueTask SetCalendarOptionsAsync(TUICalendarOptions calendarOptions);
        ValueTask ScrollToNow();
        ValueTask SetTheme(TUITheme theme);
    }
}
