using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Template
{
    /// <summary>
    /// https://github.com/nhn/tui.calendar/blob/main/docs/en/apis/event-object.md
    /// </summary>
    public class EventObject
    {
    }

    public interface IEventObject
    {
        /// <summary>
        /// Event Id
        /// </summary>
        [JsonPropertyName("id")]
        string Id { get; set; }
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
