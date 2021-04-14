//JS Interop For Tui Calendar and Blazor
//https://nhn.github.io/tui.calendar/latest/
//
function initToastCal() {
    var cal = new tui.Calendar('#calendar', {
        defaultView: 'month' // monthly view option
    });
}

window.TUICalendar = {
    calendarRef: null,
    dotNetRef: null,
    initializeCalendar: function(dotNetObjectReference) {
        this.calendarRef = new tui.Calendar('#calendar', {
            defaultView: 'month', // monthly view option
            useCreationPopup: true,
            useDetailPopup: true
        });
        this.dotNetRef = dotNetObjectReference;
        this.calendarRef.on('beforeUpdateSchedule', function (event) {
            var schedule = event.schedule;
            var changes = event.changes;
            this.TUICalendar.dotNetRef.invokeMethodAsync('UpdateSchedule',schedule.id, changes);
            this.TUICalendar.calendarRef.updateSchedule(schedule.id, schedule.calendarId, changes);
        });
    },

    createSchedules: function (schedules) {
        this.calendarRef.createSchedules(schedules);
    },

    setCalendars: function (calendars) {
        this.calendarRef.setCalendars(calendars, true);
    },

    changeView: function (viewName) {
        this.calendarRef.changeView(viewName);
    },

}