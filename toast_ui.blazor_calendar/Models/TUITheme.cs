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


}
