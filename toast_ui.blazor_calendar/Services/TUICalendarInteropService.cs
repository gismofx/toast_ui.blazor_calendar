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

        //private DotNetObjectReference<TUICalendar> ObjectReference;

        public TUICalendarInteropService(IJSRuntime jsRuntime)
        {
            _JSRuntime = jsRuntime;
        }

        /// <summary>
        /// Wraps the tui.Calendar Api clear function
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async ValueTask Clear()
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.clear");
        }

        public async ValueTask ChangeView(TUICalendarViewName viewName)
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.changeView", viewName.Value);
        }

        /// <summary>
        /// Put the events/task/etc on the calendar
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async ValueTask CreateSchedulesAsync(IEnumerable<TUISchedule> schedules)
        {
            if (schedules is not null)
            {
                await _JSRuntime.InvokeVoidAsync("TUICalendar.createSchedules", schedules);
            }
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask();
            //throw new NotImplementedException();
        }

        public async ValueTask<DateTimeOffset?> GetDateRangeEnd()
        {
            var result = await _JSRuntime.InvokeAsync<JsonElement>("TUICalendar.getDateRangeEnd");
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new TZDateJsonConverter());
            return JsonSerializer.Deserialize<DateTimeOffset?>(result.ToString(), deserializeOptions);
        }

        public async ValueTask<DateTimeOffset?> GetDateRangeStart()
        {
            var result = await _JSRuntime.InvokeAsync<JsonElement>("TUICalendar.getDateRangeStart");
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new TZDateJsonConverter());
            return JsonSerializer.Deserialize<DateTimeOffset?>(result.ToString(), deserializeOptions);
        }

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
        public void ScrollToNow()
        {
            _JSRuntime.InvokeVoidAsync("TUICalendar.scrollToNowInView");
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
        public async ValueTask SetDate(DateTimeOffset? dateToDisplay)
        {
            if (dateToDisplay is not null)
            {
                await _JSRuntime.InvokeVoidAsync("TUICalendar.setDate", dateToDisplay);
            }
        }

        /// <summary>
        /// Call when an updated schedule has been returned from the calendar
        /// </summary>
        /// <param name="scheduleToModify">Current Schedule Object</param>
        /// <param name="changedSchedule">The changes made to the schedule</param>
        /// <returns>The changed schedule ready to further processing and/or saving</returns>
        public TUISchedule UpdateSchedule(TUISchedule scheduleToModify, JsonElement changedSchedule)
        {
            return CombineTuiSchedule(scheduleToModify, changedSchedule);
        }
        private TUISchedule CombineTuiSchedule(TUISchedule schedule, JsonElement changes)
        {
            var c = JsonSerializer.Deserialize<TUISchedule>(changes.ToString());
            CopyValues(schedule, c);
            return schedule;
        }

        //@Todo: Refactor
        private void CopyValues<T>(T target, T source)
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