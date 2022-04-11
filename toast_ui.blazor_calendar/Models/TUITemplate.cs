using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Services.JsonConverters;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/Template
    /// Template functions to support customer renderer
    /// </summary>
    //[JsonConverter(typeof(TUITemplateConverter))]
    public class TUITemplate : IEnumerable<TuiTemplateFunction>
    {
        private List<TuiTemplateFunction> TemplateFunctions { get; set; } = new List<TuiTemplateFunction>();
        //@Todo: Add More Add* Methods https://nhn.github.io/tui.calendar/latest/Template



        /// <summary>
        /// In Month View, when there are too many schedules to display for a given day, a button is shown e.g. "5 more"
        /// You can change this text 
        /// </summary>
        /// <param name="jsFunctionBody">e.g. @"return '<span class=""weekday-grid-more-schedules"">+' + hiddenSchedules + '</span>';"</param>
        /// <returns></returns>
        public TUITemplate AddMonthGridHeaderExceed(string jsFunctionBody)
        {
            return AddTemplate("monthGridHeaderExceed", "hiddenSchedules", 
                jsFunctionBody);
        }

        private TUITemplate AddTemplate(string functionName, string argsAsCsv, string functionBody)
        {
            var templateFunction = new TuiTemplateFunction()
            {
                FunctionName = functionName,
                Args = argsAsCsv,
                FunctionBody = functionBody
            };
            TemplateFunctions.Add(templateFunction);
            return this;
        }

        public IEnumerator<TuiTemplateFunction> GetEnumerator()
        {
            return TemplateFunctions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class TuiTemplateFunction
    {
        public string FunctionName { get; set; }
        public string Args { get; set; }
        public string FunctionBody { get; set; }
        public TuiTemplateFunction()
        {

        }
    }
}
      /*      monthGridHeaderExceed: function(hiddenSchedules)
        {
            //return '<span class="weekday-grid-more-schedules">+' + hiddenSchedules + '</span>';


        }
      */
    /*
     var calendar = new tui.Calendar(document.getElementById('calendar'), {
    ...
    template: {
        milestone: function(schedule) {
            return '<span class="calendar-font-icon ic-milestone-b"></span> <span style="background-color: ' + schedule.bgColor + '">' + schedule.title + '</span>';
        },
        milestoneTitle: function() {
            return '<span class="tui-full-calendar-left-content">MILESTONE</span>';
        },
        task: function(schedule) {
            return '#' + schedule.title;
        },
        taskTitle: function() {
            return '<span class="tui-full-calendar-left-content">TASK</span>';
        },
        allday: function(schedule) {
            return getTimeTemplate(schedule, true);
        },
        alldayTitle: function() {
            return '<span class="tui-full-calendar-left-content">ALL DAY</span>';
        },
        time: function(schedule) {
            return '<strong>' + moment(schedule.start.getTime()).format('HH:mm') + '</strong> ' + schedule.title;
        },
        goingDuration: function(schedule) {
            return '<span class="calendar-icon ic-travel-time"></span>' + schedule.goingDuration + 'min.';
        },
        comingDuration: function(schedule) {
            return '<span class="calendar-icon ic-travel-time"></span>' + schedule.comingDuration + 'min.';
        },
        monthMoreTitleDate: function(date, dayname) {
            var day = date.split('.')[2];

            return '<span class="tui-full-calendar-month-more-title-day">' + day + '</span> <span class="tui-full-calendar-month-more-title-day-label">' + dayname + '</span>';
        },
        monthMoreClose: function() {
            return '<span class="tui-full-calendar-icon tui-full-calendar-ic-close"></span>';
        },
        monthGridHeader: function(dayModel) {
            var date = parseInt(dayModel.date.split('-')[2], 10);
            var classNames = ['tui-full-calendar-weekday-grid-date '];

            if (dayModel.isToday) {
                classNames.push('tui-full-calendar-weekday-grid-date-decorator');
            }

            return '<span class="' + classNames.join(' ') + '">' + date + '</span>';
        },
        monthGridHeaderExceed: function(hiddenSchedules) {
            return '<span class="weekday-grid-more-schedules">+' + hiddenSchedules + '</span>';
        },
        monthGridFooter: function() {
            return '';
        },
        monthGridFooterExceed: function(hiddenSchedules) {
            return '';
        },
        monthDayname: function(model) {
            return (model.label).toString().toLocaleUpperCase();
        },
        weekDayname: function(model) {
            return '<span class="tui-full-calendar-dayname-date">' + model.date + '</span>&nbsp;&nbsp;<span class="tui-full-calendar-dayname-name">' + model.dayName + '</span>';
        },
        weekGridFooterExceed: function(hiddenSchedules) {
            return '+' + hiddenSchedules;
        },
        dayGridTitle: function(viewName) {

            // use another functions instead of 'dayGridTitle'
            // milestoneTitle: function() {...}
            // taskTitle: function() {...}
            // alldayTitle: function() {...}

            var title = '';
            switch(viewName) {
                case 'milestone':
                    title = '<span class="tui-full-calendar-left-content">MILESTONE</span>';
                    break;
                case 'task':
                    title = '<span class="tui-full-calendar-left-content">TASK</span>';
                    break;
                case 'allday':
                    title = '<span class="tui-full-calendar-left-content">ALL DAY</span>';
                    break;
            }

            return title;
        },
        schedule: function(schedule) {

            // use another functions instead of 'schedule'
            // milestone: function() {...}
            // task: function() {...}
            // allday: function() {...}

            var tpl;

            switch(category) {
                case 'milestone':
                    tpl = '<span class="calendar-font-icon ic-milestone-b"></span> <span style="background-color: ' + schedule.bgColor + '">' + schedule.title + '</span>';
                    break;
                case 'task':
                    tpl = '#' + schedule.title;
                    break;
                case 'allday':
                    tpl = getTimeTemplate(schedule, true);
                    break;
            }

            return tpl;
        },
        collapseBtnTitle: function() {
            return '<span class="tui-full-calendar-icon tui-full-calendar-ic-arrow-solid-top"></span>';
        },
        timezoneDisplayLabel: function(timezoneOffset, displayLabel) {
            var gmt, hour, minutes;

            if (!displayLabel) {
                gmt = timezoneOffset < 0 ? '-' : '+';
                hour = Math.abs(parseInt(timezoneOffset / 60, 10));
                minutes = Math.abs(timezoneOffset % 60);
                displayLabel = gmt + getPadStart(hour) + ':' + getPadStart(minutes);
            }

            return displayLabel;
        },
        timegridDisplayPrimayTime: function(time) {
            // will be deprecated. use 'timegridDisplayPrimaryTime'
            var meridiem = 'am';
            var hour = time.hour;

            if (time.hour > 12) {
                meridiem = 'pm';
                hour = time.hour - 12;
            }

            return hour + ' ' + meridiem;
        },
        timegridDisplayPrimaryTime: function(time) {
            var meridiem = 'am';
            var hour = time.hour;

            if (time.hour > 12) {
                meridiem = 'pm';
                hour = time.hour - 12;
            }

            return hour + ' ' + meridiem;
        },
        timegridDisplayTime: function(time) {
            return getPadStart(time.hour) + ':' + getPadStart(time.hour);
        },
        timegridCurrentTime: function(timezone) {
            var templates = [];

            if (timezone.dateDifference) {
                templates.push('[' + timezone.dateDifferenceSign + timezone.dateDifference + ']<br>');
            }

            templates.push(moment(timezone.hourmarker).format('HH:mm a'));

            return templates.join('');
        },
        popupIsAllDay: function() {
            return 'All Day';
        },
        popupStateFree: function() {
            return 'Free';
        },
        popupStateBusy: function() {
            return 'Busy';
        },
        titlePlaceholder: function() {
            return 'Subject';
        },
        locationPlaceholder: function() {
            return 'Location';
        },
        startDatePlaceholder: function() {
            return 'Start date';
        },
        endDatePlaceholder: function() {
            return 'End date';
        },
        popupSave: function() {
            return 'Save';
        },
        popupUpdate: function() {
            return 'Update';
        },
        popupDetailDate: function(isAllDay, start, end) {
            var isSameDate = moment(start).isSame(end);
            var endFormat = (isSameDate ? '' : 'YYYY.MM.DD ') + 'hh:mm a';

            if (isAllDay) {
                return moment(start).format('YYYY.MM.DD') + (isSameDate ? '' : ' - ' + moment(end).format('YYYY.MM.DD'));
            }

            return (moment(start).format('YYYY.MM.DD hh:mm a') + ' - ' + moment(end).format(endFormat));
        },
        popupDetailLocation: function(schedule) {
            return 'Location : ' + schedule.location;
        },
        popupDetailUser: function(schedule) {
            return 'User : ' + (schedule.attendees || []).join(', ');
        },
        popupDetailState: function(schedule) {
            return 'State : ' + schedule.state || 'Busy';
        },
        popupDetailRepeat: function(schedule) {
            return 'Repeat : ' + schedule.recurrenceRule;
        },
        popupDetailBody: function(schedule) {
            return 'Body : ' + schedule.body;
        },
        popupEdit: function() {
            return 'Edit';
        },
        popupDelete: function() {
            return 'Delete';
        }
    }
}
     * */