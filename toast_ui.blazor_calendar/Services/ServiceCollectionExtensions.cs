using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using toast_ui.blazor_calendar.ThemeTranslator;

namespace toast_ui.blazor_calendar.Services
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the TUIBlazorCalendar services to the IServiceCollection
        /// </summary>
        public static IServiceCollection AddTUIBlazorCalendar(this IServiceCollection services)
        {
            services.AddTransient<ITUICalendarInteropService, TUICalendarInteropService>();
            services.AddScoped<IThemeService, ThemeService>();
            return services;
        }

        public static IServiceCollection AddThemeBinder(this IServiceCollection services, Action<ThemeBinder> configure)
        {
            services.AddScoped(provider =>
            {
                var themeBinder = new ThemeBinder();
                configure(themeBinder);
                return themeBinder;
            });

            return services;
        }
    }
}
