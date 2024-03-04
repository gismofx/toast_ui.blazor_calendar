using System.Drawing;
using System.Dynamic;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.JsonConverters;
using toast_ui.blazor_calendar.Models.Theme;


namespace toast_ui.blazor_calendar.Models
{
    //https://github.com/nhn/tui.calendar/blob/main/docs/en/apis/theme.md#theme



    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/themeConfig
    /// </summary>
    public class TUITheme
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CommonTheme CommonTheme { get; set; } = null;
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public WeekTheme WeekTheme { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MonthTheme MonthTheme { get; set; } = null;

    }


    public class CommonTheme
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? BackgroundColor { get; set; } = Color.White;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Border { get; set; } = "1px solid #e5e5e5";

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GridSelectionTheme GridSelection { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? DayName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? Holiday {get; set;}

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? Saturday {get; set;}

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? Today {get; set;} 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///   backgroundColor: 'rgba(81, 92, 230, 0.05)',
    ///   border: '1px solid #515ce6'
    /// </example>
    public class GridSelectionTheme
    {
        public Color? BackgroundColor { get; set; }
        public string Border { get; set; }
    }

    public class WeekTheme
    {
        public class DayNameTheme
        {
            public string BorderLeft { get; set; }
            public string BorderTop { get; set; }
            public string BorderBottom { get; set; }

            [JsonConverter(typeof(ColorJsonConverter))]
            public Color? BackgroundColor { get; set; }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DayNameTheme DayName { get; set; } = null;


        public class DayGridTheme
        {
            public string BorderRight { get; set; }

            [JsonConverter(typeof(ColorJsonConverter))]
            public Color? BackgroundColor { get; set; }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DayGridTheme DayGrid { get; set; } = null;

        public class DayGridLeftTheme
        {
            public string BorderRight { get; set; }

            [JsonConverter(typeof(ColorJsonConverter))]
            public Color? BackgroundColor { get; set; }
            public string Width { get; set; }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DayGridTheme DayGridLeft {get; set;}

        public class TimeGridTheme : ITimeGrid
        {
            /// <summary>
            /// 
            /// </summary>
            /// <example>
            /// '1px solid #e5e5e5'
            /// </example>
            public string Border { get; set; }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TimeGridTheme TimeGrid { get; set; } = null;
        
        
        public ITimeGridLeft TimeGridLeft {get; set;}
        public ITimeGridLeftAdditionalTimeZone TimeGridLeftAdditionalTimeZone {get; set;}
        public ITimeGridHalfHour TimeGridHalfHour {get; set;}
        public INowIndicatorLabel NowIndicatorLabel {get; set;}
        public INowIndicatorPast NowIndicatorPast {get; set;}
        public INowIndicatorBullet NowIndicatorBullet {get; set;}
        public INowIndicatorToday NowIndicatorToday {get; set;}
        public INowIndicatorFuture NowIndicatorFuture {get; set;}
        public IPastTime PastTime {get; set;}
        public IFutureTime FutureTime {get; set;}
        public IWeekend Weekend {get; set;}
        public IToday Today {get; set;}
        public IPastDay PastDay {get; set;}
        public IPanelResizer PanelResizer {get; set;}
        public IGridSelection GridSelection {get; set;}
    }

    public class MonthTheme
    {

        public class DayExceptThisMonthTheme
        {
            [JsonConverter(typeof(ColorJsonConverter))]
            public Color? Color { get; set; }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DayExceptThisMonthTheme DayExceptThisMonth { get; set;}

        public class DayNameMonthTheme : IDayNameMonth
        {
            public string BorderLeft { get; set; }

            [JsonConverter(typeof(ColorJsonConverter))]
            public Color BackgroundColor { get; set; }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DayNameMonthTheme DayName {get; set;}


        public IHolidayExceptThisMonth HolidayExceptThisMonth {get; set;}
        public IMoreView MoreView {get; set;}
        public IMoreViewTitle MoreViewTitle {get; set;}
        public IWeekendMonth Weekend {get; set;}
        public IGridCell GridCell {get; set;}
    }

}
