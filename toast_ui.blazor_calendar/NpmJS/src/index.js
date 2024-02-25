import Calendar from '@toast-ui/calendar';
import '@toast-ui/calendar/dist/toastui-calendar.min.css';

//import Calendar from 'tui-calendar'; /* ES6 */
//import "tui-calendar/dist/tui-calendar.css";

// If you use the default popups, use this.
import 'tui-date-picker/dist/tui-date-picker.css';
import 'tui-time-picker/dist/tui-time-picker.css';

//UUID
import { v4 as uuidv4 } from 'uuid';



//JS Interop For Tui Calendar and Blazor
//https://nhn.github.io/tui.calendar/latest/
//

window.TUICalendar = {
    calendarRef: null,
    dotNetRef: null,
    initializeCalendar: function (dotNetObjectReference, options) {
        var tTemplate;

        var templates = {
            milestone: function (schedule) {
                return '<span style="color:red;"><i class="fa fa-flag"></i> ' + schedule.title + '</span>';
            }
        };

        if (options.template !== undefined) {
            var templateFunctions = new Object();
            options.template.forEach(templateFunction => {

                var tfunc = new Function(templateFunction.args, templateFunction.functionBody);
                templateFunctions[templateFunction.functionName] = tfunc;
            });
        }

        options.template = templateFunctions;
        TUICalendar.calendarRef = new Calendar('#calendar', options
            /*{
            defaultView: 'month', // monthly view option
            useCreationPopup: true,
            useDetailPopup: true
        }*/);
        TUICalendar.dotNetRef = dotNetObjectReference;
        //how do I separate
        //this.calendarRef.on('beforeUpdateSchedule', function (event) { beforeUpdateScheduleB(event); });
        //this.calendarRef.on('beforeUpdateSchedule',beforeUpdateSchedule);
        //this.calendarRef.beforeUpdateSchedule = beforeUpdateSchedule2;


        //Events
        https://nhn.github.io/tui.calendar/latest/Calendar#event-beforeUpdateSchedule
        TUICalendar.calendarRef.on('beforeUpdateSchedule', function (event) {
            var schedule = event.schedule;
            var changes = event.changes;
            TUICalendar.dotNetRef.invokeMethodAsync('UpdateSchedule', schedule, changes);
            TUICalendar.calendarRef.updateSchedule(schedule.id, schedule.calendarId, changes);
        });

        //beforeCreateSchedule
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
                category: event.isAllDay ? 'allday' : 'time',                
            };
            TUICalendar.dotNetRef.invokeMethodAsync('CreateSchedule', schedule);
            TUICalendar.calendarRef.createSchedules([schedule]);
            TUICalendar.calendarRef.render(true);
        });

        //beforeDeleteSchedule
        TUICalendar.calendarRef.on('beforeDeleteSchedule', function (event) {
            TUICalendar.dotNetRef.invokeMethodAsync('DeleteSchedule', event.schedule.id);
            TUICalendar.calendarRef.deleteSchedule(event.schedule.id, event.schedule.calendarId);
        });

        //clickSchedule
        TUICalendar.calendarRef.on('clickSchedule', function (event) {
            TUICalendar.dotNetRef.invokeMethodAsync('OnClickSchedule', event.schedule.id);
            /*
            var triggerEventName = event.triggerEventName;
            if (triggerEventName === 'click') {
                TUICalendar.dotNetRef.invokeMethodAsync('OnClickSchedule', event.schedule.id);
            } else if (triggerEventName === 'dblclick') {
                TUICalendar.dotNetRef.invokeMethodAsync('OnDoubleClickSchedule', event.schedule.id);
            }
            */
        });

    },

    clear: function () {
        TUICalendar.calendarRef.clear();
    },

    createSchedules: function (schedules) {
        TUICalendar.calendarRef.createEvents(schedules);
    },

    setCalendars: function (calendars) {
        TUICalendar.calendarRef.setCalendars(calendars, true);
    },

    changeView: function (viewName) {
        TUICalendar.calendarRef.changeView(viewName);
    },

    moveToNextOrPreviousOrToday: function (val) {
        if (val === -1) {
            TUICalendar.calendarRef.prev();
        }
        else if (val === 1) {
            TUICalendar.calendarRef.next();
        }
        else if (val === 0) {
            TUICalendar.calendarRef.today();
        }
    },

    deleteSchedule: function (calendarId, scheduleId) {
        TUICalendar.calendarRef.deleteEvent(scheduleId, calendarId);
    },

    setCalendarOptions: function (options) {
        TUICalendar.calendarRef.setOptions(options);
    },

    hideShowCalendar: function (calendarId, hide) {
        TUICalendar.calendarRef.toggleSchedules(calendarId, hide);
    },

    setDate: function (date) {
        TUICalendar.calendarRef.setDate(date);
    },

    getDateRangeStart: function () {
        return TUICalendar.calendarRef.getDateRangeStart();
    },

    getDateRangeEnd: function () {
        return TUICalendar.calendarRef.getDateRangeEnd();
    },

    scrollToNowInView: function () {
        if (TUICalendar.calendarRef.getViewName() !== 'month') {
            TUICalendar.calendarRef.scrollToNow();
            TUICalendar.calendarRef.render();
        }
    },

}