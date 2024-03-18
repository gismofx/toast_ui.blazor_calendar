using MudBlazor;
using toast_ui.blazor_calendar.Models;
using toast_ui.blazor_calendar.ThemeTranslator;

namespace toast_ui.blazor_calendar.TestProjectMudBlazorServerSide.Themes
{
    public class Dark : MudTheme, ITranslator
    {
        public TUITheme Translate()
        {
            return new TUITheme()
            {
                CommonTheme = new CommonTheme()
                {
                    BackgroundColor = System.Drawing.Color.FromArgb(37, 37, 38),
                    EventTitleColor = System.Drawing.Color.White,
                    Border = $"1px solid {System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(62, 62, 66))}",
                    DayName = new() { Color = System.Drawing.Color.White },
                    GridSelection = new GridSelectionTheme()
                    {
                        BackgroundColor = System.Drawing.Color.WhiteSmoke,
                        Border = "1px dotted #515ce6"
                    },
                    Today = new() { Color = System.Drawing.Color.White },
                    Saturday = new() { Color = System.Drawing.Color.Pink },
                    Holiday = new() { Color = System.Drawing.Color.Red }
                },
                MonthTheme = new MonthTheme()
                {
                    DayName = new()
                    {
                        BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30),
                        BorderLeft = null,

                    },
                    Weekend = new()
                    {
                        BackgroundColor = System.Drawing.Color.FromArgb(45, 45, 48)
                    },
                    DayExceptThisMonth = new()
                    {
                        Color = System.Drawing.Color.FromArgb(179, 179, 179)
                    }
                },
            };
        }
    }
}
