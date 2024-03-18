using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar.ThemeTranslator
{
    public class ThemeBinder
    {
        private readonly Dictionary<string, object> _themeBindings = new Dictionary<string, object>();

        public ThemeBinder()
        {
        }

        public void AddThemeBinding(string sourceTheme, Action<object> updateAction)
        {
            _themeBindings[sourceTheme] = updateAction;
        }

        public void ChangeTheme(string sourceTheme)
        {
            //if (_themeBindings.TryGetValue(sourceTheme, out var updateAction))
            //{
            //    updateAction.Invoke(sourceTheme);
            //}
            //else
            //{
            //    Console.WriteLine($"No theme binding found for '{sourceTheme}'.");
            //}
        }
    }

    public interface ITranslator
    {
        TUITheme Translate();
    }  
}
