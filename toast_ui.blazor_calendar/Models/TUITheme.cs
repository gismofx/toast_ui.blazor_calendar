using System.Dynamic;
using System.Text.Json.Serialization;

namespace toast_ui.blazor_calendar.Models
{
    //https://github.com/nhn/tui.calendar/blob/main/docs/en/apis/theme.md#theme

    public interface ITUITheme
    {
        [JsonPropertyName("common")]
        ITUICommonTheme CommonTheme { get; set; }
        
        [JsonPropertyName("week")]
        ITUIWeekTheme WeekTheme { get; set; }
        
        [JsonPropertyName("month")]
        ITUIMonthTheme MonthTheme { get; set; }

    }

    public interface IColorProperty
    {
        [JsonPropertyName("color")]
        public string Color { get; set; }
    }

    public interface IBackgroundColorProperty
    {
        [JsonPropertyName("backgroundColor")]
        public string BackgroundColor { get; set; }
    }

    public interface IBorderBottomProperty
    {
        [JsonPropertyName("borderBottom")]
        public string BorderBottom { get; set; }
    }

    public interface IBorderTopProperty
    {
        [JsonPropertyName("borderTop")]
        public string BorderTop { get; set; }
    }

    public interface IBorderLeftProperty
    {
        [JsonPropertyName("borderLeft")]
        public string BorderLeft { get; set; }
    }
    public interface IBorderRightProperty
    {
        [JsonPropertyName("borderRight")]
        public string BorderRight { get; set; }
    }

    public interface ITUICommonTheme
    {
        /// <summary>
        /// Background color of calendar
        /// Default Value: white
        /// </summary>
        [JsonPropertyName("backgroundColor")]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Border of calendar
        /// Default Value: 1px solid #e5e5e5
        /// </summary>
        [JsonPropertyName("border")]
        public string Border { get; set; }

        [JsonPropertyName("gridSelection")]
        public object GridSelection { get; set; }
        //gridSelection: {
        //  backgroundColor: string;
        //  border: string;
        //};

        /// <summary>
        /// Day of the week
        /// Default Value: { color: '#333' }	
        /// </summary>
        [JsonPropertyName("dayName")]
        public IColorProperty DayName { get; set; }
    
        [JsonPropertyName("holiday")]
        public IColorProperty Holiday { get; set; }

        [JsonPropertyName("saturday")]
        public IColorProperty Saturday { get; set;}

        [JsonPropertyName("today")]
        public IColorProperty Today { get; set; }
      
    }

 

    public interface ITUIWeekTheme
    {
        [JsonPropertyName("dayName")]
        public TuiDayName DayName { get; set; }
        public class TuiDayName : IBorderLeftProperty, IBorderTopProperty, IBorderBottomProperty, IBackgroundColorProperty
        {
            public string BorderLeft { get; set; }
            public string BorderTop { get; set; }
            public string BorderBottom { get; set; }
            public string BackgroundColor { get; set; }
        }

        public class DayGrid : IBorderRightProperty, IBackgroundColorProperty
        {
            public string BorderRight { get; set; }
            public string BackgroundColor { get; set; }
        }

        public class DayGridLeft
        {
            public string BorderRight { get; set; }
            public string BackgroundColor { get; set; }
            public string Width { get; set; }
        }

        public class TimeGrid
        {
            public string BorderRight { get; set; }
        }

        public class TimeGridLeft
        {
            public string BorderRight { get; set; }
            public string BackgroundColor { get; set; }
            public string Width { get; set; }
        }

        public class TimeGridLeftAdditionalTimezone
        {
            public string BackgroundColor { get; set; }
        }

        public class TimeGridHalfHour
        {
            public string BorderBottom { get; set; }
        }

        public class NowIndicatorLabel
        {
            public string Color { get; set; }
        }

        public class NowIndicatorPast
        {
            public string Border { get; set; }
        }

        public class NowIndicatorBullet
        {
            public string BackgroundColor { get; set; }
        }

        public class NowIndicatorToday
        {
            public string Border { get; set; }
        }

        public class NowIndicatorFuture
        {
            public string Border { get; set; }
        }

        public class PastTime
        {
            public string Color { get; set; }
        }

        public class FutureTime
        {
            public string Color { get; set; }
        }

        public class Weekend
        {
            public string BackgroundColor { get; set; }
        }

        public class Today
        {
            public string Color { get; set; }
            public string BackgroundColor { get; set; }
        }

        public class PastDay
        {
            public string Color { get; set; }
        }

        public class PanelResizer
        {
            public string Border { get; set; }
        }

        public class GridSelection
        {
            public string Color { get; set; }
        }
    }

    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/themeConfig
    /// </summary>
    public class TUITheme : ITUITheme
    {
        public TUITheme()
        {
        }

        public ITUICommonTheme CommonTheme { get; set; }
        public ITUIWeekTheme WeekTheme { get; set; }
        public ITUIMonthTheme MonthTheme { get; set; }
    }

    public interface ITUIMonthTheme
    {
        [JsonPropertyName("dayExceptThisMonth")]
        public IColorProperty DayExceptThisMonth { get; set; }

        [JsonPropertyName("dayName")]
        public DayNameTui DayName { get; set; }

        public struct DayNameTui
        {
            [JsonPropertyName("borderLeft")]
            public string BorderLeft { get; set;}
            [JsonPropertyName("backgroundColor")]
            public string BackgroundColor { get; set; }
        }

        /*

  dayName: {
    borderLeft: string;
    backgroundColor: string;
  };
  holidayExceptThisMonth: { color: string };
  moreView: {
    backgroundColor: string;
    border: string;
    boxShadow: string;
    width: number | null,
    height: number | null,
  };
  moreViewTitle: {
    backgroundColor: string;
  };
  weekend: { backgroundColor: string };
  gridCell: {
    headerHeight: number | null;
    footerHeight: number | null;
  };
}
        */
    }

}
