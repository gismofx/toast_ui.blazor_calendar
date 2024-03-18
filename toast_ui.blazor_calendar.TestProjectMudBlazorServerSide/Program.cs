using toast_ui.blazor_calendar.TestProjectMudBlazorServerSide.Components;
using MudBlazor.Services;
using toast_ui.blazor_calendar.Services;
using toast_ui.blazor_calendar.TestProjectMudBlazorServerSide.ViewModels;
using MudBlazor;
using toast_ui.blazor_calendar.ThemeTranslator;
using toast_ui.blazor_calendar.Models;
using toast_ui.blazor_calendar.Models.Extensions;
using toast_ui.blazor_calendar.TestProjectMudBlazorServerSide.Themes;
using MudBlazor.Utilities;

namespace toast_ui.blazor_calendar.TestProjectMudBlazorServerSide
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddMudServices(new MudServicesConfiguration());

            //Add ViewModel
            builder.Services.AddTransient<CalendarViewModel>();

            //If you want to interact with the calendar from code
            builder.Services.AddTUIBlazorCalendar();
            builder.Services.AddThemeBinder(th =>
            {
                th.AddThemeBinding("Light", new Light());
                th.AddThemeBinding("Dark", new Dark());
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();



            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();




        }
    }
}
