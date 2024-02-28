﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Microsoft.Extensions.Logging;
using toast_ui.blazor_calendar.Models;
using toast_ui.blazor_calendar.Services;
using System.Diagnostics;

namespace toast_ui.blazor_calendar.TestProjectWithMudBlazor.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        private readonly ITUICalendarInteropService _CalendarService;

        public CalendarViewModel(ITUICalendarInteropService calendarService)
        {
            _CalendarService = calendarService;
        }

        private List<IEventObject> _Schedules;

        public List<IEventObject> Schedules
        {
            get => _Schedules;
            set
            {
                SetValue(ref _Schedules, value);
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

        private IEnumerable<TUICalendarProps> _CalendarProps;

        public IEnumerable<TUICalendarProps> CalendarProps
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
                visibleWeeksCount = 6,
                //visibleScheduleCount = 0,
                
            };

            var weekOptions = new TUIWeekOptions()
            {
                //daynames = new[] { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" },
                narrowWeekend=true,
                //startDayOfWeek = 0,
            };

            //optionally setup timezones for display
            var timeZones = new TUICalendarTimeZoneOption();
            timeZones.AddTimeZone(TimeZoneInfo.Local);

            //Set the Calendar Options
            CalendarOptions = new TUICalendarOptions()
            {
                useFormPopup = true,
                useDetailPopup = true,
                defaultView = TUICalendarViewName.Month,
                gridSelection = new() { enableClick = true , enableDbClick = true},
                //taskView = false,
                //scheduleView = true,
                month = monthOptions,
                week = weekOptions,
                TUITemplate = calendarTemplate,
                timezone = timeZones
            };

            var calendarProps = new List<TUICalendarProps>();
            var calendar1 = new TUICalendarProps()
            {
                id = "1",
                name = "My Test Calendar",
                color = "#ffffff",
                bgColor = "#9e5fff",
                dragBgColor = "#9e5fff",
                borderColor = "#9e5fff"
            };
            calendarProps.Add(calendar1);

            var calendar2 = new TUICalendarProps()
            {
                id = "2",
                name = "My Test Calendar2",
                color = "#ffffff",
                bgColor = "#00a9ff",
                dragBgColor = "#00a9ff",
                borderColor = "#00a9ff"
            };
            calendarProps.Add(calendar2);
            CalendarProps = calendarProps;

            //.calendars = calendarProps;

            await Task.Run(() =>
            {
                _Schedules = new List<IEventObject>();
                for (int i = 0; i < 50; i++)
                {
                    _Schedules.Add(GetFakeSchedule());
                }
            });
        }

        private IEventObject GetFakeSchedule()
        {
            var faker = new Faker();

            var startDate = faker.Date.BetweenOffset(DateTimeOffset.Now.AddDays(-10), DateTimeOffset.Now.AddDays(10));
            var endDate = startDate.AddMinutes(faker.Random.Int(15, 300));
            var sched = new TUIEvent()
            {
                id = Guid.NewGuid().ToString(),
                CalendarId = faker.Random.Int(1, 2).ToString(),
                Start = startDate,
                End = endDate,
                Title = faker.Lorem.Sentence(faker.Random.Int(3, 7)),
                Body = faker.Lorem.Paragraph(3),
                Category = EventCategory.Time,
                IsVisible = true,
                IsAllDay = false,
                State = EventState.Busy
            };

            return sched;
        }

        public async Task OnChangeCalendarEventOrTask(IEventObject eventObject)
        {
            //do something when an event is clicked
            //Show a custom pop up if some conditions are met?
            Debug.WriteLine($"Event or Task Changed: {eventObject.Title}");
            //Simulate long running task
            await Task.Delay(10);
        }

        public async Task OnClickCalendarEventOrTask(string eventId)
        {
            //do something when an event is clicked
            //You can find Event in Database by Id
            Debug.WriteLine($"Event or Task Clicked: {eventId}");
            //Simulate long running task
            await Task.Delay(10);
        }

        public async Task OnCreateCalendarEventOrTask(IEventObject newEvent)
        {
            //Save event to database
            Debug.WriteLine($"Event or Task Created: {newEvent.Title}");
            //Simulate long running task
            await Task.Delay(10);
        }

        public async Task OnDeleteCalendarEventOrTask(string eventId)
        {
            //Delete the event from database
            Debug.WriteLine($"Delete this Event: {eventId}");
            //Simulate long running task
            await Task.Delay(10);
        }

    }
}