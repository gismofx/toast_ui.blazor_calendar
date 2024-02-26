using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Theme
{
    public interface ITheme
    {
        [JsonPropertyName("common")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        ICommonTheme CommonTheme { get; set; }

        [JsonPropertyName("week")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        IWeekTheme WeekTheme { get; set; }

        [JsonPropertyName("month")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        IMonthTheme MonthTheme { get; set; }
    }
}
