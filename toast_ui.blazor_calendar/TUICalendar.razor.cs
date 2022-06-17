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
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace toast_ui.blazor_calendar
{
    /// <summary>
    /// Enum Used to Advance the Calendar Forward, Reverse, or to Today
    /// </summary>
    public enum CalendarMove
    {
        Next,
        Previous,
        Today
    }

    public partial class TUICalendar : ComponentBase, IDisposable
    {

        private DotNetObjectReference<TUICalendar> _ObjectReference;
        
        /// <summary>
        /// Used to Queue events in SetParametersAsync. Code cannot be left until after all parameters have been set
        /// </summary>
        private Queue<Task> _OnParameterChangeEvents = new();

        /// <summary>
        /// Direct access to some calendar functions via the Interop
        /// </summary>
        public TUICalendarInteropService CalendarInterop { get; private set; } = null;

        /// <summary>
        /// Calendar display options and defaults, can be null
        /// </summary>
        [Parameter]
        public TUICalendarOptions CalendarOptions { get; set; } = null;

        public EventCallback<TUICalendarOptions> CalendarOptionsChanged { get; set; }

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

        [Inject]
        public IJSRuntime jsRuntime { get; set; }

        /// <summary>
        /// Invoked when a calendar Event or Task is changed
        /// </summary>
        [Parameter]
        public EventCallback<TUISchedule> OnChangeCalendarEventOrTask { get; set; }

        /// <summary>
        /// Invoked when a calendar Event or Task is Clicked
        /// </summary>
        [Parameter]
        public EventCallback<string> OnClickCalendarEventOrTask { get; set; }

        /// <summary>
        /// Raised when a calendar Event or Task is Created
        /// </summary>
        [Parameter]
        public EventCallback<TUISchedule> OnCreateCalendarEventOrTask { get; set; }
        
        /// <summary>
        /// Raised when a calendar Event or Task is Deleted
        /// </summary>
        [Parameter]
        public EventCallback<string> OnDeleteCalendarEventOrTask { get; set; }

        /// <summary>
        /// Not Working
        /// </summary>
        /*
        [Parameter]
        public EventCallback<string> OnDoubleClickCalendarEventOrTask { get; set; }
        */

        /// <summary>
        /// IEnumerable of all events/tasks etc of type TUISchedule.
        /// This is the initial set of schedules/events to be loaded
        /// </summary>
        [Parameter]
        public ICollection<TUISchedule> Schedules { get; set; }
        
        /// <summary>
        /// The End Date of the Range of days displated on the calendar
        /// </summary>
        [Parameter]
        public DateTimeOffset? VisibleEndDateRange { get; set; }

        /// <summary>
        /// The End Date of the Range of days displated on the calendar
        /// </summary>
        [Parameter]
        public EventCallback<DateTimeOffset?> VisibleEndDateRangeChanged { get; set; }

        /// <summary>
        /// The Start Date of the Range of days displayed on the calendar
        /// </summary>
        [Parameter]
        public DateTimeOffset? VisibleStartDateRange { get; set; }
        
        /// <summary>
        /// The Start Date of the Range of days displayed on the calendar
        /// </summary>
        [Parameter]
        public EventCallback<DateTimeOffset?> VisibleStartDateRangeChanged { get; set; }
        
        /// <summary>
        /// When the user created a new schedule from the UI
        /// </summary>
        /// <param name="newSchedule"></param>
        /// <returns></returns>
        [JSInvokable("CreateSchedule")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task CreateSchedule(JsonElement newSchedule)
        {
            var schedule = JsonSerializer.Deserialize<TUISchedule>(newSchedule.ToString());
            Schedules.Add(schedule);
            await OnCreateCalendarEventOrTask.InvokeAsync(schedule);
            Debug.WriteLine("New Schedule Created");
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

        /// <summary>
        /// Clears all schedules from the calendar.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async ValueTask ClearCalendar()
        {
            await CalendarInterop.Clear();
        }

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
        
        /// <summary>
        /// When a schedule is deleted from calendar UI, this is invoked        
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        [JSInvokable("DeleteSchedule")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task OnDeleteSchedule(string scheduleId)
        {
            await OnDeleteCalendarEventOrTask.InvokeAsync(scheduleId);
            Debug.WriteLine($"Schedule {scheduleId} Deleted!");
        }

        /// <summary>
        /// When a schedule is clicked from the calendar UI, this is invoked
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        [JSInvokable("OnClickSchedule")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task OnScheduleClick(string scheduleId)
        {
            await OnClickCalendarEventOrTask.InvokeAsync(scheduleId);
            Debug.WriteLine($"Schedule {scheduleId} Clicked!");
        }

        /// <summary>
        /// Any time a new parameter is added, it must be MANUALLY set here
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            var viewName = parameters.GetValueOrDefault<TUICalendarViewName>("CalendarViewName");
            if (viewName != CalendarViewName)
            {
                CalendarViewName = viewName;
                _OnParameterChangeEvents.Enqueue(CalendarInterop?.ChangeView(viewName).AsTask());
                _OnParameterChangeEvents.Enqueue(SetDateRange());
            }

            var newDateDisplay = parameters.GetValueOrDefault<DateTimeOffset?>("GoToDate");
            if (newDateDisplay != GoToDate)
            {
                GoToDate = newDateDisplay;
                _OnParameterChangeEvents.Enqueue(CalendarInterop?.SetDate(newDateDisplay).AsTask());
                _OnParameterChangeEvents.Enqueue(SetDateRange());
            }

            var calendarOptions = parameters.GetValueOrDefault<TUICalendarOptions>("CalendarOptions");
            if (calendarOptions is not null)
            {
                if (!calendarOptions.Equals(CalendarOptions))
                {
                    CalendarOptions = calendarOptions;
                    _OnParameterChangeEvents.Enqueue(CalendarInterop?.SetCalendarOptionsAsync(calendarOptions).AsTask());
                    _OnParameterChangeEvents.Enqueue(SetDateRange());
                }
            }
            CalendarProperties = parameters.GetValueOrDefault<IEnumerable<TUICalendarProps>>("CalendarProperties");
            Schedules = parameters.GetValueOrDefault<ICollection<TUISchedule>>("Schedules");

            //Visible Date Range
            VisibleEndDateRange = parameters.GetValueOrDefault<DateTimeOffset?>("VisibleEndDateRange");
            VisibleStartDateRange = parameters.GetValueOrDefault<DateTimeOffset?>("VisibleStartDateRange");
            VisibleStartDateRangeChanged = parameters.GetValueOrDefault<EventCallback<DateTimeOffset?>>("VisibleStartDateRangeChanged");
            VisibleEndDateRangeChanged = parameters.GetValueOrDefault<EventCallback<DateTimeOffset?>>("VisibleEndDateRangeChanged");

            //Events
            OnChangeCalendarEventOrTask = parameters.GetValueOrDefault<EventCallback<TUISchedule>>("OnChangeCalendarEventOrTask");
            OnCreateCalendarEventOrTask = parameters.GetValueOrDefault<EventCallback<TUISchedule>>("OnCreateCalendarEventOrTask");
            OnClickCalendarEventOrTask = parameters.GetValueOrDefault<EventCallback<string>>("OnClickCalendarEventOrTask");
            OnDeleteCalendarEventOrTask = parameters.GetValueOrDefault<EventCallback<string>>("OnDeleteCalendarEventOrTask");

            await base.SetParametersAsync(ParameterView.Empty);

        }

        /// <summary>
        /// When an schedule is updated from the UI, this is invoked.
        /// </summary>
        /// <param name="scheduleBeingModified"></param>
        /// <param name="updatedScheduleFields"></param>
        /// <returns></returns>
        [JSInvokable("UpdateSchedule")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task UpdateSchedule(dynamic scheduleBeingModified, dynamic updatedScheduleFields)
        {
            var currentSchedule = JsonSerializer.Deserialize<TUISchedule>(scheduleBeingModified.ToString());
            var updatedSchedule = CalendarInterop.UpdateSchedule(currentSchedule, updatedScheduleFields); //Todo: Combine changes with actual schedule
            await OnChangeCalendarEventOrTask.InvokeAsync(updatedSchedule); //Todo: Test This callback!
            Debug.WriteLine($"Schedule {currentSchedule.id} Modified");
        }
        
        /*@Todo: Waiting for Double click in TUI API
        [JSInvokable("OnDoubleClickSchedule")]
        public async Task OnScheduleDoubleClick(string scheduleId)
        {
            await OnDoubleClickCalendarEventOrTask.InvokeAsync(scheduleId);
            Debug.WriteLine($"Schedule {scheduleId} Double-Clicked!");
        }
        */
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CalendarInterop.InitCalendarAsync(_ObjectReference, CalendarOptions);
                await CalendarInterop.SetCalendars(CalendarProperties);
                await CalendarInterop.CreateSchedulesAsync(Schedules);
                await SetDateRange();
            }
        }

        protected override void OnInitialized()
        {
            _ObjectReference = DotNetObjectReference.Create(this);
            if (CalendarInterop is null)
            {
                CalendarInterop = new TUICalendarInteropService(jsRuntime);
            }
        }
        protected override async Task OnParametersSetAsync()
        {
            if (CalendarInterop is not null)
            {
                while (_OnParameterChangeEvents.Count > 0)
                {
                    try
                    {
                        await _OnParameterChangeEvents.Dequeue();
                    }
                    catch (NullReferenceException)
                    {
                        //do nothing
                    }
                }
            }
        }

        /// <summary>
        /// Since there is no subsequent rendering required by blazor after the first render, this set to false
        /// </summary>
        /// <returns></returns>
        protected override bool ShouldRender()
        {
            return false;
        }
        
        /// <summary>
        /// Each time there is a view change or advance of the calendar, ask the calendar what date range is visible
        /// </summary>
        /// <returns></returns>
        private async Task SetDateRange()
        {
            if (CalendarInterop is not null)
            {
                await VisibleStartDateRangeChanged.InvokeAsync(await CalendarInterop.GetDateRangeStart());
                await VisibleEndDateRangeChanged.InvokeAsync(await CalendarInterop.GetDateRangeEnd());
            }
        }
    }
}

//SOME REFERENCE CODE
/*
 * if (!_hasSetInitialParameters)
            {
                // This is the first run
                // Could put this logic in OnInit, but its nice to avoid forcing people who override OnInit to call base.OnInit()

                if (ValueExpression != null)
                {
                    FieldIdentifier = FieldIdentifier.Create(ValueExpression);
                }

                //EditContext = CascadedEditContext;
                _nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TUICalendar));
                _hasSetInitialParameters = true;

                //LogMBDebug($"SetParametersAsync setting ComponentValue value to '{Value?.ToString() ?? "null"}'");

                _cachedValue = Value;
                _componentValue = Value;
            }

private Type _nullableUnderlyingType;

        /// <summary>
        /// Gets or sets an expression that identifies the bound value.
        /// </summary>
        [Parameter] public Expression<Func<TUICalendar>> ValueExpression { get; set; }

        /// <summary>
        /// Gets the <see cref="FieldIdentifier"/> for the bound value.
        /// </summary>
        protected FieldIdentifier FieldIdentifier { get; private set; }

        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        /// <example>
        /// @bind-Value="@model.PropertyName"
        /// </example>
        [Parameter] public TUICalendar Value { get; set; }
        private TUICalendar _cachedValue;

        private TUICalendar _componentValue;

        private bool _hasSetInitialParameters;

*/
