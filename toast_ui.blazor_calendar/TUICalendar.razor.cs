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
        public IJSRuntime jsRuntime { get; set; }

        [Parameter]
        public EventCallback<TUISchedule> OnCalendarEventOrTaskChanged { get; set; }

        [Parameter]
        public EventCallback<TUISchedule> OnCalendarEventOrTaskCreated { get; set; }

        [Parameter]
        public EventCallback<string> OnClickCalendarEventOrTask { get; set; }

        /// <summary>
        /// Not Working
        /// </summary>
        /*
        [Parameter]
        public EventCallback<string> OnDoubleClickCalendarEventOrTask { get; set; }
        */

        [Parameter]
        public EventCallback<string> OnDeleteCalendarEventOrTask { get; set; }

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

        /// <summary>
        /// Change the Calendar View Mode to Day, Week, or Month View
        /// The initial setting of this parameter has no affect.
        /// The calendar Options initial view will override this
        /// </summary>
        [Parameter]
        public TUICalendarViewName CalendarViewName { get; set; }

        /// <summary>
        /// One-Way bind to this value to change/jump to any date
        /// which will be made visible for any given calendar view name
        /// Initial setting of this parameter will have no affect during 
        /// loading of this component
        /// </summary>
        [Parameter]
        public DateTimeOffset? GoToDate { get; set; }


        private DateTimeOffset? _VisibleStartDateRange;

        /// <summary>
        /// The Start Date of the Range of days displayed on the calendar
        /// </summary>
        [Parameter]
        public DateTimeOffset? VisibleStartDateRange
        {
            get => _VisibleStartDateRange;
            set
            {
                if (_VisibleStartDateRange == value) return;
                _VisibleStartDateRange = value;
                VisibleStartDateRangeChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<DateTimeOffset?> VisibleStartDateRangeChanged { get; set; }

        private DateTimeOffset? _VisibleEndDateRange;

        /// <summary>
        /// The End Date of the Range of days displated on the calendar
        /// </summary>
        [Parameter]
        public DateTimeOffset? VisibleEndDateRange
        {
            get => _VisibleEndDateRange;
            set
            {
                if (_VisibleEndDateRange == value) return;
                _VisibleEndDateRange = value;
                VisibleEndDateRangeChanged.InvokeAsync(value);
            }
        }
        
        [Parameter]
        public EventCallback<DateTimeOffset?> VisibleEndDateRangeChanged { get; set; }



        /// <summary>
        /// Call this method and Advance the calendar, in any view, forward,backward, or to today.
        /// </summary>
        /// <param name="moveTo">Previous, Next, or Today</param>
        /// <returns></returns>
        public async ValueTask MoveCalendar(CalendarMove moveTo)
        {
            await CalendarInterop.MoveCalendar(moveTo);
            await SetDateRange();
        }


        private DotNetObjectReference<TUICalendar> _ObjectReference;
        
        /// <summary>
        /// Used to Queue events in SetParametersAsync. Code cannot be left until after all parameters have been set
        /// </summary>
        private Queue<Task> _OnParameterChangeEvents = new Queue<Task>();

        [JSInvokable("UpdateSchedule")]
        public async Task UpdateSchedule(dynamic scheduleBeingModified, dynamic updatedScheduleFields)
        {
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

        /*@Todo: Waiting for Double click in TUI API
        [JSInvokable("OnDoubleClickSchedule")]
        public async Task OnScheduleDoubleClick(string scheduleId)
        {
            await OnDoubleClickCalendarEventOrTask.InvokeAsync(scheduleId);
            Debug.WriteLine($"Schedule {scheduleId} Double-Clicked!");
        }
        */

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
                _OnParameterChangeEvents.Enqueue(CalendarInterop.ChangeView(viewName).AsTask());
                _OnParameterChangeEvents.Enqueue(SetDateRange());
            }
            
            var newDateDisplay = parameters.GetValueOrDefault<DateTimeOffset?>("GoToDate");
            if (newDateDisplay != GoToDate)
            {
                _OnParameterChangeEvents.Enqueue(CalendarInterop.SetDate(newDateDisplay).AsTask());
                _OnParameterChangeEvents.Enqueue(SetDateRange());
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
                    await _OnParameterChangeEvents.Dequeue();
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
                await SetDateRange();
            }
        }

        private async Task SetDateRange()
        {
            VisibleStartDateRange = await CalendarInterop.GetDateRangeStart();
            VisibleEndDateRange = await CalendarInterop.GetDateRangeEnd();
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
