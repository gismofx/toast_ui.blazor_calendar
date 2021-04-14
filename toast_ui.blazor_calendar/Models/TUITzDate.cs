using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using toast_ui.blazor_calendar.Services;

namespace toast_ui.blazor_calendar.Models
{
    public class TUITzDate
    {
        private  DateTimeOffset? _dateInternal;

        [JsonConverter(typeof(TZDateJsonConverter))]
        public  DateTimeOffset? _date
        {
            get { return _dateInternal; }
            set { _dateInternal = value; }
        }

        //@Todo: How to enable without breaking deserialization?
        /*
        public TUITzDate(DateTimeOffset? date=null)
        {
            _date = date;
        }
       */

    }
}
