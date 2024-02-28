using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using toast_ui.blazor_calendar.Services.JsonConverters;

namespace toast_ui.blazor_calendar.Models
{
    public interface IEventObject
    {

        /// <summary>
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
        /// Event Start
        /// </summary>
        [JsonConverter(typeof(TZDateJsonConverter))]
        [JsonPropertyName("start")]
        public DateTimeOffset? Start { get; set; }

        /// <summary>
        /// Event End
        /// </summary>
        [JsonConverter(typeof(TZDateJsonConverter))]
        [JsonPropertyName("end")]
        public DateTimeOffset? End { get; set; }

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
}
