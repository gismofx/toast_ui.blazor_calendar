using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toast_ui.blazor_calendar.Services;
using toast_ui.blazor_calendar.Models;
using Bogus;

namespace toast_ui.blazor_calendar.TestProject.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {

        private readonly TUICalendarInteropService CalendarService;
        public CalendarViewModel(TUICalendarInteropService calendarService)
        {
            CalendarService = calendarService;
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


        public async Task InitCalendarDataAsync()
        {
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
            
            var startDate = faker.Date.Between(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10));
            var endDate = startDate.AddMinutes(faker.Random.Int(15, 300));
            var sched = new TUISchedule()
            {
                id = Guid.NewGuid().ToString(),
                calendarId = "1",
                start = startDate,
                end = endDate,
                title = faker.Lorem.Sentence(faker.Random.Int(3,7)),
                body = faker.Lorem.Paragraph(3),
                category = "time",
                isVisible = true
                
            };

            return sched;

        }
    }
}
