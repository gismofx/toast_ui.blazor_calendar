using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using toast_ui.blazor_calendar.Models;
namespace toast_ui.blazor_calendar.Services
{
    internal interface ITUICalendarInteropService
    {
        ValueTask Clear();
        ValueTask ChangeView(TUICalendarViewName viewName);
        ValueTask CreateEventsAsync(IEnumerable<TUIEvent> events);
        ValueTask InitCalendarAsync(DotNetObjectReference<TUICalendar> objectReference, TUICalendarOptions calendarOptions);
        ValueTask MoveCalendar(CalendarMove moveTo);
        ValueTask SetCalendars(IEnumerable<CalendarInfo> calendars);
        ValueTask SetCalendarVisibility(IEnumerable<string> calendarIds, bool isVisible);
        ValueTask SetCalendarVisibility(string calendarId, bool isVisible);

        (TUIEvent newEvent,TUIEvent eventChangesOnly) UpdateEvent(JsonElement eventToModify, JsonElement changedEvent); //TUIEvent eventToModify, string changedEvent);

        ValueTask SetDate(DateTimeOffset? dateToDisplay);
        ValueTask<DateTimeOffset?> GetDateRangeStart();
        ValueTask<DateTimeOffset?> GetDateRangeEnd();
        ValueTask SetCalendarOptions(TUICalendarOptions calendarOptions);
        ValueTask ScrollToNow();
        ValueTask SetTheme(TUITheme theme);
        TUIEvent Deserialize(JsonElement jsonEvent);

    }
}
