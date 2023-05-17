using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.Messenger;
using SIMSProject.WPF.Messenger.Messages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels
{
    public class ToursManagerViewModel: ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;

        private ObservableCollection<Tour> _tours = new();
        public ObservableCollection<Tour> Tours
        {
            get => _tours;
            set
            {
                if(_tours != value)
                {
                    _tours = value;
                    OnPropertyChanged(nameof(Tours));
                }
            }
        }
        private Tour _tour = new();
        public Tour SelectedTour
        {
            get => _tour;
            set
            {
                if (value != _tour)
                {
                    _tour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }
        public ToursManagerViewModel(string callerId)
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourService = Injector.GetService<TourService>();
            switch(callerId)
            {
                case "TodaysTours": Tours = new(_tourAppointmentService.GetTodaysTours());break;
                case "AllTours": Tours = new(_tourService.GetTours()); break;
            }
            TourInfoCommand = new RelayCommand(TourInfoExecute, TourInfoCanExecute);
        }

        #region TourInfoCommand
        public ICommand TourInfoCommand { get; private set; }
        public bool TourInfoCanExecute()
        {
            return SelectedTour.Id > 0;
        }
        public void TourInfoExecute()
        {
            SendMessage();
        }
        #endregion
        public void SendMessage()
        {
            var message = new TourInfoMessage(this, SelectedTour);
            MessageBus.Publish(message);
        }
    }
}
