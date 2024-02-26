using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using toast_ui.blazor_calendar.Services;
using toast_ui.blazor_calendar.TestProjectWithMudBlazor;
using toast_ui.blazor_calendar.TestProjectWithMudBlazor.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddTransient<CalendarViewModel>();

//If you want to interact with the calendar from code
builder.Services.AddTransient<ITUICalendarInteropService, TUICalendarInteropService>();
await builder.Build().RunAsync();
