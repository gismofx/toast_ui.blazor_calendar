using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar.Services.JsonConverters
{
    public class TUICalendarTimeZoneOptionJsonConverter : JsonConverter<TUICalendarTimeZoneOption>
    {
        //relevant property to extract
        private static readonly byte[] PropertyNameBytes = Encoding.UTF8.GetBytes("timezoneName");

        /// <summary>
        /// Reads JSON and converts the values to Timezone options
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns>TUICalendarTimeZoneOption</returns>
        public override TUICalendarTimeZoneOption? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            TUICalendarTimeZoneOption timeZoneOption = new TUICalendarTimeZoneOption();

            bool isProperty = false;

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.PropertyName:
                        isProperty = reader.ValueTextEquals(PropertyNameBytes);
                        break;
                    case JsonTokenType.String:
                        if (!isProperty) break;

                        string IanaId = reader.GetString();

                        if (TimeZoneInfo.TryConvertIanaIdToWindowsId(IanaId, out string windowsId))
                        {
                            timeZoneOption.zones.Add(TimeZoneInfo.FindSystemTimeZoneById(windowsId));
                        }

                        isProperty = false;
                        break;
                }
            }

            return timeZoneOption;
        }
        
        /// <summary>
        /// Serializes TUICalendarTimeZoneOption into expected format for TUI Calendar
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, TUICalendarTimeZoneOption value, JsonSerializerOptions options)
        {
            TUIZones timezone = new TUIZones();

            foreach (TimeZoneInfo timeZoneInfo in value.zones)
            {
                timezone.zones.Add(ToTuiTimeZone(timeZoneInfo, value.DisplayLabelFunction, value.DisplayLabelFunction));
            }

            writer.WriteRawValue(JsonSerializer.Serialize(timezone));
        }
        
        /// <summary>
        /// Converts a TimeZoneInfo into a TUI Defined Time Zone object
        /// </summary>
        /// <param name="timeZoneInfo">TimeZoneInfo</param>
        /// <returns>TUITimeZone</returns>
        private TUITimeZone ToTuiTimeZone(TimeZoneInfo timeZoneInfo, Func<TimeZoneInfo, string> displayLabelFunction, Func<TimeZoneInfo, string> toolTipFunction)
        {
            //if unable to get the IanaId return a null object
            if (!TimeZoneInfo.TryConvertWindowsIdToIanaId(timeZoneInfo.Id, out string timezoneName)) return null;

            //create default options based on the example https://nhn.github.io/tui.calendar/latest/Timezone
            string positiveOrNegative = timeZoneInfo.BaseUtcOffset < TimeSpan.Zero ? "-" : "+";
            string displayLabel = $"GMT{positiveOrNegative}{timeZoneInfo.BaseUtcOffset:hh\\:mm}";
            string tooltip = timeZoneInfo.StandardName;

            //if the user specified a function to get the
            //display label use that
            if (displayLabelFunction != null)
            {
                displayLabel = displayLabelFunction(timeZoneInfo);
            }

            //if the user specified a function to get the
            //tool tip use that
            if (toolTipFunction != null)
            {
                tooltip = toolTipFunction(timeZoneInfo);
            }

            return new TUITimeZone
            {
                timezoneName = timezoneName,
                displayLabel = displayLabel,
                tooltip = tooltip
            };
        }

        /// <summary>
        /// Parent Class for holding time zone information as prescribed at https://nhn.github.io/tui.calendar/latest/Timezone
        /// For serialization purposes
        /// </summary>
        private class TUIZones
        {
            public List<TUITimeZone> zones { get; set; }

            public TUIZones()
            {
                zones = new List<TUITimeZone>();
            }
        }

        /// <summary>
        /// Class for holding time zone information as prescribed at https://nhn.github.io/tui.calendar/latest/Timezone
        /// For serialization purposes
        /// </summary>
        private class TUITimeZone
        {
            /// <summary>
            /// Time Zone Name
            /// </summary>
            public string timezoneName { get; set; }
            /// <summary>
            /// Time Zone Display Label
            /// </summary>
            public string displayLabel { get; set; }
            /// <summary>
            /// Time Zone Tool Tip
            /// </summary>
            public string tooltip { get; set; }
        }
    }
}
