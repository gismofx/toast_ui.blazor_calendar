using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using toast_ui.blazor_calendar.Models;
using toast_ui.blazor_calendar.Services.JsonConverters;

namespace toast_ui.blazor_calendar.Services
{
    /// <summary>
    /// JSInterop for BlazorTuiCalendarInterop.js
    /// API: https://nhn.github.io/tui.calendar/latest/
    /// </summary>
    public class TUICalendarInteropService : ITUICalendarInteropService, IAsyncDisposable
    {
        private readonly IJSRuntime _JSRuntime;

        public TUICalendarInteropService(IJSRuntime jsRuntime)
        {
            _JSRuntime = jsRuntime;
        }

        /// <summary>
        /// Clear the Calendar
        /// Wraps the tui.Calendar Api clear function
        /// </summary>
        public async ValueTask Clear()
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.clear");
        }

        /// <summary>
        /// Change the Display Mode/View of the Calendar
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public async ValueTask ChangeView(TUICalendarViewName viewName)
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.changeView", viewName.Value);
        }

        /// <summary>
        /// Put the events/task/etc on the calendar
        /// </summary>
        /// <param name="events">Events/Tasks To Display</param>
        /// <returns></returns>
        public async ValueTask CreateEventsAsync(IEnumerable<TUIEvent> events)
        {
            if (events is not null)
            {
                await _JSRuntime.InvokeVoidAsync("TUICalendar.createEvents", events);
            }
        }

        public ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            return new ValueTask();
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve the start date of the date range being displayed
        /// </summary>
        /// <returns></returns>
        public async ValueTask<DateTimeOffset?> GetDateRangeEnd()
        {
            var result = await _JSRuntime.InvokeAsync<JsonElement>("TUICalendar.getDateRangeEnd");
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new TZDateJsonConverter());
            return JsonSerializer.Deserialize<DateTimeOffset?>(result.ToString(), deserializeOptions);
        }

        /// <summary>
        /// Retrieve the ending rate of the date range being displayed 
        /// </summary>
        /// <returns></returns>
        public async ValueTask<DateTimeOffset?> GetDateRangeStart()
        {
            var result = await _JSRuntime.InvokeAsync<JsonElement>("TUICalendar.getDateRangeStart");
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new TZDateJsonConverter());
            return JsonSerializer.Deserialize<DateTimeOffset?>(result.ToString(), deserializeOptions);
        }

        /// <summary>
        /// Hide or Show a Specific Calendar of Events/Tasks By Calendar Id
        /// </summary>
        /// <param name="calendarId">Id of the calendar you want to hide or show</param>
        /// <param name="hide">True will hide. False will show</param>
        /// <returns></returns>
        public async ValueTask HideShowCalendar(string calendarId, bool hide)
        {
            await _JSRuntime.InvokeVoidAsync("hideShowCalendar", calendarId, hide);
        }

        /// <summary>
        /// Initialize the Calendar
        /// </summary>
        /// <param name="objectReference"></param>
        /// <returns></returns>
        public async ValueTask InitCalendarAsync(DotNetObjectReference<TUICalendar> objectReference, TUICalendarOptions calendarOptions)
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.initializeCalendar", objectReference, calendarOptions);
        }

        /// <summary>
        /// Move/Change the Calendar Viewing Date Range
        /// </summary>
        /// <param name="moveTo"></param>
        /// <returns></returns>
        public async ValueTask MoveCalendar(CalendarMove moveTo)
        {
            short value = 0;
            switch (moveTo)
            {
                case CalendarMove.Next:
                    value = 1;
                    break;

                case CalendarMove.Previous:
                    value = -1;
                    break;

                case CalendarMove.Today:
                    value = 0;
                    break;
            }
            await _JSRuntime.InvokeVoidAsync("TUICalendar.moveToNextOrPreviousOrToday", value);
        }

        /// <summary>
        /// Scroll to current time on today in daily or weekly view
        /// </summary>
        public async ValueTask ScrollToNow()
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.scrollToNowInView");
        }

        /// <summary>
        /// Sets Calendar Options
        /// </summary>
        /// <param name="calendarOptions"></param>
        /// <returns></returns>
        public async ValueTask SetCalendarOptionsAsync(TUICalendarOptions calendarOptions)
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.setCalendarOptions", calendarOptions);
        }
        
        /// <summary>
        /// Set the calendars' properties via TUICalendarProps
        /// </summary>
        /// <param name="calendars"></param>
        /// <returns></returns>
        public async ValueTask SetCalendars(IEnumerable<TUICalendarProps> calendars)
        {
            if (calendars is not null)
            {
                await _JSRuntime.InvokeVoidAsync("TUICalendar.setCalendars", calendars);
            }
        }
        
        /// <summary>
        /// Sett/Go To a Date on the Calendar
        /// </summary>
        /// <param name="dateToDisplay"></param>
        /// <returns></returns>
        public async ValueTask SetDate(DateTimeOffset? dateToDisplay)
        {
            if (dateToDisplay is not null)
            {
                var dateTimeInMilliseconds = dateToDisplay.Value.ToUnixTimeMilliseconds();
                await _JSRuntime.InvokeVoidAsync("TUICalendar.setDate", dateTimeInMilliseconds);
            }
        }

        /// <summary>
        /// Call when an updated event has been returned from the calendar
        /// </summary>
        /// <param name="eventToModify">Current Event Object</param>
        /// <param name="changedEvent">The changes made to the event</param>
        /// <returns>The changed event ready to further processing and/or saving</returns>
        public TUIEvent UpdateEvent(TUIEvent eventToModify, JsonElement changedEvent)
        {
            return CombineTuiEvent(eventToModify, changedEvent);
        }
        
        private static TUIEvent CombineTuiEvent(TUIEvent @event, JsonElement changes)
        {
            var c = JsonSerializer.Deserialize<TUIEvent>(changes.ToString());
            CopyValues(@event, c);
            return @event;
        }

        //@Todo: Refactor
        private static void CopyValues<T>(T target, T source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }
    }
}
