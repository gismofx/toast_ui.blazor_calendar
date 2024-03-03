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

    public class Rootobject
    {
        public string id { get; set; }
        public string calendarId { get; set; }
        public int __cid { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public bool isAllday { get; set; }
        public Start start { get; set; }
        public End end { get; set; }
        public object goingDuration { get; set; }
        public object comingDuration { get; set; }
        public object location { get; set; }
        public object attendees { get; set; }
        public string category { get; set; }
        public string dueDateClass { get; set; }
        public object recurrenceRule { get; set; }
        public int state { get; set; }
        public bool isVisible { get; set; }
        public bool isPending { get; set; }
        public bool isFocused { get; set; }
        public bool isReadOnly { get; set; }
        public bool isPrivate { get; set; }
        public object color { get; set; }
        public object backgroundColor { get; set; }
        public object dragBackgroundColor { get; set; }
        public object borderColor { get; set; }
        public object customStyle { get; set; }
        public object raw { get; set; }
    }

    public class Start
    {
        public int tzOffset { get; set; }
        public D d { get; set; }
    }

    public class D
    {
        public DateTime d { get; set; }
    }

    public class End
    {
        public int tzOffset { get; set; }
        public D1 d { get; set; }
    }

    public class D1
    {
        public DateTime d { get; set; }
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
