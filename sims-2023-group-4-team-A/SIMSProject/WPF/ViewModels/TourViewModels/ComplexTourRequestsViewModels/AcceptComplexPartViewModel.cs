using SIMSProject.WPF.Messenger.Messages;
using SIMSProject.WPF.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Domain.Models.TourModels;
using System.Collections.ObjectModel;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ComplexTourRequestsViewModels
{
    public class AcceptComplexPartViewModel: ViewModelBase
    {
        private readonly ComplexTourRequestService _service;
        private ObservableCollection<DateTime> _freeDates = new();
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
        public ObservableCollection<DateTime> FreeDates
        {
            get => _freeDates;
            set
            {
                if (_freeDates == value) return;
                _freeDates = value;
                OnPropertyChanged(nameof(FreeDates));
            }
        }
        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (_duration == value) return;
                _duration = value; 
                OnPropertyChanged(nameof(Duration));
                FreeDates = new(_service.GetFreeTimes(GuideHomeViewModel.Guide, StartDate, EndDate, Duration));
            }
        }
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

        private CustomTourRequest _customTourRequest = new();
        public CustomTourRequest TourRequest
        {
            get => _customTourRequest;
            set
            {
                if(value == _customTourRequest) return;
                _customTourRequest = value;
                OnPropertyChanged(nameof(TourRequest));
            }
        }
        public AcceptComplexPartViewModel()
        {
            _service = Injector.GetService<ComplexTourRequestService>();
            MessageBus.Subscribe<ScheduleRequestMessage>(this, OpenMessage);
        }

        private void OpenMessage(ScheduleRequestMessage message)
        {
            TourRequest = message.Request;
            StartDate = TourRequest.StartDate;
            EndDate = TourRequest.EndDate;
        }
    }
}
