using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable CS8632 // The annotation for nullable reference types may only be used in code within a #nullable annotation context.

namespace toast_ui.blazor_calendar.Models.Template
{
    /// <summary>
    /// https://github.com/nhn/tui.calendar/blob/main/docs/en/apis/event-object.md
    /// </summary>
    public class EventObject
    {
        // TODO: Translation for specific properties if possible
        public EventObject()
        {
            
        }
    }

    public interface IEventObject
    {
        /// <summary>
        /// Event Id
        /// </summary>
        [JsonPropertyName("id")]
        string Id { get; set; }

        /// <summary>
        /// Event Title
        /// </summary>
        [JsonPropertyName("title")]
        string? Title { get; set; }

        /// <summary>
        /// Event Body
        /// </summary>
        [JsonPropertyName("body")]
        string? Body { get; set; }

        /// <summary>
        /// Event is Allday
        /// </summary>
        [JsonPropertyName("isAllday")]
        bool IsAllday { get; set; }

        /// <summary>
        /// Event Start
        /// </summary>
        [JsonPropertyName("start")]
        DateTime? Start { get; set; }

        /// <summary>
        /// Event End
        /// </summary>
        [JsonPropertyName("end")]
        DateTime? End { get; set; }

        /// <summary>
        /// Event Going Duration
        /// </summary>
        [JsonPropertyName("goingDuration")]
        int? GoingDuration { get; set; }

        /// <summary>
        /// Event Coming Duration
        /// </summary>
        [JsonPropertyName("comingDuration")]
        int? ComingDuration { get; set; }

        /// <summary>
        /// Event Location
        /// </summary>
        [JsonPropertyName("location")]
        string Location { get; set; }

        /// <summary>
        /// Event Attendees
        /// </summary>
        [JsonPropertyName("attendees")]
        string[] Attendees { get; set; }

        /// <summary>
        /// <seealso cref="TUICategorys"/> of an Event.
        /// </summary>
        [JsonPropertyName("category")]
        string Category { get; set; }

        // TODO: Create Structure for RecurrenceRule

        /// <summary>
        /// Event Recurrence Rule
        /// </summary>
        [JsonPropertyName("recurrenceRule")]
        string RecurrenceRule { get; set; }

        /// <summary>
        /// <seealso cref="TUIStates"/> of an Event.
        /// </summary>
        [JsonPropertyName("state")]
        string State { get; set; }

        /// <summary>
        /// Event is Visible
        /// </summary>
        [JsonPropertyName("isVisible")]
        bool IsVisible { get; set; }

        /// <summary>
        /// Event is Pending
        /// </summary>
        [JsonPropertyName("isPending")]
        bool IsPending { get; set; }

        /// <summary>
        /// Event is Focused
        /// </summary>
        [JsonPropertyName("isFocused")]
        bool IsFocused { get; set; }

        /// <summary>
        /// Event is Read Only
        [JsonPropertyName("isReadOnly")]
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Event is Private
        /// </summary>
        [JsonPropertyName("isPrivate")]
        bool IsPrivate { get; set; }


        /// <summary>
        /// Event Color
        /// </summary>
        [JsonPropertyName("color")]
        string Color { get; set; }

        /// <summary>
        /// Event Background Color
        /// </summary>
        [JsonPropertyName("backgroundColor")]
        string BackgroundColor { get; set; }

        /// <summary>
        /// Event Drag Background Color
        /// </summary>
        [JsonPropertyName("dragBackgroundColor")]
        string DragBackgroundColor { get; set; }

        /// <summary>
        /// Event Border Color
        /// </summary>
        [JsonPropertyName("borderColor")]
        string BorderColor { get; set; }

        // TODO: Create Structure for CustomStyle

        /// <summary>
        /// Event Custom Style
        /// </summary>
        [JsonPropertyName("customStyle")]
        string CustomStyle { get; set; }

        /// <summary>
        /// Event Raw
        /// </summary>
        [JsonPropertyName("raw")]
        string Raw { get; set; }

        /*
            calendarId?: string;
          title?: string;
          body?: string;
          isAllday?: boolean;
          start?: Date | string | number | TZDate;
          end?: Date | string | number | TZDate;
          goingDuration?: number;
          comingDuration?: number;
          location?: string;
          attendees?: string[];
          category?: 'milestone' | 'task' | 'allday' | 'time';
          recurrenceRule?: string;
          state?: 'Busy' | 'Free';
          isVisible?: boolean;
          isPending?: boolean;
          isFocused?: boolean;
          isReadOnly?: boolean;
          isPrivate?: boolean;
          color?: string;
          backgroundColor?: string;
          dragBackgroundColor?: string;
          borderColor?: string;
          customStyle?: JSX.CSSProperties;
          raw?: any;
        }
        */
    }
}
