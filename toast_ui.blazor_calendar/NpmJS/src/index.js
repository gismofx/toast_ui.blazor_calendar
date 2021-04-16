import Calendar from 'tui-calendar'; /* ES6 */
/*
import "tui-calendar/dist/tui-calendar.css";

// If you use the default popups, use this.
import 'tui-date-picker/dist/tui-date-picker.css';
import 'tui-time-picker/dist/tui-time-picker.css';
*/
import { v4 as uuidv4 } from 'uuid';



//JS Interop For Tui Calendar and Blazor
//https://nhn.github.io/tui.calendar/latest/
//

window.TUICalendar = {
    calendarRef: null,
    dotNetRef: null,
    initializeCalendar: function (dotNetObjectReference) {
        TUICalendar.calendarRef = new Calendar('#calendar', {
            defaultView: 'month', // monthly view option
            useCreationPopup: true,
            useDetailPopup: true
        });
        TUICalendar.dotNetRef = dotNetObjectReference;
        //this.calendarRef.on('beforeUpdateSchedule', function (event) { beforeUpdateScheduleB(event); });
        //this.calendarRef.on('beforeUpdateSchedule',beforeUpdateSchedule);
        //this.calendarRef.beforeUpdateSchedule = beforeUpdateSchedule2;

        TUICalendar.calendarRef.on('beforeUpdateSchedule', function (event) {
            var schedule = event.schedule;
            var changes = event.changes;
            TUICalendar.dotNetRef.invokeMethodAsync('UpdateSchedule', schedule.id, changes);
            TUICalendar.calendarRef.updateSchedule(schedule.id, schedule.calendarId, changes);
        });

        TUICalendar.calendarRef.on("beforeCreateSchedule", function (event) {
            var id = uuidv4();
            var schedule =
            {
                id: uuidv4(),
                calendarId: event.calendarId,
                title: event.title,
                location: event.location,
                start: event.start,
                end: event.end,
                state: event.state,
                isAllDay: event.isAllDay,
                isVisible: true,
                category: 'time',                
            };
            TUICalendar.dotNetRef.invokeMethodAsync('CreateSchedule', schedule);
            TUICalendar.calendarRef.createSchedules([schedule]);
            TUICalendar.calendarRef.render(true);
        });

    },

    beforeUpdateSchedule2: function (event) {
        var schedule = event.schedule;
        var changes = event.changes;
        TUICalendar.dotNetRef.invokeMethodAsync('UpdateSchedule', schedule.id, changes);
        TUICalendar.calendarRef.updateSchedule(schedule.id, schedule.calendarId, changes);
    },

    createSchedules: function (schedules) {
        TUICalendar.calendarRef.createSchedules(schedules);
    },

    setCalendars: function (calendars) {
        TUICalendar.calendarRef.setCalendars(calendars, true);
    },

    changeView: function (viewName) {
        TUICalendar.calendarRef.changeView(viewName);
    },

}