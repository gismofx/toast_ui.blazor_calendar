using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar.ThemeTranslator
{
    public class ThemeBinder
    {

        internal delegate void ThemeUpdateAction(string themeName, TUITheme theme);
        internal event ThemeUpdateAction OnThemeUpdate;

        private readonly Dictionary<string, object> _themeBindings = new Dictionary<string, object>();

        public ThemeBinder()
        {
        }

        public void AddThemeBinding(string themeName, object theme)
        {
            _themeBindings[themeName] = theme;
        }

        /// <summary>
        /// This needs to be called, when the theme of as example MudBlazor changes!
        /// </summary>
        /// <param name="themeName">The Theme name that was previously specified.</param>
        public void UpdateTheme(string themeName)
        {
            if (_themeBindings.TryGetValue(themeName, out object theme))
                if (typeof(ITranslator).IsAssignableFrom(theme.GetType()))
                    OnThemeUpdate?.Invoke(themeName, ((ITranslator)theme).Translate());
                else
                    throw new Exception($"Theme does not {nameof(ITranslator)} for Themebinding!");
        }
    }

    public interface ITranslator
    {
        TUITheme Translate();
    }
}
