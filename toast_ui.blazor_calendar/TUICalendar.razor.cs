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
using System.Diagnostics;

namespace toast_ui.blazor_calendar
{
    public partial class TUICalendar : ComponentBase, IDisposable
    {

        public TUICalendarInteropService CalendarInterop { get; private set; } = null;

        [Inject]
        public  IJSRuntime jsRuntime { get; set; }

        [Parameter]
        public EventCallback<TUISchedule> OnCalendarEventOrTaskChanged { get; set; }

        [Parameter]
        public EventCallback<TUISchedule> OnCalendarEventOrTaskCreated { get; set; }

        [Parameter]
        public EventCallback<string> OnClickCalendarEventOrTask { get; set; }

        /// <summary>
        /// Not Working
        /// </summary>
        [Parameter]
        public EventCallback<string> OnDoubleClickCalendarEventOrTask { get; set; }

        [Parameter]
        public EventCallback<string> OnDeleteCalendarEventOrTask { get; set; }

        /// <summary>
        /// Call this method and Advance the calendar, in any view, forward,backward, or to today.
        /// </summary>
        /// <param name="moveTo">Previous, Next, or Today</param>
        /// <returns></returns>
        public async ValueTask MoveCalendar(CalendarMove moveTo)
        {
            await CalendarInterop.MoveCalendar(moveTo);
        }

        /// <summary>
        /// IEnumerable of all events/tasks etc of type TUISchedule.
        /// The initial set to be loaded
        /// </summary>
        [Parameter]
        public IEnumerable<TUISchedule> Schedules { get; set; }

        /// <summary>
        /// Calendar display options and defaults, can be null
        /// </summary>
        [Parameter]
        public TUICalendarOptions CalendarOptions { get; set; } = null;

        /// <summary>
        /// Calendar Properties for each calendar, i.e. colors, name, etc
        /// </summary>
        [Parameter]
        public IEnumerable<TUICalendarProps> CalendarProperties { get; set; } = null;

        private TUICalendarViewName _CalendarViewName;

        /// <summary>
        /// Change the Calendar View Mode to Day, Week, or Month View
        /// The initial setting of this parameter has no affect.
        /// The calendar Options initial view will override this
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
        
        /// <summary>
        /// Used to Queue events in SetParametersAsync. Code cannot be left until after all parameters have been set
        /// </summary>
        private Queue<ValueTask> _OnParameterChangeEvents = new Queue<ValueTask>();

        [JSInvokable("UpdateSchedule")]
        public async Task UpdateSchedule(dynamic scheduleBeingModified, dynamic updatedScheduleFields)
        {
            //var currentSchedule = Schedules.Where(x => x.id == scheduleId).FirstOrDefault();
            var currentSchedule = JsonSerializer.Deserialize<TUISchedule>(scheduleBeingModified.ToString());
            var updatedSchedule = CalendarInterop.UpdateSchedule(currentSchedule, updatedScheduleFields); //Todo: Combine changes with actual schedule
            await OnCalendarEventOrTaskChanged.InvokeAsync(updatedSchedule); //Todo: Test This callback!
            Debug.WriteLine($"Schedule {currentSchedule.Id} Modified");
        }

        [JSInvokable("CreateSchedule")]
        public async Task CreateSchedule(JsonElement newSchedule)
        {
            var schedule = JsonSerializer.Deserialize<TUISchedule>(newSchedule.ToString());
            Schedules.ToList().Add(schedule);
            await OnCalendarEventOrTaskCreated.InvokeAsync(schedule);
            Debug.WriteLine("New Schedule Created");
        }
        
        [JSInvokable("OnClickSchedule")]
        public async Task OnScheduleClick(string scheduleId)
        {
            await OnClickCalendarEventOrTask.InvokeAsync(scheduleId);
            Debug.WriteLine($"Schedule {scheduleId} Clicked!");
        }

        [JSInvokable("OnDoubleClickSchedule")]
        public async Task OnScheduleDoubleClick(string scheduleId)
        {
            await OnDoubleClickCalendarEventOrTask.InvokeAsync(scheduleId);
            Debug.WriteLine($"Schedule {scheduleId} Double-Clicked!");
        }

        [JSInvokable("DeleteSchedule")]
        public async Task OnDeleteSchedule(string scheduleId)
        {
            await OnDeleteCalendarEventOrTask.InvokeAsync(scheduleId);
            Debug.WriteLine($"Schedule {scheduleId} Deleted!");
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
    /// <summary>
    /// Enum Used to Advance the Calendar Forward, Reverse, or to Today
    /// </summary>
    public enum CalendarMove
    {
        Next,
        Previous,
        Today
    }
}
