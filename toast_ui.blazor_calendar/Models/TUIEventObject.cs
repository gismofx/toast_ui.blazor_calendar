using System;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Services;
using toast_ui.blazor_calendar.Services.JsonConverters;


namespace toast_ui.blazor_calendar.Models
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum EventState
    {
        [JsonPropertyName("busy")] Busy,
        [JsonPropertyName("free")] Free
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum EventCategory
    {
        [JsonPropertyName("milestone")] Milestone,
        [JsonPropertyName("task")] Task,
        [JsonPropertyName("allday")] Allday,
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("time")] Time
    }


    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/EventObject
    /// </summary>
    public class TUIEventObject : IEventObject
    {
        
        public string? Id { get; set; } = null;
        public string? CalendarId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public bool? IsAllDay { get; set; }
        
        [JsonConverter(typeof(TZDateJsonConverter))]
        public DateTimeOffset? Start { get; set; }
        
        [JsonConverter(typeof(TZDateJsonConverter))]
        public DateTimeOffset? End { get; set; }
        
        public int? GoingDuration { get; set; }
        
        public int? ComingDuration { get; set; }
        
        public string? Location { get; set; }
        
        public string[]? Attendees { get; set; }

        public EventCategory? Category { get; set; }
        
        public string? RecurrenceRule { get; set; }
        
        public EventState? State { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsPending { get; set; }
        public bool? IsFocused { get; set; }
        public bool? IsReadOnly { get; set; }
        public bool? IsPrivate { get; set; }
        public string? Color { get; set; }
        public string? BackgroundColor { get; set; }
        public string? DragBackgroundColor { get; set; }
        public string? BorderColor { get; set; }
        public string? CustomStyle { get; set; }
        public string? Raw { get; set; }
    }
}
