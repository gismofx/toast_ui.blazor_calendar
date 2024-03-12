using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar
{
    /// <summary>
    /// Functions for the Calendar
    /// </summary>
    public partial class TUICalendar 
    {
        /// <summary>
        /// Add a new schedule to the calendar
        /// </summary>
        /// <param name="newSchedule"></param>
        /// <returns></returns>
        [JSInvokable("CreateEvent")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task CreateEvent(JsonElement newSchedule)
        {
            var schedule = CalendarInterop.Deserialize(newSchedule);
            await OnCreateCalendarEventOrTask.InvokeAsync(schedule);
            Debug.WriteLine("New Event Created");
        }

        /// <summary>
        /// Clears all events from the calendar.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task ClearCalendars()
        {
            await CalendarInterop.Clear();
        }

        /// <summary>
        /// Call this method and Advance the calendar, in any view, forward,backward, or to today.
        /// </summary>
        /// <param name="moveTo">Previous, Next, or Today</param>
        /// <returns></returns>
        public async Task MoveCalendar(CalendarMove moveTo)
        {
            await CalendarInterop.MoveCalendar(moveTo);
            await SetDateRange();
        }

        /// <summary>
        /// Updates the schedule on the calendar
        /// </summary>
        /// <param name="eventBeingModified"></param>
        /// <param name="updatedEventFields"></param>
        /// <returns></returns>
        [JSInvokable("UpdateEvent")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task UpdateSchedule(JsonElement eventBeingModified, JsonElement updatedEventFields)
        {
            var updatedEvent = CalendarInterop.UpdateEvent(eventBeingModified, updatedEventFields);
            await OnChangeCalendarEventOrTask.InvokeAsync(updatedEvent.newEvent); //Todo: Test This callback!
            Debug.WriteLine($"Event {updatedEvent.newEvent.Id} Modified");
        }

        /// <summary>
        /// Each time there is a view change or advance of the calendar, ask the calendar what date range is visible
        /// </summary>
        /// <returns></returns>
        private async Task SetDateRange()
        {
            if (CalendarInterop is not null)
            {
                await VisibleStartDateRangeChanged.InvokeAsync(await CalendarInterop.GetDateRangeStart());
                await VisibleEndDateRangeChanged.InvokeAsync(await CalendarInterop.GetDateRangeEnd());
            }
        }


        /// <summary>
        /// Scroll the calendar view to user's current time. i.e. now.
        /// </summary>
        /// <returns></returns>
        public async Task ScrollToNow()
        {
            await CalendarInterop.ScrollToNow();
        }

        /// <summary>
        /// Go To A Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task SetDate(DateTimeOffset date)
        {
            await CalendarInterop.SetDate(date);
        }

        /// <summary>
        /// Set the Calendar Theme
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public async Task SetTheme(TUITheme theme)
        {
            await CalendarInterop.SetTheme(theme);
        }

        public async ValueTask SetCalendarVisibility(string calendarId, bool isVisible)
        {
            await CalendarInterop.SetCalendarVisibility(calendarId, isVisible);
        }

        public async ValueTask ChangeView(TUICalendarViewName viewName)
        {
            await CalendarInterop.ChangeView(viewName);
        }

        public async ValueTask SetCalendarOptions(TUICalendarOptions options)
        {
            await CalendarInterop.SetCalendarOptions(options);
        }

    }
}
