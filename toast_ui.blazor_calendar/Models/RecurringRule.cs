using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// recurrenceRule: 'FREQ=WEEKLY;UNTIL=20241231'
    /// </summary>
    public class RecurringRule : IRecurringRule
    {
        public RecurringRule()
        {

        }

        public RecurrenceTypes Frequency { get; set; }
        public DateTime? Until { get; set; }

        public string BuildRule()
        {
            return $"FREQ={Frequency};UNTIL={Until.Value.ToString("yyyyMMdd")}";
        }
    }

    public interface IRecurringRule
    {
        /// <summary>
        /// Defines the Frequency of the Recurrence
        /// </summary>
        public RecurrenceTypes Frequency { get; set; }

        /// <summary>
        /// Defines the End of the Recurrence
        /// </summary>
        public DateTime? Until { get; set; }

        string BuildRule();
    }

    /// <summary>
    /// Defines the Frequency of the Recurrence
    /// </summary>
    public enum RecurrenceTypes
    {
        /// <summary>
        /// Fequency is Daily
        /// </summary>
        [JsonPropertyName("daily")] Daily,
        /// <summary>
        /// Fequency is Weekly
        /// </summary>
        [JsonPropertyName("weekly")] Weekly,
        /// <summary>
        /// Fequency is Monthly
        /// </summary>
        [JsonPropertyName("monthly")] Monthly,
        /// <summary>
        /// Fequency is Yearly
        /// </summary>
        [JsonPropertyName("yearly")] Yearly
    }
}
