using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Microsoft.Extensions.Logging;
using toast_ui.blazor_calendar.Models;
using System.Diagnostics;
using System.Drawing;
using toast_ui.blazor_calendar.Helpers;

namespace toast_ui.blazor_calendar.TestProject.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {

        public CalendarViewModel()
        {
        }

        private List<TUIEvent> _Schedules;

        public List<TUIEvent> Schedules
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
                narrowWeekend=true,
                //startDayOfWeek = 0,
            };

            //optionally setup timezones for display
            var timeZones = new TUICalendarTimeZoneOption();
            //timeZones.AddTimeZone(TimeZoneInfo.Utc);
            timeZones.AddTimeZone(TimeZoneInfo.Local);
            //timeZones.AddTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));

            //Set the Calendar Options
            CalendarOptions = new TUICalendarOptions()
            {
                UseFormPopup = true,
                UseDetailPopup = true,
                DefaultView = TUICalendarViewName.Month,
                GridSelection = new() { EnableClick = true , EnableDbClick = true},
                //taskView = false,
                //scheduleView = true,
                Month = monthOptions,
                Week = weekOptions,
                TUITemplate = calendarTemplate,
                Timezone = timeZones
            };

            var calendarProps = new List<CalendarInfo>();
            var calendar1 = new CalendarInfo()
            {
                Id = "1",
                Name = "My Test Calendar",
                Color = Color.Red,
                BackgroundColor = Color.Aqua,
                DragBackgroundColor = Color.DarkBlue,
                BorderColor = Color.Black,
            };
            calendarProps.Add(calendar1);

            var calendar2 = new CalendarInfo()
            {
                Id = "2",
                Name = "My Test Calendar2",
                Color = Color.Green,
                BackgroundColor = Color.LightGreen,
                DragBackgroundColor = Color.DarkGreen,
                BorderColor = Color.Black,
            };
            calendarProps.Add(calendar2);
            CalendarProps = calendarProps;

            //.calendars = calendarProps;

            await Task.Run(() =>
            {
                _Schedules = new List<TUIEvent>();
                for (int i = 0; i < 50; i++)
                {
                    _Schedules.Add(GetFakeSchedule());
                }
            });
        }

        private TUIEvent GetFakeSchedule()
        {
            var faker = new Faker();

            var startDate = faker.Date.BetweenOffset(DateTimeOffset.Now.AddDays(-10), DateTimeOffset.Now.AddDays(10)).RoundToNearest(new TimeSpan(0,15,0));
            var endDate = startDate.AddMinutes(faker.Random.Int(15, 300));
            var sched = new TUIEvent()
            {
                Id = Guid.NewGuid().ToString(),
                CalendarId = faker.Random.Int(1, 2).ToString(),
                Start = startDate,
                End = endDate,
                Title = faker.Lorem.Sentence(faker.Random.Int(3, 7)),
                Body = faker.Lorem.Paragraph(3),
                Category = EventCategory.Time,
                IsVisible = true,
                IsAllDay = false,
                IsReadOnly = false,
                State = EventState.Busy
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

        public async Task OnClickCalendarEventOrTask(TUIEvent eventClicked)
        {
            //do something when an event is clicked
            //You can find Event in Database by Id
            Debug.WriteLine($"Event or Task Clicked: {eventClicked.Id}");
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

    }
}