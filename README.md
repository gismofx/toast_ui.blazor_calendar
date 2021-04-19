# toast_ui.blazor_calendar
A Toast UI Calendar Wrapper For Blazor  
For Events, Task, and Scheduling
![Sample](Sample.JPG)

See Toast UI Calendar for details:
https://github.com/nhn/tui.calendar  
Toast UI Calendar Site:
https://ui.toast.com/tui-calendar


## This is a work in progress!

### How to start:

####
Nuget Pre-release
`Install-Package toast_ui.blazor_calendar -Version 1.0.0-alpha3`

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
<TUICalendar Schedules ="ViewModel.Schedules" 
             CalendarOptions ="ViewModel.CalendarOptions" 
             CalendarProperties ="ViewModel.CalendarProps"></TUICalendar>
```


### WAAAYYY More To Come... Help Welcomed
