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
    /// https://github.com/nhn/tui.calendar/blob/main/docs/en/apis/theme.md
    /// </summary>
    public class TUITheme
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("common")]
        public CommonTheme CommonTheme { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("week")]
        public WeekTheme WeekTheme { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("month")]
        public MonthTheme MonthTheme { get; set; } = null;
    }


    public class CommonTheme
    {
        /// <summary>
        /// Background Color of the Calendar
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? BackgroundColor { get; set; } = Color.White;

        /// <summary>
        /// Border of calendar
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Border { get; set; } = "1px solid #e5e5e5";

        [JsonIgnore]
        public Color? EventTitleColor { get; set; }

        /// <summary>
        /// Selected date/time area
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GridSelectionTheme GridSelection { get; set; } = null;

        /// <summary>
        /// Day of the week
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DayNameProperties? DayName { get; set; }

        /// <summary>
        /// Holiday
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public HolidayProperties? Holiday { get; set; }

        /// <summary>
        /// Saturday
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SaturdayProperties? Saturday { get; set; }

        /// <summary>
        /// Current Day
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TodayProperties? Today { get; set; }
    }

    public class DayNameProperties
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? Color { get; set; }
    }
    public class TodayProperties
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? Color { get; set; }
    }
    public class SaturdayProperties
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? Color { get; set; }
    }
    public class HolidayProperties
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color? Color { get; set; }
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
        public DayGridTheme DayGridLeft { get; set; }

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


        public ITimeGridLeft TimeGridLeft { get; set; }
        public ITimeGridLeftAdditionalTimeZone TimeGridLeftAdditionalTimeZone { get; set; }
        public ITimeGridHalfHour TimeGridHalfHour { get; set; }
        public INowIndicatorLabel NowIndicatorLabel { get; set; }
        public INowIndicatorPast NowIndicatorPast { get; set; }
        public INowIndicatorBullet NowIndicatorBullet { get; set; }
        public INowIndicatorToday NowIndicatorToday { get; set; }
        public INowIndicatorFuture NowIndicatorFuture { get; set; }
        public IPastTime PastTime { get; set; }
        public IFutureTime FutureTime { get; set; }
        public IWeekend Weekend { get; set; }
        public IToday Today { get; set; }
        public IPastDay PastDay { get; set; }
        public IPanelResizer PanelResizer { get; set; }
        public IGridSelection GridSelection { get; set; }
    }

    /// <summary>
    /// Theme for Month-View
    /// </summary>
    public class MonthTheme
    {

        public class DayExceptThisMonthTheme
        {
            [JsonConverter(typeof(ColorJsonConverter))]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public Color? Color { get; set; }
        }

        /// <summary>
        /// Color of Day Number for days not in current month
        /// i.e. dimmed/muted grey
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DayExceptThisMonthTheme DayExceptThisMonth { get; set; }

        public class HolidayExceptThisMonthTheme
        {
            [JsonConverter(typeof(ColorJsonConverter))]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public Color? Color { get; set; }
        }

        /// <summary>
        /// Color of Day Number for holiday days not in current month
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public HolidayExceptThisMonthTheme HolidayExceptThisMonth { get; set; }

        public class DayNameMonthTheme
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string BorderLeft { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonConverter(typeof(ColorJsonConverter))]
            public Color? BackgroundColor { get; set; }
        }

        /// <summary>
        /// The Day Name Header for day names in month view
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DayNameMonthTheme DayName { get; set; }

        public class MoreViewTheme
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            [JsonConverter(typeof(ColorJsonConverter))]
            public Color? BackgroundColor { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string Border { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string BoxShadow { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? Width { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? Height { get; set; }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MoreViewTheme MoreView { get; set; } = null;

        public class MoreViewTitleTheme
        {
            [JsonConverter(typeof(ColorJsonConverter))]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public Color? BackgroundColor { get; set; }
        }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MoreViewTitleTheme MoreViewTitle { get; set; }

        /// <summary>
        /// Weekend cell in monthly view
        /// </summary>
        public class WeekendTheme
        {
            [JsonConverter(typeof(ColorJsonConverter))]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public Color? BackgroundColor { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public WeekendTheme Weekend { get; set; }



        public class GridCellTheme
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? HeaderHeight { get; set; } = null;
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public int? FooterHeight { get; set; } = null;
        }

        /// <summary>
        /// Header and footer height of all cells in monthly view
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GridCellTheme GridCell { get; set; }
    }

}
