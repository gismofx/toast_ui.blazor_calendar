using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace toast_ui.blazor_calendar.Services
{
    class TUICalendarInteropService : IAsyncDisposable
    {

        private readonly IJSRuntime _JSRuntime;

        //private DotNetObjectReference<TUICalendar> ObjectReference;

        public TUICalendarInteropService(IJSRuntime jsRuntime)
        {
            _JSRuntime = jsRuntime;
        }

        public async Task InitCalendarAsync(DotNetObjectReference<TUICalendar> objectReference)
        {
            await _JSRuntime.InvokeVoidAsync("TUICalendar.initializeCalendar", objectReference);
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask();
            //throw new NotImplementedException();
        }
    }
}
