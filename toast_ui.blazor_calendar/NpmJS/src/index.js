﻿import Calendar from '@toast-ui/calendar';
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
            milestone: function (event) {
                return '<span style="color:red;"><i class="fa fa-flag"></i> ' + event.title + '</span>';
            }
        };

        if (options.template !== undefined) {
            var templateFunctions = new Object();
            options.template.forEach(templateFunction => {

                var tfunc = new Function(templateFunction.args, templateFunction.functionBody);
                templateFunctions[templateFunction.functionName] = tfunc;
            });
        }

        //options.template = templateFunctions;
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
        TUICalendar.calendarRef.on('beforeUpdateEvent', function (eventToChange) {
            var myEvent = eventToChange.event;
            var changes = eventToChange.changes;
            TUICalendar.dotNetRef.invokeMethodAsync('UpdateEvent', myEvent, changes);
            TUICalendar.calendarRef.updateEvent(myEvent.id, myEvent.calendarId, changes);
        });

        //beforeCreateSchedule
        TUICalendar.calendarRef.on("beforeCreateEvent", function (event) {
            //var event =
            //{
            //    id: uuidv4(),
            //    calendarId: event.calendarId,
            //    title: event.title,
            //    location: event.location,
            //    start: event.start,
            //    end: event.end,
            //    state: event.state,
            //    isAllDay: event.isAllDay,
            //    isVisible: true,
            //    category: event.isAllDay ? 'allday' : 'time',
            //};
            event.id = uuidv4();
            TUICalendar.dotNetRef.invokeMethodAsync('CreateEvent', event);
            TUICalendar.calendarRef.createEvents([event]);
            TUICalendar.calendarRef.render(true);
        });

        //beforeDeleteEvent
        TUICalendar.calendarRef.on('beforeDeleteEvent', function (event) {
            TUICalendar.dotNetRef.invokeMethodAsync('DeleteEvent', event.id);
            TUICalendar.calendarRef.deleteEvent(event.id, event.calendarId);
        });

        //clickEvent
        TUICalendar.calendarRef.on('clickEvent', function (event) {
            TUICalendar.dotNetRef.invokeMethodAsync('OnClickEvent', event.event);
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

    //Clear Whole Calendar
    clear: function () {
        TUICalendar.calendarRef.clear();
    },

    //Create/Add Events from list of events
    createEvents: function (events) {
        TUICalendar.calendarRef.createEvents(events);
    },

    //Update an event on the calendar
    updateSchedule: function (event) {
        TUICalendar.calendarRef.updateEvent(event.id, event.calendarId, event);
        TUICalendar.calendarRef.render(true);
    },

    setCalendars: function (calendars) {
        TUICalendar.calendarRef.setCalendars(calendars, true);
    },

    setTheme: function (userTheme) {
        TUICalendar.calendarRef.setTheme(userTheme);
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

    deleteEvent: function (calendarId, scheduleId) {
        TUICalendar.calendarRef.deleteEvent(scheduleId, calendarId);
    },

    setCalendarOptions: function (options) {
        TUICalendar.calendarRef.setOptions(options);
    },

    /*Make calendars' event visible or hidden*/
    setCalendarVisibility: function (calendarIds, isVisible) {
        TUICalendar.calendarRef.setCalendarVisibility(calendarIds, isVisible);
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