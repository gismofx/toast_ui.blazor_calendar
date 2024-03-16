using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using toast_ui.blazor_calendar.JsonConverters;
using toast_ui.blazor_calendar.Models;
using toast_ui.blazor_calendar.Models.Extensions;

namespace toast_ui.blazor_calendar.Services
{
    /// <summary>
    /// JSInterop for BlazorTuiCalendarInterop.js
    /// API: https://nhn.github.io/tui.calendar/latest/
    /// </summary>
    internal class TUICalendarInteropService : ITUICalendarInteropService, IAsyncDisposable
    {
        private readonly IJSRuntime _JSRuntime;

        private static JsonSerializerOptions _JsonSerializerOptions = new JsonSerializerOptions() 
        { 
            PropertyNameCaseInsensitive = true, 
            UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip
        };

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
            await _JSRuntime.InvokeVoidAsync("TUICalendar.changeView", viewName);
        }

        /// <summary>
        /// Put the events/task/etc on the calendar
        /// </summary>
        /// <param name="events">Events/Tasks To Display</param>
        /// <returns></returns>
        public async ValueTask CreateEventsAsync(IEnumerable<TUIEvent> events)
        {
            if (events is null) return;
                
            await _JSRuntime.InvokeVoidAsync("TUICalendar.createEvents", events);
        }

        /// <summary>
        /// Put the event/task/etc on the calendar
        /// </summary>
        /// <param name="tuiEvent">Schedule/Event/Task To Display</param>
        public async ValueTask CreateEventAsync(TUIEvent tuiEvent)
        {
            if (tuiEvent is not null)
            {
                await CreateEventsAsync(new List<TUIEvent>() { 
                    tuiEvent
                });
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
        public async ValueTask SetCalendarOptions(TUICalendarOptions calendarOptions)
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.setCalendarOptions", calendarOptions);
        }

        /// <summary>
        /// Set the calendars' properties via TUICalendarProps
        /// </summary>
        /// <param name="calendars"></param>
        /// <returns></returns>
        public async ValueTask SetCalendars(IEnumerable<CalendarInfo> calendars)
        {
            if (calendars is null) return;
            
            await _JSRuntime.InvokeVoidAsync("TUICalendar.setCalendars", calendars);
        }

        public async ValueTask SetTheme(TUITheme theme)
        {
            if (theme is null) return;
            try
            {
                await _JSRuntime.InvokeVoidAsync("TUICalendar.setTheme", JsonSerializer.Serialize(theme));
            }
            
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async ValueTask ChangeTUIEventColors(string color)
        {
            if (color is null) return;
            try
            {
                await _JSRuntime.InvokeVoidAsync("TUICalendar.changeTuiEventColor", color);
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Set/Go To a Date on the Calendar
        /// </summary>
        /// <param name="dateToDisplay"></param>
        /// <returns></returns>
        public async ValueTask SetDate(DateTimeOffset? dateToDisplay)
        {
            if (dateToDisplay is null) return;
            
            var dateTimeInMilliseconds = dateToDisplay.Value.ToUnixTimeMilliseconds();
            await _JSRuntime.InvokeVoidAsync("TUICalendar.setDate", dateTimeInMilliseconds);
            
        }

        /// <summary>
        /// Call when an updated event has been returned from the calendar
        /// </summary>
        /// <param name="eventToModify">Current Event Object</param>
        /// <param name="changedEvent">The changes made to the event</param>
        /// <returns>Tuple:The changed event ready to further processing and/or saving, an event object with ONLY the values that were changed
        /// This is what TUI produces</returns>
        public (TUIEvent newEvent, TUIEvent eventChangesOnly) UpdateEvent(JsonElement eventToModify, JsonElement changedEvent)
        {
            var currentEvent = DeserializeEventObject(eventToModify);
            var c = DeserializeEventObject(changedEvent);
            return (CombineTuiEvent(currentEvent,c),c);
        }


        // TODO: ??????        
        private static TUIEvent CombineTuiEvent(TUIEvent eventToModify, TUIEvent changesAsJson)//JsonElement changes)
        {
            CopyValues<TUIEvent>(eventToModify, changesAsJson);
            return eventToModify;
        }

        // TODO: ??????
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

        private static TUIEvent DeserializeEventObject(JsonElement eventAsJson)
        {
            return JsonSerializer.Deserialize<TUIEvent>(eventAsJson, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip });
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

        public TUIEvent Deserialize(JsonElement jsonEvent)
        {
            return DeserializeEventObject(jsonEvent);
        }

        /// <summary>
        /// Hide/Show all events that belong to a list of calendar Ids
        /// </summary>
        /// <param name="calendarIds">Calendar Ids that you want to set visibility for</param>
        /// <param name="isVisible">True=visible, False=hidden</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask SetCalendarVisibility(IEnumerable<string> calendarIds, bool isVisible)
        {
            try
            {
                await _JSRuntime.InvokeVoidAsync("TUICalendar.setCalendarVisibility", calendarIds, isVisible);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            
        }

        /// <summary>
        /// Hide/Show all events that belong to a given calendar id
        /// </summary>
        /// <param name="calendarId"></param>
        /// <param name="isVisibile">True=visible, False=hidden</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask SetCalendarVisibility(string calendarId, bool isVisible)
        {
            await SetCalendarVisibility(new[] {calendarId}, isVisible);
        }
    }
}
