using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Theme
{
    public interface IMonthTheme 
    {
        [JsonPropertyName("dayExceptThisMonth")]
        public IDayExceptThisMonth DayExceptThisMonth { get; set; }

        [JsonPropertyName("dayName")]
        public IDayNameMonth DayName { get; set; }

        [JsonPropertyName("holidayExceptThisMonth")]
        public IHolidayExceptThisMonth HolidayExceptThisMonth { get; set; }

        [JsonPropertyName("moreView")]
        public IMoreView MoreView { get; set; }

        [JsonPropertyName("moreViewTitle")]
        public IMoreViewTitle MoreViewTitle { get; set; }

        [JsonPropertyName("weekend")]
        public IWeekendMonth Weekend { get; set; }

        [JsonPropertyName("gridCell")]
        public IGridCell GridCell { get; set; }

    }

    public interface IDayExceptThisMonth : IColorProperty { }

    public interface IDayNameMonth : IBorderLeftProperty, IBackgroundColorProperty { }

    public interface IHolidayExceptThisMonth : IColorProperty { }

    public interface IMoreView : IBackgroundColorProperty, IBorderProperty, IBoxShadowProperty, IWidthNullableInt, IHeightNullableInt { }

    public interface IMoreViewTitle : IBackgroundColorProperty { }

    public interface IWeekendMonth : IBackgroundColorProperty { }

    public interface IGridCell : IHeaderHeightNullableInt, IFooterHeightNullableInt { }



}
