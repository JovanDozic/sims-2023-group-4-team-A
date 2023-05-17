using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.Messenger;
using SIMSProject.WPF.Messenger.Messages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels
{
    public class AppointmentPickerViewModel : ViewModelBase
    {
        private readonly TourAppointmentService _tourAppointmentService;
        private Tour _tour { get; set; } = new();
        private TourAppointment _active { get => _tourAppointmentService.GetActive(); }
        
        private ObservableCollection<TourAppointment> _appointments = new();
        public ObservableCollection<TourAppointment> Appointments
        {
            get => _appointments;
            set
            {
                if (_appointments == value) return;
                _appointments = value;
                OnPropertyChanged();
            }
        }

        private TourAppointment _selectedAppointment = new();
        public TourAppointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (value != _selectedAppointment)
                {
                    _selectedAppointment = value;
                    OnPropertyChanged(nameof(SelectedAppointment));
                }
            }
        }

        public AppointmentPickerViewModel()
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            MessageBus.Subscribe<TourInfoMessage>(this, OpenMessage);

            LiveTrackCommand = new RelayCommand(LiveTrackExecute, LiveTrackCanExecute);
        }

        private void OpenMessage(TourInfoMessage message)
        {
            _tour = message.Tour;
            Appointments = new(_tourAppointmentService.GetTodays(_tour));
        }

        #region LiveTrackCommand
        public ICommand LiveTrackCommand {get; private set;}

        public bool LiveTrackCanExecute()
        {
            return SelectedAppointment != null 
                && ((SelectedAppointment == _active && _active != null) || (SelectedAppointment.TourStatus == Status.INACTIVE && _active == null));
        }
        public void LiveTrackExecute() 
        {
            if (_active == null)
            {
                SelectedAppointment = _tourAppointmentService.Activate(SelectedAppointment, _tour);
            }
            SendMessage(SelectedAppointment);   
        }

        public void SendMessage(TourAppointment selectedAppointment)
        {
            var message = new LiveTrackMessage(this, selectedAppointment);
            MessageBus.Publish(message);
        }
        #endregion
    }
}
