using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.Messenger;
using SIMSProject.WPF.Messenger.Messages;
using SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels;
using SIMSProject.WPF.Views.TourViews.GuideViews;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels
{
    public class ToursManagerViewModel : ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;
        public DetailedTourViewModel NextViewModel;
        public AppointmentPickerViewModel NextViewModel1;

        private ObservableCollection<Tour> _tours = new();
        public ObservableCollection<Tour> Tours
        {
            get => _tours;
            set
            {
                if (_tours != value)
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
            switch (callerId)
            {
                case "TodaysTours": Tours = new(_tourAppointmentService.GetTodaysTours()); break;
                case "AllTours": Tours = new(_tourService.GetTours()); break;
            }
            TourInfoCommand = new RelayCommand(TourInfoExecute, TourInfoCanExecute);
            TodaysAppointmentsCommand = new RelayCommand(TodaysAppointmentsExecute, TodaysAppointmentsCanExecute);
        }

        #region TourInfoCommand
        public ICommand TourInfoCommand { get; private set; }
        public bool TourInfoCanExecute()
        {
            return SelectedTour.Id > 0;
        }
        public void TourInfoExecute()
        {
            NextViewModel = new();
            SendMessage();
            OnRequestOpen();
        }
        #endregion
        #region TodaysAppointmentsCommand
        public ICommand TodaysAppointmentsCommand { get; private set; }
        public bool TodaysAppointmentsCanExecute()
        {
            return SelectedTour.Id > 0;
        }
        public void TodaysAppointmentsExecute()
        {
            NextViewModel1 = new();
            SendMessage();
            OnRequestOpen();
        }
        #endregion
        public void SendMessage()
        {
            var message = new TourInfoMessage(this, SelectedTour);
            MessageBus.Publish(message);
        }
    } 
}