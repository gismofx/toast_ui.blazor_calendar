using toast_ui.blazor_calendar.TestProjectMudBlazorServerSide.Components;
using MudBlazor.Services;
using toast_ui.blazor_calendar.Services;
using toast_ui.blazor_calendar.TestProjectMudBlazorServerSide.ViewModels;
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
            builder.Services.AddMudServices();

            //Add ViewModel
            builder.Services.AddTransient<CalendarViewModel>();

            //If you want to interact with the calendar from code
            builder.Services.AddTUIBlazorCalendar();
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
