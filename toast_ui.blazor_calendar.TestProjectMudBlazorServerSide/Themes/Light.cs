using MudBlazor;
using toast_ui.blazor_calendar.Models;
using toast_ui.blazor_calendar.ThemeTranslator;

namespace toast_ui.blazor_calendar.TestProjectMudBlazorServerSide.Themes
{
    public class Light : MudTheme, ITranslator
    {
        public TUITheme Translate()
        {
            return new TUITheme()
            {

            };
        }
    }
}
