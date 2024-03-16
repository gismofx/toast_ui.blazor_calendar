﻿using System;
using Bogus;
using System.Diagnostics;
using toast_ui.blazor_calendar.Models;
using System.Drawing;
using toast_ui.blazor_calendar.Helpers;
using System.Reflection;
using Microsoft.Extensions.Options;
using MudBlazor;
using Color = System.Drawing.Color;
using Microsoft.AspNetCore.Hosting.Server;


namespace toast_ui.blazor_calendar.TestProjectMudBlazorServerSide.ViewModels;

public class CalendarViewModel : BaseViewModel
{
    private IDialogService _dialogService;

    public CalendarViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public TUICalendar CalendarRef { get; set; }

    public MudBlazor.MudChip[] SelectedCalendars = null;

    private List<TUIEvent> _Events;

    private TUITheme _Theme;

    public bool UseCustomPopup { get; set; } = false;

    public List<TUIEvent> Events
    {
        get => _Events;
        set
        {
            SetValue(ref _Events, value);
        }
    }

    private TUICalendarOptions _CalendarOptions;

    public TUICalendarOptions CalendarOptions
    {
        get => _CalendarOptions;
        set
        {
            SetValue(ref _CalendarOptions, value);
        }
    }

    private IEnumerable<CalendarInfo> _CalendarProps;

    public IEnumerable<CalendarInfo> CalendarProps
    {
        get => _CalendarProps;
        set
        {
            SetValue(ref _CalendarProps, value);
        }
    }

    private TUICalendarViewName _CalendarViewName;

    public TUICalendarViewName CalendarViewName
    {
        get => _CalendarViewName;
        set
        {
            SetValue(ref _CalendarViewName, value);
        }
    }

    private DateTimeOffset? _StartDate;

    public DateTimeOffset? StartDate
    {
        get => _StartDate;
        set
        {
            SetValue(ref _StartDate, value);
        }
    }


    private DateTimeOffset? _EndDate;

    public DateTimeOffset? EndDate
    {
        get => _EndDate;
        set
        {
            SetValue(ref _EndDate, value);
        }
    }

    public async Task InitCalendarDataAsync()
    {
        StartDate = DateTimeOffset.Now;
        EndDate = DateTimeOffset.Now;

        //Todo: Add More Template Examples
        //TUITemplate calendarTemplate = null;
        var calendarTemplate = new TUITemplate();
        calendarTemplate.AddMonthGridHeaderExceed(@"return '<span class=""weekday-grid-more-schedules"">+' + hiddenSchedules + ' says gismofx' + '</span>';");
        //calendarTemplate = null;

        var monthOptions = new TUIMonthOptions()
        {
            //daynames = new[] { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" },
            //startDayOfWeek = 0,
            VisibleWeeksCount = 6,
            //visibleScheduleCount = 0,

        };

        var weekOptions = new TUIWeekOptions()
        {
            //daynames = new[] { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" },
            narrowWeekend = true,
            //startDayOfWeek = 0,
        };

        //optionally setup timezones for display
        var timeZones = new TUICalendarTimeZoneOption();
        //timeZones.AddTimeZone(TimeZoneInfo.Utc);
        timeZones.AddTimeZone(TimeZoneInfo.Local);
        //timeZones.AddTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));

        //Create a Theme
        _Theme = new TUITheme()
        {
            CommonTheme = new CommonTheme()
            {
                BackgroundColor = Color.FromArgb(37, 37, 38),
                Border = $"1px solid {System.Drawing.ColorTranslator.ToHtml(Color.FromArgb(62, 62, 66))}",
                DayName = new() { Color = System.Drawing.Color.White },
                GridSelection = new GridSelectionTheme()
                {
                    BackgroundColor = Color.WhiteSmoke,
                    Border = "1px dotted #515ce6"
                },
                Today = new() { Color = Color.White },
                Saturday = new() { Color = Color.Pink },
                Holiday = new() { Color = Color.Red }
            },
            MonthTheme = new MonthTheme()
            {
                DayName = new()
                {
                    BackgroundColor = Color.FromArgb(30, 30, 30),
                    BorderLeft = null,

                },
                Weekend = new()
                {
                    BackgroundColor = Color.FromArgb(45, 45, 48)
                },
                DayExceptThisMonth = new()
                {
                    Color = Color.FromArgb(179,179,179)
                }
            },
            WeekTheme = null//new WeekTheme()
        };

        //Set the Calendar Options
        CalendarOptions = new TUICalendarOptions()
        {
            UseFormPopup = true,
            UseDetailPopup = true,
            DefaultView = TUICalendarViewName.Month,
            GridSelection = new() { EnableClick = true, EnableDbClick = true },
            //taskView = false,
            //scheduleView = true,
            Month = monthOptions,
            Week = weekOptions,
            TUITemplate = calendarTemplate,
            Timezone = timeZones,
            Theme = _Theme,
        };

        var calendarProps = new List<CalendarInfo>();
        var calendar1 = new CalendarInfo()
        {
            Id = "1",
            Name = "Calendar 1",
            Color = Color.White,
            BackgroundColor = Color.CadetBlue,
            DragBackgroundColor = Color.DarkBlue,
            BorderColor = Color.Black,
        };
        calendarProps.Add(calendar1);

        var calendar2 = new CalendarInfo()
        {
            Id = "2",
            Name = "Calendar 2",
            Color = Color.White,
            BackgroundColor = Color.DarkSeaGreen,
            DragBackgroundColor = Color.DarkGreen,
            BorderColor = Color.Black,
        };
        calendarProps.Add(calendar2);

        var calendar3 = new CalendarInfo()
        {
            Id = "3",
            Name = "Calendar 3",
            Color = Color.White,
            BackgroundColor = Color.MediumPurple,
            DragBackgroundColor = Color.LightPink,
            BorderColor = Color.Black
        };
        calendarProps.Add(calendar3);

        CalendarProps = calendarProps;

        await Task.Run(() =>
        {
            _Events = new List<TUIEvent>();
            for (int i = 0; i < 50; i++)
            {
                _Events.Add(GetFakeEvent());
            }
        });
    }

