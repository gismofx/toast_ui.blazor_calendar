using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Theme
{
    //todo: add descriptions
    public interface IWeekTheme
    {
        [JsonPropertyName("dayName")]
        public IDayName DayName { get; set; }

        [JsonPropertyName("dayGrid")]
        public IDayGrid DayGrid { get; set; }

        [JsonPropertyName("dayGridLeft")]
        public IDayGridLeft DayGridLeft { get; set; }

        [JsonPropertyName("timeGrid")]
        public ITimeGrid TimeGrid { get; set; }

        [JsonPropertyName("timeGridLeft")]
        public ITimeGridLeft TimeGridLeft { get; set; }

        [JsonPropertyName("timeGridLeftAdditionalTimeZone")]
        public ITimeGridLeftAdditionalTimeZone TimeGridLeftAdditionalTimeZone { get; set; }

        [JsonPropertyName("timeGridHalfHour")]
        public ITimeGridHalfHour TimeGridHalfHour { get; set; }

        [JsonPropertyName("nowIndicatorLabel")]
        public INowIndicatorLabel NowIndicatorLabel { get; set; }

        [JsonPropertyName("nowIndicatorPast")]
        public INowIndicatorPast NowIndicatorPast { get; set; }

        [JsonPropertyName("nowIndicatorBullet")]
        public INowIndicatorBullet NowIndicatorBullet { get; set; }

        [JsonPropertyName("nowIndicatorToday")]
        public INowIndicatorToday NowIndicatorToday { get; set; }

        [JsonPropertyName("nowIndicatorFuture")]
        public INowIndicatorFuture NowIndicatorFuture { get; set; }

        [JsonPropertyName("pastTime")]
        public IPastTime PastTime { get; set; }

        [JsonPropertyName("futureTime")]
        public IFutureTime FutureTime { get; set; }

        [JsonPropertyName("weekend")]
        public IWeekend Weekend { get; set; }

        [JsonPropertyName("today")]
        public IToday Today { get; set; }

        [JsonPropertyName("pastDay")]
        public IPastDay PastDay { get; set; }

        [JsonPropertyName("panelResizer")]
        public IPanelResizer PanelResizer { get; set; }

        [JsonPropertyName("gridSelection")]
        public IGridSelection GridSelection { get; set; }
    }

}
