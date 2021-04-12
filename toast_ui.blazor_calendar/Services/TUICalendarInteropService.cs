using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async ValueTask UpdateSchedule(TUISchedule changedSchedule)
        {
            Console.WriteLine("Event Received");
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask();
            //throw new NotImplementedException();
        }
    }
}
