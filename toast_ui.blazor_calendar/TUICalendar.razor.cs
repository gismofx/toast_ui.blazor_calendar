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
using System.Text.Json.Nodes;
using toast_ui.blazor_calendar.Models.Extensions;

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

    public partial class TUICalendar : ComponentBase, INotifyPropertyChanged, IDisposable
    {
        internal static readonly string NotifyUI = "UI";
        
        [Inject] internal IThemeService ThemeService { get; set; }

        public TUICalendar()
        {
            PropertyChanged -= TUICalendar_PropertyChanged;
            PropertyChanged += TUICalendar_PropertyChanged;
        }

        private async void TUICalendar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NotifyUI)
            {
                await CalendarInterop.ChangeTUIEventColors(ThemeService.CurrentTheme.CommonTheme.EventTitleColor.Value.ToHex());
                return;
            }
            
            switch (e.PropertyName)
            {
                case nameof(CalendarOptions):
                    if (CalendarOptions?.Theme != null && ThemeService.CurrentTheme != CalendarOptions.Theme)
                        ThemeService.SetTheme(CalendarOptions.Theme);
                    break;
                default:
                    break;
            }
        }

        private DotNetObjectReference<TUICalendar> _ObjectReference;

        /// <summary>
        /// Used to Queue events in SetParametersAsync. Code cannot be left until after all parameters have been set
        /// </summary>
        private Queue<Task> _OnParameterChangeEvents = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Direct access to some calendar functions via the Interop
        /// </summary>
        [Inject]
        internal ITUICalendarInteropService CalendarInterop { get; private set; } = null;

        [Inject]
        internal IJSRuntime jsRuntime { get; set; }

        private TUICalendarOptions _CalendarOptions = null;
        /// <summary>
        /// Calendar display options and defaults, can be null
        /// </summary>
        [Parameter]
        public TUICalendarOptions CalendarOptions
        {
            get => _CalendarOptions;
            set
            {
                _CalendarOptions = value;
                Notify(nameof(CalendarOptions));
            }
        }

        public EventCallback<TUICalendarOptions> CalendarOptionsChanged { get; set; }

        /// <summary>
        /// Calendar Properties for each calendar, i.e. colors, name, etc
        /// </summary>
        [Parameter]
        public IEnumerable<CalendarInfo> CalendarProperties { get; set; } = null;

        /// <summary>
        /// One-Way bind to this value to change/jump to any date
        /// which will be made visible for any given calendar view name
        /// Initial setting of this parameter will have no affect during 
        /// loading of this component
        /// </summary>
        //[Parameter]
        //public DateTimeOffset? GoToDate { get; set; }



        /// <summary>
        /// Invoked when a calendar Event or Task is changed
        /// </summary>
        [Parameter]
        public EventCallback<TUIEvent> OnChangeCalendarEventOrTask { get; set; }

        /// <summary>
        /// Invoked when a calendar Event or Task is Clicked
        /// </summary>
        [Parameter]
        public EventCallback<string> OnClickCalendarEventOrTask { get; set; }

        /// <summary>
        /// Raised when a calendar Event or Task is Created
        /// </summary>
        [Parameter]
        public EventCallback<TUIEvent> OnCreateCalendarEventOrTask { get; set; }

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
        /// IEnumerable of all events/tasks etc of type TUIEvents.
        /// This is the initial set of events to be loaded
        /// </summary>
        [Parameter]
        public ICollection<TUIEvent> Events { get; set; }

        /// <summary>
        /// The End Date of the Range of days displayed on the calendar
        /// </summary>
        [Parameter]
        public DateTimeOffset? VisibleEndDateRange { get; set; }

        /// <summary>
        /// The End Date of the Range of days displayed on the calendar
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
        /// Any time a new parameter is added, it must be MANUALLY set here
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override async Task SetParametersAsync(ParameterView parameters)
        {

            //Use Interop from other code this function is out of scope
            //var viewName = parameters.GetValueOrDefault<TUICalendarViewName>("CalendarViewName");
            //if (viewName != CalendarViewName)
            //{
            //    CalendarViewName = viewName;
            //    _OnParameterChangeEvents.Enqueue(CalendarInterop?.ChangeView(viewName).AsTask());
            //    _OnParameterChangeEvents.Enqueue(SetDateRange());
            //}

            //var newDateDisplay = parameters.GetValueOrDefault<DateTimeOffset?>("GoToDate");
            //if (newDateDisplay != GoToDate)
            //{
            //    GoToDate = newDateDisplay;
            //    _OnParameterChangeEvents.Enqueue(CalendarInterop?.SetDate(newDateDisplay).AsTask());
            //    _OnParameterChangeEvents.Enqueue(SetDateRange());
            //}

            var calendarOptions = parameters.GetValueOrDefault<TUICalendarOptions>("CalendarOptions");
            if (calendarOptions is not null)
            {
                if (!calendarOptions.Equals(CalendarOptions))
                {
                    CalendarOptions = calendarOptions;
                    _OnParameterChangeEvents.Enqueue(CalendarInterop?.SetCalendarOptions(calendarOptions).AsTask());
                    _OnParameterChangeEvents.Enqueue(SetDateRange());
                }
            }
            CalendarProperties = parameters.GetValueOrDefault<IEnumerable<CalendarInfo>>("CalendarProperties");
            Events = parameters.GetValueOrDefault<ICollection<TUIEvent>>("Events");

            //Visible Date Range
            VisibleEndDateRange = parameters.GetValueOrDefault<DateTimeOffset?>("VisibleEndDateRange");
            VisibleStartDateRange = parameters.GetValueOrDefault<DateTimeOffset?>("VisibleStartDateRange");
            VisibleStartDateRangeChanged = parameters.GetValueOrDefault<EventCallback<DateTimeOffset?>>("VisibleStartDateRangeChanged");
            VisibleEndDateRangeChanged = parameters.GetValueOrDefault<EventCallback<DateTimeOffset?>>("VisibleEndDateRangeChanged");

            //Events
            OnChangeCalendarEventOrTask = parameters.GetValueOrDefault<EventCallback<TUIEvent>>("OnChangeCalendarEventOrTask");
            OnCreateCalendarEventOrTask = parameters.GetValueOrDefault<EventCallback<TUIEvent>>("OnCreateCalendarEventOrTask");
            OnClickCalendarEventOrTask = parameters.GetValueOrDefault<EventCallback<string>>("OnClickCalendarEventOrTask");
            OnDeleteCalendarEventOrTask = parameters.GetValueOrDefault<EventCallback<string>>("OnDeleteCalendarEventOrTask");

            await base.SetParametersAsync(ParameterView.Empty);

        }

        /*
        devV2
                /// <param name="changedSchedule">The changes made to the schedule</param>
                public async Task UpdateSchedule(TUISchedule changedSchedule)
                {
                    await CalendarInterop.UpdateSchedule(changedSchedule); 
                    await OnChangeCalendarEventOrTask.InvokeAsync(changedSchedule);
                    Debug.WriteLine($"Schedule {changedSchedule.id} Modified");

        */

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
                await CalendarInterop.CreateEventsAsync(Events);
                Notify(NotifyUI);
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
            _OnParameterChangeEvents.Clear();
            //ToDo: What am I thinking here?
            //if (CalendarInterop is not null)
            //{
            //    while (_OnParameterChangeEvents.Any())
            //    {
            //        try
            //        {
            //            Task ev = _OnParameterChangeEvents.Dequeue();
            //            await ev;
            //        }
            //        catch (NullReferenceException)
            //        {
            //            //do nothing
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Since there is no subsequent rendering required by blazor after the first render, this must be set to false
        /// </summary>
        /// <returns></returns>
        protected override bool ShouldRender() => false;


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
