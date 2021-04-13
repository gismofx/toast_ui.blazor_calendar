# toast_ui.blazor_calendar
A Toast UI Calendar Wrapper For Blazor  
For Events, Task, and Scheduling
![Sample](https://github.com/gismofx/toast_ui.blazor_calendar/blob/main/Sample.JPG?raw=true)

See Toast UI Calendar for details:
https://github.com/nhn/tui.calendar  
Toast UI Calendar Site:
https://ui.toast.com/tui-calendar


## This is a work in progress!

### How to start:

#### In `_Imports.razor` 
add: 
```c#
@using toast_ui.blazor_calendar
```

#### In `_Host.cshtml` 
add this inside the `<body>` near the bottom 
```html
<script src="https://uicdn.toast.com/tui.code-snippet/v1.5.2/tui-code-snippet.min.js"></script>
<script src="https://uicdn.toast.com/tui.time-picker/latest/tui-time-picker.min.js"></script>
<script src="https://uicdn.toast.com/tui.date-picker/latest/tui-date-picker.min.js"></script>
<script src="https://uicdn.toast.com/tui-calendar/latest/tui-calendar.js"></script>

<script src= "_content/toast_ui_blazor_calendar/BlazorTuiCalendarInterop.js"></script>
```

#### Place the Component in a razor file (See Test Project)
```razor
<TUICalendar Schedules ="ViewModel.Schedules" 
             CalendarOptions ="ViewModel.CalendarOptions" 
             CalendarProperties ="ViewModel.CalendarProps"></TUICalendar>
```


### WAAAYYY More To Come... Help Welcomed
