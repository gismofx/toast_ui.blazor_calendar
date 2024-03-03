using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using toast_ui.blazor_calendar.Services.JsonConverters;

namespace toast_ui.blazor_calendar.Models
{
    public interface ITUIEventObject
    {

        /// <summary>
        /// Event Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Calendar Id
        /// </summary>
        public string CalendarId { get; set; }


        /// <summary>
        /// Event Title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Event Body
        /// </summary>
        public string? Body { get; set; }

        /// <summary>
        /// Event is All day
        /// </summary>
        public bool? IsAllDay { get; set; }

        /// <summary>
        /// Event Start UTC Time
        /// </summary>
        [JsonConverter(typeof(TZDateJsonConverter))]
        public DateTimeOffset? Start { get; set; }

        /// <summary>
        /// Event End UTC Time
        /// </summary>
        [JsonConverter(typeof(TZDateJsonConverter))]
        public DateTimeOffset? End { get; set; }

        /// <summary>
        /// Event Going Duration
        /// </summary>
        public int? GoingDuration { get; set; }

        /// <summary>
        /// Event Coming Duration
        /// </summary>
        public int? ComingDuration { get; set; }

        /// <summary>
        /// Event Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Event Attendees
        /// </summary>
        public string[]? Attendees { get; set; }

        /// <summary>
        /// Category of the event. Available categories are 'milestone', 'task', 'time' and 'allday'.
        /// Category will affect where it's displayed
        /// </summary>
        public EventCategory? Category { get; set; }

        // TODO: Create Structure for RecurrenceRule

        /// <summary>
        /// Event Recurrence Rule
        /// </summary>
        public string RecurrenceRule { get; set; }

        /// <summary>
        /// Show as Busy or Free
        /// </summary>
        public EventState? State { get; set; }

        /// <summary>
        /// Event is Visible
        /// </summary>
        public bool? IsVisible { get; set; }

        /// <summary>
        /// Event is Pending
        /// </summary>
        public bool? IsPending { get; set; }

        /// <summary>
        /// Event is Focused
        /// </summary>
        public bool? IsFocused { get; set; }

        /// <summary>
        /// Event is Read Only
        /// </summary>
        public bool? IsReadOnly { get; set; }

        /// <summary>
        /// Event is Private
        /// </summary>
        public bool? IsPrivate { get; set; }


        /// <summary>
        /// Event Color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Event Background Color
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Event Drag Background Color
        /// </summary>
        public string DragBackgroundColor { get; set; }

        /// <summary>
        /// Event Border Color
        /// </summary>
        public string BorderColor { get; set; }

        // TODO: Create Structure for CustomStyle
        /// <summary>
        /// Event Custom Style
        /// </summary>
        public string CustomStyle { get; set; }

        /// <summary>
        /// Event Raw
        /// </summary>
        public string Raw { get; set; }
    }
}


/*
 *         /// <summary>
        /// Event Id
        /// </summary>
        [JsonPropertyName("id")]
        public string id { get; set; }

        /// <summary>
        /// Calendar Id
        /// </summary>
        [JsonPropertyName("calendarId")]
        public string CalendarId { get; set; }


        /// <summary>
        /// Event Title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Event Body
        /// </summary>
        [JsonPropertyName("body")]
        public string? Body { get; set; }

        /// <summary>
        /// Event is All day
        /// </summary>
        [JsonPropertyName("isAllday")]
        public bool IsAllDay { get; set; }

        /// <summary>
        /// Event Start UTC Time
        /// </summary>
        //[JsonConverter(typeof(TZDateJsonConverter))]
        //[JsonPropertyName("start")]
        public DateTimeOffset? Start { get; set; }

        /// <summary>
        /// Event End UTC Time
        /// </summary>
        //[JsonConverter(typeof(TZDateJsonConverter))]
        //[JsonPropertyName("end")]
        public DateTimeOffset? end { get; set; }

        /// <summary>
        /// Event Going Duration
        /// </summary>
        [JsonPropertyName("goingDuration")]
        public int? GoingDuration { get; set; }

        /// <summary>
        /// Event Coming Duration
        /// </summary>
        [JsonPropertyName("comingDuration")]
        public int? ComingDuration { get; set; }

        /// <summary>
        /// Event Location
        /// </summary>
        [JsonPropertyName("location")]
        public string Location { get; set; }

        /// <summary>
        /// Event Attendees
        /// </summary>
        [JsonPropertyName("attendees")]
        public string[] Attendees { get; set; }

        /// <summary>
        /// <seealso cref="TUIEvent"/> of an Event.
        /// </summary>
        [JsonPropertyName("category")]
        EventCategory Category { get; set; }

        // TODO: Create Structure for RecurrenceRule

        /// <summary>
        /// Event Recurrence Rule
        /// </summary>
        [JsonPropertyName("recurrenceRule")]
        public string RecurrenceRule { get; set; }

        /// <summary>
        /// <seealso cref="TUIEvent"/> of an Event.
        /// </summary>
        [JsonPropertyName("state")]
        public EventState State { get; set; }

        /// <summary>
        /// Event is Visible
        /// </summary>
        [JsonPropertyName("isVisible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// Event is Pending
        /// </summary>
        [JsonPropertyName("isPending")]
        public bool IsPending { get; set; }

        /// <summary>
        /// Event is Focused
        /// </summary>
        [JsonPropertyName("isFocused")]
        public bool IsFocused { get; set; }

        /// <summary>
        /// Event is Read Only
        [JsonPropertyName("isReadOnly")]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Event is Private
        /// </summary>
        [JsonPropertyName("isPrivate")]
        public bool IsPrivate { get; set; }


        /// <summary>
        /// Event Color
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        /// Event Background Color
        /// </summary>
        [JsonPropertyName("backgroundColor")]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Event Drag Background Color
        /// </summary>
        [JsonPropertyName("dragBackgroundColor")]
        public string DragBackgroundColor { get; set; }

        /// <summary>
        /// Event Border Color
        /// </summary>
        [JsonPropertyName("borderColor")]
        public string BorderColor { get; set; }

        // TODO: Create Structure for CustomStyle

        /// <summary>
        /// Event Custom Style
        /// </summary>
        [JsonPropertyName("customStyle")]
        public string CustomStyle { get; set; }

        /// <summary>
        /// Event Raw
        /// </summary>
        [JsonPropertyName("raw")]
        public string Raw { get; set; }
    }
*/
