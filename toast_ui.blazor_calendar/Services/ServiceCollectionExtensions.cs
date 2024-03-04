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
        /// <summary>
        /// Adds the TUIBlazorCalendar services to the IServiceCollection
        /// </summary>
        public static IServiceCollection AddTUIBlazorCalendar(this IServiceCollection services)
        {
            return services.AddTransient<ITUICalendarInteropService, TUICalendarInteropService>();
        }
    }
}
