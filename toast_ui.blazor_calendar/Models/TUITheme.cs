using System.Dynamic;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Models.Theme;


namespace toast_ui.blazor_calendar.Models
{
    //https://github.com/nhn/tui.calendar/blob/main/docs/en/apis/theme.md#theme



    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/themeConfig
    /// </summary>
    public class TUITheme : ITheme
    {
        public ICommonTheme CommonTheme { get; set; } = null;
        public IWeekTheme WeekTheme { get; set; } = null;
        public IMonthTheme MonthTheme { get; set; } = null;

    }


    public class CommonTheme : ICommonTheme
    {
        public string BackgroundColor { get; set; } = "white";
        public string Border { get; set; } = "1px solid #e5e5e5";
        public object GridSelection { get; set; } = null;
        public IColorProperty DayName { get; set; }
        public IColorProperty Holiday {get; set;} 
        public IColorProperty Saturday {get; set;} 
        public IColorProperty Today {get; set;} 
    }

    public class WeekTheme : IWeekTheme
    {
        public IDayName DayName {get; set;}
        public IDayGrid DayGrid {get; set;}
        public IDayGridLeft DayGridLeft {get; set;}
        public ITimeGrid TimeGrid {get; set;}
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

    public class MonthTheme : IMonthTheme
    {
        public IDayExceptThisMonth DayExceptThisMonth {get; set;}
        public IDayNameMonth DayName {get; set;}
        public IHolidayExceptThisMonth HolidayExceptThisMonth {get; set;}
        public IMoreView MoreView {get; set;}
        public IMoreViewTitle MoreViewTitle {get; set;}
        public IWeekendMonth Weekend {get; set;}
        public IGridCell GridCell {get; set;}
    }

}
