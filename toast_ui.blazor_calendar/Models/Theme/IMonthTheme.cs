using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Theme
{
    public interface IMonthTheme : IDayExceptThisMonth, IDayNameMonth, IHolidayExceptThisMonth,
        IMoreView, IMoreViewTitle, IWeekendMonth, IGridCell
    { }

    public interface IDayExceptThisMonth : IColorProperty { }

    public interface IDayNameMonth : IBorderLeftProperty, IBackgroundColorProperty { }

    public interface IHolidayExceptThisMonth : IColorProperty { }

    public interface IMoreView : IBackgroundColorProperty, IBorderProperty, IBoxShadowProperty, IWidthNullableInt, IHeightNullableInt { }

    public interface IMoreViewTitle : IBackgroundColorProperty { }

    public interface IWeekendMonth : IBackgroundColorProperty { }

    public interface IGridCell : IHeaderHeightNullableInt, IFooterHeightNullableInt { }



}
