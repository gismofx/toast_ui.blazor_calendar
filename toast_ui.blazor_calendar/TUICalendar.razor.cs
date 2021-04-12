using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using toast_ui.blazor_calendar.Services;
using Microsoft.JSInterop;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar
{
    public partial class TUICalendar : ComponentBase, IDisposable
    {

        [Inject]
        private TUICalendarInteropService CalendarInterop { get; set; }

        /// <summary>
        /// IEnumerable of all events/tasks etc of type TUISchedule
        /// </summary>
        [Parameter]
        public IEnumerable<TUISchedule> Schedules { get; set; }

        /// <summary>
        /// Calendar display options and defaults, can be null
        /// </summary>
        [Parameter]
        public TUICalendarOptions CalendarOptions { get; set; } = null;

        /// <summary>
        /// Calendar Properties
        /// </summary>
        [Parameter]
        public IEnumerable<TUICalendarProps> CalendarProperties { get; set; } = null;

        private TUICalendarViewName _CalendarViewName;

        /// <summary>
        /// Day, Week, or Month View
        /// </summary>
        [Parameter]
        public TUICalendarViewName CalendarViewName
        {
            get => _CalendarViewName;
            set
            {
                if (_CalendarViewName == value) return;
                _CalendarViewName = value;
                CalendarViewNameChanged.InvokeAsync(value);
                CalendarInterop.ChangeView(value);
            }
        }   

        [Parameter]
        public EventCallback<TUICalendarViewName> CalendarViewNameChanged { get; set; }

        /*
        private DateTime? _SearchDateStart;
        [Parameter]
        public DateTime? SearchDateStart
        {
            get => _SearchDateStart;
            set
            {
                if (_SearchDateStart == value) return;
                _SearchDateStart = value;
                SearchDateStartChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<DateTime?> SearchDateStartChanged { get; set; }
        */

        private DotNetObjectReference<TUICalendar> _ObjectReference;

        protected override void OnInitialized()
        {
            _ObjectReference = DotNetObjectReference.Create(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CalendarInterop.InitCalendarAsync(_ObjectReference);
                await CalendarInterop.SetCalendars(CalendarProperties);
                await CalendarInterop.CreateSchedulesAsync(Schedules);
            }
        }

        [JSInvokable("UpdateSchedule")]
        public async Task UpdateSchedule(string scheduleId, dynamic updatedScheduleFields)
        {
            Console.WriteLine("Test");
            //await CalendarInterop.UpdateSchedule(updatedSchedule);
        }

        public TUICalendar()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (_ObjectReference != null)
            {
                //Now dispose our object reference so our component can be garbage collected
                _ObjectReference.Dispose();
            }
        }
    }
}
