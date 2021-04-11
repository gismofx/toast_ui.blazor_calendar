//JS Interop For Tui Calendar and Blazor
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
    },

    createSchedules: function (schedules) {
        this.calendarRef.createSchedules(schedules);
    }
}