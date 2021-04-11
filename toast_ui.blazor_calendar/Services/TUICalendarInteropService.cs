using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar.Services
{
    class TUICalendarInteropService : IAsyncDisposable
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
                await _JSRuntime.InvokeVoidAsync("TUICalendar.setSchedules", schedules);
            }
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask();
            //throw new NotImplementedException();
        }
    }
}
