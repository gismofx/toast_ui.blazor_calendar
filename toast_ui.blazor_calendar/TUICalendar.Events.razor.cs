using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace toast_ui.blazor_calendar
{
    /// <summary>
    /// Events for the Calendar
    /// </summary>
    public partial class TUICalendar
    {
        /// <summary>
        /// When a schedule is deleted from calendar UI, this is invoked        
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        [JSInvokable("DeleteEvent")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task OnDeleteEvent(string eventID)
        {
            await OnDeleteCalendarEventOrTask.InvokeAsync(eventID);
            Debug.WriteLine($"Event {eventID} Deleted!");
        }

        /// <summary>
        /// When a schedule is clicked from the calendar UI, this is invoked
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [JSInvokable("OnClickEvent")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task OnScheduleClick(string eventId)
        {
            await OnClickCalendarEventOrTask.InvokeAsync(eventId);
            Debug.WriteLine($"Event {eventId} Clicked!");
        }
    }
}
