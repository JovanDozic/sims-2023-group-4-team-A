using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.Messenger.Messages;
using SIMSProject.WPF.ViewModels.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels
{
    public class CustomTourRequestsViewModel: ViewModelBase
    {
        private readonly CustomTourRequestService _requestService;

        private ObservableCollection<CustomTourRequest> _customTourRequests = new();
        public ObservableCollection<CustomTourRequest> CustomTourRequests
        {
            get => _customTourRequests;
            set
            {
                if(value == _customTourRequests) return;
                _customTourRequests = value;
                OnPropertyChanged(nameof(CustomTourRequests));
            }
        }
        private CustomTourRequest _selectedRequest = new();
        public CustomTourRequest SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                if(value == _selectedRequest) return;
                _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }

        public ObservableCollection<Location> RequestsLocations { get; set; }
        public ObservableCollection<Language> RequestsLanguages { get; set; }

        private Location _location = new();
        public Location SelectedLocation
        {
            get => _location;
            set
            {
                if(value == _location) return;
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private Language _language;
        public Language SelectedLanguage
        {
            get => _language;
            set
            {
                if(value == _language) return;
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        private int _guests;
        public int NumberOfGuests
        {
            get => _guests;
            set
            {
                if(_guests == value) return;
                _guests = value;
                OnPropertyChanged(nameof(NumberOfGuests));
            }
        }

        private DateTime _start;
        public DateTime StartDate
        {
            get => _start;
            set
            {
                if(value == _start) return;
                _start = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _end;
        public DateTime EndDate
        {
            get => _end;
            set
            {
                if(value == _end) return;
                _end = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }


        public CustomTourRequestsViewModel()
        {
            _requestService = Injector.GetService<CustomTourRequestService>();
            CustomTourRequests = new(_requestService.GetAll());
            RequestsLocations = new(_requestService.GetRequestsLocations());
            RequestsLanguages = new(_requestService.GetRequestsLanguages());

            FilterCommand = new RelayCommand(FilterExecute, FilterCanExecute);
            PickDateCommand = new RelayCommand(PickDateExecute, PickDateCanExecute);
        }
        #region PickDateCommand
        public ICommand PickDateCommand { get; private set; }
        public bool PickDateCanExecute()
        {
            return SelectedRequest.Id > 0;
        }
        public void PickDateExecute()
        {
            SendMessage();
        }
        public void SendMessage()
        {
            var message = new ScheduleRequestMessage(this, SelectedRequest);
            MessageBus.Publish(message);
        }
        #endregion

        #region FilterCommand
        public ICommand FilterCommand { get; private set; }
        public bool FilterCanExecute()
        {
            return true;
        }
        public void FilterExecute()
        {
            CustomTourRequests = new(_requestService.FilterRequests(SelectedLocation, SelectedLanguage, NumberOfGuests, StartDate, EndDate));
        }
        #endregion
    }
}
