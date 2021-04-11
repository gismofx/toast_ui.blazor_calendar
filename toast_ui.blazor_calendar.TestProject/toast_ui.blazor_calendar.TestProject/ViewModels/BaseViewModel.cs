using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.TestProject.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                SetValue(ref isBusy, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingFiled, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingFiled, value)) return;
            backingFiled = value;
            OnPropertyChanged(propertyName);
        }

        protected bool TrySetProperty<T>(Action<T> property, T newValue, T oldValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                return false;
            }

            property(newValue);
            OnPropertyChanged(propertyName);
            return true;
        }

        public void ShowUserMessage(MessageEventArgs args)
        {
            RaiseDialogMessage(args);
        }
        public void RaiseDialogMessage(MessageEventArgs args)
        {
            ShowMessage?.Invoke(this, args);
        }

        public event MessageEventHandler ShowMessage;

        public delegate void MessageEventHandler(object sender, MessageEventArgs e);

        public enum eMessageType
        {
            None = 0,
            Exclamation = 1,
            Warning = 2,
            Error = 3,
            Certificate = 11,
            Like = 21
        }
        public class MessageEventArgs : EventArgs
        {
            public string Text { get; set; } = null;
            public eMessageType Type { get; set; } = eMessageType.Like;
        }
    }


}
