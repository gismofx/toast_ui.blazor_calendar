## toast_ui.blazor_calendar
A Toast UI Calendar Wrapper For Blazor  
For Events, Task, and Scheduling  
![NuGet](https://img.shields.io/nuget/vpre/toast_ui.blazor_calendar?color=blue)
![NuGetPre](https://img.shields.io/nuget/v/toast_ui.blazor_calendar?label=Pre-Release&color=yellow)
![DLs](https://img.shields.io/nuget/dt/toast_ui.blazor_calendar?color=brightgreen&label=NuGet%20downloads)
![Issues](https://img.shields.io/github/issues/gismofx/toast_ui.blazor_calendar?color=red)
![license](https://img.shields.io/github/license/gismofx/toast_ui.blazor_calendar?color=brightgreen)
![stars](https://img.shields.io/github/stars/gismofx/toast_ui.blazor_calendar?style=social)

---
![Sample](Sample.JPG)

See Toast UI Calendar for details:
https://github.com/nhn/tui.calendar  
Toast UI Calendar Site:
https://ui.toast.com/tui-calendar


### Pre-Release BETA now available on Nuget.

## How to start:

####
Nuget Pre-release:  
`Install-Package toast_ui.blazor_calendar -Version 1.0.0-beta1.1`

### Edit your project files:
#### In `_Imports.razor` 
add: 
```c#
@using toast_ui.blazor_calendar
```

#### In `_Host.cshtml` 
add this inside the `<head>` 
```html
<link href="_content/toast_ui.blazorCalendar/TuiCalendar.css" rel="stylesheet">
```

add this inside the `<body>` near the bottom 
```html
<script src="_content/toast_ui.blazor_calendar/TUI.blazor_calendar.min.js"></script> 
```

#### Place the Component in a razor file (See Test Project)
```razor
<TUICalendar Schedules="ViewModel.Schedules" 
             CalendarOptions="ViewModel.CalendarOptions" 
             CalendarProperties="ViewModel.CalendarProps"
             CalendarViewName="ViewModel.CalendarViewName"
             @bind-VisibleStartDateRange="ViewModel.StartDate"
             @bind-VisibleEndDateRange="ViewModel.EndDate"
             @ref=_calendarRef></TUICalendar>
```


### WAAAYYY More To Come... Help Welcomed

I am attempting to implement a simple CalDav Server and Client to make a better example of the calendar component.
[CalDavSharp](https://github.com/gismofx/CalDavSharp)
