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
        public TUICalendarInteropService CalendarInterop { get; private set; } = null;

        [Inject]
        public  IJSRuntime jsRuntime { get; set; }

        [Parameter]
        public EventCallback<TUISchedule> OnCalendarEventOrTaskChanged { get; set; }

        [Parameter]
        public EventCallback<TUISchedule> OnCalendarEventOrTaskCreated { get; set; }

        [Parameter]
        public EventCallback<string> OnClickCalendarEventOrTask { get; set; }

        [Parameter]
        public EventCallback<string> OnDeleteCalendarEventOrTask { get; set; }

        public async ValueTask MoveCalendar(CalendarMove moveTo)
        {
            await CalendarInterop.MoveCalendar(moveTo);
        }

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


        [JSInvokable("UpdateSchedule")]
        public async Task UpdateSchedule(string scheduleId, dynamic updatedScheduleFields)
        {
            var currentSchedule = Schedules.Where(x => x.id == scheduleId).FirstOrDefault();
            var updatedSchedule = CombineTuiSchedule(currentSchedule, updatedScheduleFields); //Todo: Combine changes with actual schedule
            await OnCalendarEventOrTaskChanged.InvokeAsync(updatedSchedule); //Todo: Test This callback!
            Console.WriteLine($"Schedule {scheduleId} Modified");
        }

        [JSInvokable("CreateSchedule")]
        public async Task CreateSchedule(JsonElement newSchedule)
        {
            var schedule = JsonSerializer.Deserialize<TUISchedule>(newSchedule.ToString());
            await OnCalendarEventOrTaskCreated.InvokeAsync(schedule);
            Console.WriteLine("New Schedule Created");
        }
        
        [JSInvokable("OnClickSchedule")]
        public async Task OnScheduleClick(string scheduleId)
        {
            await OnClickCalendarEventOrTask.InvokeAsync(scheduleId);
            Console.WriteLine($"Schedule {scheduleId} Clicked!");
        }

        [JSInvokable("DeleteSchedule")]
        public async Task OnDeleteSchedule(string scheduleId)
        {
            await OnDeleteCalendarEventOrTask.InvokeAsync(scheduleId);
            Console.WriteLine($"Schedule {scheduleId} Deleted!");
        }

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


        //Todo: Refactor or move to service?
        //Todo: Bug when modifying a create event from UI
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

    public enum CalendarMove
    {
        Next,
        Previous,
        Today
    }
}
