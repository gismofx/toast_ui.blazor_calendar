using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace toast_ui.blazor_calendar.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTUIBlazorCalendar(this IServiceCollection services)
        {
            return services.AddTransient<TUICalendarInteropService>();
        }
    }
}
