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
