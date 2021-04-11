using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toast_ui.blazor_calendar.Models;
using Bogus;

namespace toast_ui.blazor_calendar.TestProject.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {

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

        public async Task InitCalendarDataAsync()
        {
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
            
            var startDate = faker.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(30));
            var endDate = startDate.AddMinutes(faker.Random.Int(15, 300));
            var sched = new TUISchedule()
            {
                id = Guid.NewGuid().ToString(),
                calendarId = "1",
                start = startDate,
                end = endDate,
                title = faker.Lorem.Words(faker.Random.Int(3, 8)).ToString(),
                body = faker.Lorem.Paragraph(3)
            };

            return sched;

        }
    }
}
