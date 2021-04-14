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
using System.Text.Json;

namespace toast_ui.blazor_calendar
{
    public partial class TUICalendar : ComponentBase, IDisposable
    {

        //[Inject]
        private TUICalendarInteropService CalendarInterop { get; set; } = null;

        [Inject]
        public  IJSRuntime jsRuntime { get; set; }

        [Parameter]
        public EventCallback<TUISchedule> OnCalendarEventOrTaskChanged { get; set; }

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
            }
        }   


        private DotNetObjectReference<TUICalendar> _ObjectReference;
        private Queue<ValueTask> _OnParameterChangeEvents = new Queue<ValueTask>();

        protected override void OnInitialized()
        {
            _ObjectReference = DotNetObjectReference.Create(this);
            if (CalendarInterop is null)
            {
                CalendarInterop = new TUICalendarInteropService(jsRuntime);
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            //var changeQueue = new Queue<ValueTask>();
            var viewName = parameters.GetValueOrDefault<TUICalendarViewName>("CalendarViewName");
            if (viewName != CalendarViewName)
            {
                _OnParameterChangeEvents.Enqueue(CalendarInterop.ChangeView(viewName));
            }
            CalendarProperties = parameters.GetValueOrDefault<IEnumerable<TUICalendarProps>>("CalendarProperties");
            CalendarOptions = parameters.GetValueOrDefault<TUICalendarOptions>("CalendarOptions");
            Schedules = parameters.GetValueOrDefault<IEnumerable<TUISchedule>>("Schedules");

            await base.SetParametersAsync(ParameterView.Empty);

        }

        protected override async Task OnParametersSetAsync()
        {
            if (CalendarInterop is not null)
            {
                while (_OnParameterChangeEvents.Count > 0)
                {
                    await InvokeAsync(_OnParameterChangeEvents.Dequeue().AsTask);
                }
            }
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
            var curentSchedule = Schedules.Where(x => x.id == scheduleId).FirstOrDefault();
            var updatedSchedule = CombineTuiSchedule(curentSchedule, updatedScheduleFields); //Todo: Combine changes with actual schedule
            OnCalendarEventOrTaskChanged.InvokeAsync(updatedSchedule); //Todo: Test This callback!
        }

        //Todo: Refactor or move to service?
        private TUISchedule CombineTuiSchedule(TUISchedule schedule, JsonElement changes )
        {
            var c = JsonSerializer.Deserialize<TUISchedule>(changes.ToString());
            CopyValues(schedule, c);
            return schedule;
        }

        //Todo: Refactor
        public void CopyValues<T>(T target, T source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
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
