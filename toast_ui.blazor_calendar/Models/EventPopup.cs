using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models
{
    public class EventPopup
    {
        public string IsAlldayText { get; set; }
        public string TitlePlaceholderText { get; set; }
        public string LocationPlaceholderText { get; set; }
        public string StateBusyText { get; set; }
        public string StateFreeText { get; set; }
        public string StartDatePlaceholderText { get; set; }
        public string EndDatePlaceholderText { get; set; }
        public string SaveText { get; set; }
        public string UpdateText { get; set; }
        public string EditText { get; set; }
        public string DeleteText { get; set; }

        // TODO: Is it useful?
        public string DetailBodyText { get; set; }

    }

    //popupIsAllday()
    //{
    //return 'Ganztägig';
    //}

    //popupStateBusy()
    //{
    //    return 'Tätig';
    //},
    //     popupStateFree()
    //{
    //    return 'Frei';
    //},
    //     titlePlaceholder()
    //{
    //    return 'Titel';
    //},
    //     locationPlaceholder()
    //{
    //    return 'Ort';
    //},
    //     startDatePlaceholder()
    //{
    //    return 'Start-Datum';
    //},
    //     endDatePlaceholder()
    //{
    //    return 'End-Datum';
    //},
    //     popupSave()
    //{
    //    return 'Hinzufügen';
    //},
    //     popupUpdate()
    //{
    //    return 'Ändern';
    //},
    //     popupEdit()
    //{
    //    return 'Anpassen';
    //},
    //     popupDelete()
    //{
    //    return 'Löschen';
    //},
    //     popupDetailBody({ body }) {
    //         return "Test Description";
    //     }
}
