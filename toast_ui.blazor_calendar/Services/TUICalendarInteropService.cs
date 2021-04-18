using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar.Services
{
    /// <summary>
    /// JSInterop for BlazorTuiCalendarInterop.js
    /// API: https://nhn.github.io/tui.calendar/latest/
    /// </summary>
    public class TUICalendarInteropService : IAsyncDisposable
    {

        private readonly IJSRuntime _JSRuntime;

        //private DotNetObjectReference<TUICalendar> ObjectReference;

        public TUICalendarInteropService(IJSRuntime jsRuntime)
        {
            _JSRuntime = jsRuntime;
        }
        
        /// <summary>
        /// Initialize the Calendar
        /// </summary>
        /// <param name="objectReference"></param>
        /// <returns></returns>
        public async Task InitCalendarAsync(DotNetObjectReference<TUICalendar> objectReference)
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.initializeCalendar", objectReference);
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

        /// <summary>
        /// Set the calendars' properties via TUICalendarProps 
        /// </summary>
        /// <param name="calendars"></param>
        /// <returns></returns>
        public async ValueTask SetCalendars(IEnumerable<TUICalendarProps> calendars)
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.setCalendars", calendars);
        }

        public async ValueTask ChangeView(TUICalendarViewName viewName)
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.changeView", viewName.Value);
        }

        public TUISchedule UpdateSchedule(TUISchedule scheduleToModify, JsonElement changedSchedule)
        {
            return CombineTuiSchedule(scheduleToModify, changedSchedule);
        }

        /// <summary>
        /// Move/Change the Calendar Viewing Date Range
        /// </summary>
        /// <param name="moveTo"></param>
        /// <returns></returns>
        public async ValueTask MoveCalendar(CalendarMove moveTo)
        {
            short value=0;
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

        //Todo: Refactor or move to service?
        //Todo: Bug when modifying a create event from UI
        private TUISchedule CombineTuiSchedule(TUISchedule schedule, JsonElement changes)
        {
            var c = JsonSerializer.Deserialize<TUISchedule>(changes.ToString());
            CopyValues(schedule, c);
            return schedule;
        }

        //Todo: Refactor
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

        public ValueTask DisposeAsync()
        {
            return new ValueTask();
            //throw new NotImplementedException();
        }
    }
}
