# toast_ui.blazor_calendar
A Toast UI Calendar Wrapper For Blazor

See Toast UI Calendar for details:
https://github.com/nhn/tui.calendar

## This is a work in progress!

### How to start:
### in `startup.cs`
add:  
```c#
using toast_ui.blazor_calendar.Services;
```

and somewhere inside the Configuration method add:  
```c#
services.AddTUIBlazorCalendar();
```

### In `_Imports.razor` 
add: 
```c#
@using toast_ui.blazor_calendar
```

### In `_Host.cshtml` 
add this inside the body near the bottom `<body>`
```html
<script src="https://uicdn.toast.com/tui.code-snippet/v1.5.2/tui-code-snippet.min.js"></script>
<script src="https://uicdn.toast.com/tui.time-picker/latest/tui-time-picker.min.js"></script>
<script src="https://uicdn.toast.com/tui.date-picker/latest/tui-date-picker.min.js"></script>
<script src="https://uicdn.toast.com/tui-calendar/latest/tui-calendar.js"></script>

<script src= "_content/toast_ui_blazor_calendar/BlazorTuiCalendarInterop.js"></script>
```

## Place the Component in a razor file
```html
<TUICalendar/>
```

### Way More To Come... Help Welcomed
