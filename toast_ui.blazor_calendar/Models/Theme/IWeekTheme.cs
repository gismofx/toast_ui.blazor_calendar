using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Theme
{
    public interface IWeekTheme :
        IDayName, IDayGrid, IDayGridLeft, ITimeGrid, ITimeGridLeft, ITimeGridLeftAdditionalTimeZone,
        ITimeGridHalfHour, INowIndicatorLabel, INowIndicatorPast, INowIndicatorBullet, INowIndicatorToday,
        INowIndicatorFuture, IPastTime, IFutureTime, IWeekend, IToday, IPastDay, IPanelResizer, IGridSelection
    { }

}
