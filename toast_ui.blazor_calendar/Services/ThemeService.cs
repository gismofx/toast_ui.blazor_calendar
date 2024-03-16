using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using toast_ui.blazor_calendar.Models;
using toast_ui.blazor_calendar.Models.Extensions;

namespace toast_ui.blazor_calendar.Services
{
    public interface IThemeService
    {
        TUITheme CurrentTheme { get; set; }
        void SetTheme(TUITheme theme);
    }



    public class ThemeService : IThemeService
    {
        private ITUICalendarInteropService TUICalendarInteropService { get; set; }

        public TUITheme CurrentTheme { get; set; }

        public ThemeService(IServiceProvider provider)
        {
            TUICalendarInteropService = provider.GetService<ITUICalendarInteropService>();
        }

        public void SetTheme(TUITheme theme)
        {
            CurrentTheme = theme;
            TUICalendarInteropService.SetTheme(theme);
        }
    }
}
