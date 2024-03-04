using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Helpers
{
    /// <summary>
    /// Helpers for rounding a DateTimeOffset
    /// ref: https://stackoverflow.com/questions/7029353/how-can-i-round-up-the-time-to-the-nearest-x-minutes
    /// </summary>
    public static class DateTimeHelpers
    {
        public static DateTimeOffset RoundToNearest(this DateTimeOffset dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            var roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;

            return new DateTimeOffset(dt.Ticks + offset - delta, dt.Offset);
        }

        public static DateTimeOffset RoundUp(this DateTimeOffset dt, TimeSpan d)
        {
            var modTicks = dt.Ticks % d.Ticks;
            var delta = modTicks != 0 ? d.Ticks - modTicks : 0;
            return new DateTimeOffset(dt.Ticks + delta, dt.Offset);
        }

        public static DateTimeOffset RoundDown(this DateTimeOffset dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTimeOffset(dt.Ticks - delta, dt.Offset);
        }
    }
}
