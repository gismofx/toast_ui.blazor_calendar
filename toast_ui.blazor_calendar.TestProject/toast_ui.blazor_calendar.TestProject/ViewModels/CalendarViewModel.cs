using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using toast_ui.blazor_calendar.Models;
using toast_ui.blazor_calendar.Services;

namespace toast_ui.blazor_calendar.TestProject.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        private readonly TUICalendarInteropService CalendarService;

        public CalendarViewModel()
        {
        }

        private List<TUISchedule> _Schedules;

        public List<TUISchedule> Schedules
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
            
            var month = new TUIMonthOptions()
            {
                //daynames = new[] { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" },
                //startDayOfWeek = 0,
                visibleWeeksCount = 6,
                //visibleScheduleCount = 0,
            };
            

            //Set the Calendar Options
            CalendarOptions = new TUICalendarOptions()
            {
                useCreationPopup = true,
                useDetailPopup = true,
                defaultView = TUICalendarViewName.Month,
                taskView = false,
                scheduleView = true,
                month = month
                
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

            

            await Task.Run(() =>
            {
                _Schedules = new List<TUISchedule>();
                for (int i = 0; i < 50; i++)
                {
                    _Schedules.Add(GetFakeSchedule());
                }
            });
        }

        private TUISchedule GetFakeSchedule()
        {
            var faker = new Faker();

            var startDate = faker.Date.BetweenOffset(DateTimeOffset.Now.AddDays(-10), DateTimeOffset.Now.AddDays(10));
            var endDate = startDate.AddMinutes(faker.Random.Int(15, 300));
            var sched = new TUISchedule()
            {
                id = Guid.NewGuid().ToString(),
                calendarId = faker.Random.Int(1, 2).ToString(),
                start = startDate,
                end = endDate,
                title = faker.Lorem.Sentence(faker.Random.Int(3, 7)),
                body = faker.Lorem.Paragraph(3),
                category = "time",
                isVisible = true,
                isAllDay = false,
                state = "busy"
            };

            return sched;
        }
    }
}