    /// <summary>
    /// Generates Fake Events
    /// </summary>
    /// <returns></returns>
    private TUIEvent GetFakeEvent()
    {
        var faker = new Faker();

        var startDate = faker.Date.BetweenOffset(DateTimeOffset.Now.AddDays(-10), DateTimeOffset.Now.AddDays(10)).RoundToNearest(new TimeSpan(0, 15, 0));
        var endDate = startDate.AddMinutes(faker.Random.Int(15, 300));
        var sched = new TUIEvent()
        {
            Id = Guid.NewGuid().ToString(),
            CalendarId = faker.Random.Int(1, 3).ToString(),
            Start = startDate,
            End = endDate,
            Title = faker.Lorem.Sentence(faker.Random.Int(3, 7)),
            Body = faker.Lorem.Paragraph(3),
            Category = EventCategory.Time,
            IsVisible = true,
            IsAllDay = false,
            IsReadOnly = false,
            State = EventState.Busy,
        };

        return sched;
    }

    public async Task OnChangeCalendarEventOrTask(TUIEvent schedule)
    {
        //do something when an event is clicked
        //Show a custom pop up if some conditions are met?
        Debug.WriteLine($"Event or Task Changed: {schedule.Title}");
        //Simulate long running task
        await Task.Delay(10);
    }

    public async Task OnClickCalendarEventOrTask(TUIEvent clickedEvent)
    {
        //do something when an event is clicked
        //You can find Event in Database by Id
        Debug.WriteLine($"Event or Task Clicked: {clickedEvent.Id}");
        
        if (UseCustomPopup)
        {

            var parameters = new DialogParameters<Components.Dialogs.CustomPopup> { { x => x.MyEvent, clickedEvent } };

            var dialog = await _dialogService.ShowAsync<Components.Dialogs.CustomPopup>("This is a custom popup", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                Snackbar.Add("Server Deleted", Severity.Success);
                //In a real world scenario we would reload the data from the source here since we "removed" it in the dialog already.
                //Guid.TryParse(result.Data.ToString(), out Guid deletedServer);
                //Servers.RemoveAll(item => item.Id == deletedServer);
            }
        }
        //Simulate long running task
        await Task.Delay(10);


    }

    public async Task OnCreateCalendarEventOrTask(TUIEvent newSchedule)
    {
        //Save event to database
        Debug.WriteLine($"Event or Task Created: {newSchedule.Title}");
        //Simulate long running task
        await Task.Delay(10);
    }

    public async Task OnDeleteCalendarEventOrTask(string EventId)
    {
        //Delete the event from database
        Debug.WriteLine($"Delete this Event: {EventId}");
        //Simulate long running task
        await Task.Delay(10);
    }

    public async ValueTask OnCaldendarChipClick(string calendarId)
    {
        bool visible = false;
        if (SelectedCalendars.Any(x => ((CalendarInfo)(x.Tag)).Id == calendarId))
            visible = true;

        CalendarRef.SetCalendarVisibility(calendarId, visible);
    }

    public async ValueTask ViewChanged(string viewName)
    {
        Enum.TryParse(viewName, out TUICalendarViewName viewAsEnum);
        await CalendarRef.ChangeView(viewAsEnum);
    }

    public async ValueTask SetTheme()
    {
        await CalendarRef.SetTheme(_Theme);
    }

    public async Task UseCustomPopupChange(bool useCustomPopup)
    {
        UseCustomPopup = useCustomPopup; 
        var options = new TUICalendarOptions()
        { UseDetailPopup = !useCustomPopup };
        await CalendarRef.SetCalendarOptions(options);
        //use code to switch to showing custom popup
    }



}