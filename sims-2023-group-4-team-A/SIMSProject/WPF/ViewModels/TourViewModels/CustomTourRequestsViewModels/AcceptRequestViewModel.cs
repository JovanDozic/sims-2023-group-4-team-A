using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.View.GuideViews;
using SIMSProject.WPF.Messenger;
using SIMSProject.WPF.Messenger.Messages;
using SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels
{
    public class AcceptRequestViewModel : ViewModelBase
    {
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly CustomTourRequestService _requestService;
        public TourCreationViewModel NextViewModel;
        public List<DateTime> BusyDates { get; set; } = new();
        public CustomTourRequest TourRequest { get; set; } = new();

        private DateTime _date;
        public DateTime SelectedDate
        {
            get { return _date; }
            set
            {
                if (value == _date) return;
                _date = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value == _startDate) return;
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value == _endDate) return;
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public AcceptRequestViewModel()
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _requestService = Injector.GetService<CustomTourRequestService>();
            BusyDates = _tourAppointmentService.GetBusyDates();

            MessageBus.Subscribe<ScheduleRequestMessage>(this, OpenMessage);
            
            AcceptCommand = new RelayCommand(AcceptCommandExecute, AcceptCommandCanExecute);
        }
        private void OpenMessage(ScheduleRequestMessage message)
        {
            TourRequest = message.Request;
            SelectedDate = TourRequest.StartDate;
            StartDate = TourRequest.StartDate;
            EndDate = TourRequest.EndDate;
        }
        #region AcceptRequestCommand
        public ICommand AcceptCommand {  get; private set; }
        public bool AcceptCommandCanExecute()
        {
            return !BusyDates.Any(x => x.Date.Equals(SelectedDate.Date));
        }
        public void AcceptCommandExecute()
        {
            _requestService.ApproveRequest(TourRequest);
            NextViewModel = new();
            SendMessage();
            OnRequestOpen();
        }

        public void SendMessage()
        {
            var message = new CreateRequestedMessage(this, TourRequest, SelectedDate);
            MessageBus.Publish(message);
        }
        #endregion
    }
}
