using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.WPF.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event EventHandler? RequestOpen;
        protected virtual void OnRequestOpen()
        {
            RequestOpen?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
