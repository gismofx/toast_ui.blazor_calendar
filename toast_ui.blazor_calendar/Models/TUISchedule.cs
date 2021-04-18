using System;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Services;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/Schedule
    /// </summary>
    public class TUISchedule
    {
        public string id { get; set; }

        /// <summary>
        /// The unique calendar id
        /// </summary>
        public string calendarId { get; set; }

        /// <summary>
        /// The schedule title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// The schedule body text which is text/plain
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// The start time. It's 'string' for input. It's 'TZDate' for output like event handler.
        /// </summary>
        [JsonConverter(typeof(TZDateJsonConverter))]
        public DateTimeOffset? start { get; set; }

        /// <summary>
        /// The end time. It's 'string' for input. It's 'TZDate' for output like event handler.
        /// </summary>

        [JsonConverter(typeof(TZDateJsonConverter))]
        public DateTimeOffset? end { get; set; }

        /// <summary>
        /// The travel time: Going duration minutes
        /// </summary>
        public int? goingDuration { get; set; }

        /// <summary>
        /// The travel time: Coming duration minutes
        /// </summary>
        public int? comingDuration { get; set; }

        /// <summary>
        /// The all day schedule
        /// </summary>
        public bool? isAllDay { get; set; }

        /// <summary>
        /// The schedule type('milestone', 'task', allday', 'time')
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// The task schedule type string
        ///(any string value is ok and mandatory if category is 'task')
        /// </summary>
        public string dueDateClass { get; set; }

        /// <summary>
        /// The location
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// The attendees
        /// </summary>
        public string[] attendees { get; set; }

        /// <summary>
        /// ICS RRule RFC Standard?
        /// </summary>
        public string recurrenceRule { get; set; }

        /// <summary>
        /// The in progress flag to do something like network job(The schedule will be transparent.)
        /// </summary>
        public bool? isPending { get; set; }

        /// <summary>
        /// The focused schedule flag
        /// </summary>
        public bool? isFocused { get; set; }

        /// <summary>
        /// The schedule visibility flag
        /// </summary>
        public bool? isVisible { get; set; } = true;

        /// <summary>
        /// The schedule read-only flag
        /// </summary>
        public bool? isReadOnly { get; set; }

        /// <summary>
        /// The private schedule
        /// </summary>
        public bool? isPrivate { get; set; }

        /// <summary>
        /// The schedule text color
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// The schedule background color
        /// </summary>
        public string bgColor { get; set; }

        /// <summary>
        /// The schedule background color when dragging it
        /// </summary>
        public string dragBgColor { get; set; }

        /// <summary>
        /// The schedule left border color
        /// </summary>
        public string borderColor { get; set; }

        /// <summary>
        /// The schedule's custom css class
        /// </summary>
        public string customStyle { get; set; }

        /// <summary>
        /// The user data
        /// any type of data
        /// </summary>
        public string raw { get; set; }

        /// <summary>
        /// The schedule's state ('busy', 'free')
        /// </summary>
        public string state { get; set; }
    }
}