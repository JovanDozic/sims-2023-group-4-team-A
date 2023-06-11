using SIMSProject.Application.Services;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.Guest2Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class CustomTourRequestViewModel : ViewModelBase
    {
        #region Polja
        private Guest2 _user;
        private CustomTourRequest _customTourRequest = new();
        private CustomTourRequestService _customTourRequestService;
        private ComplexTourRequestService _complexTourRequestService;
        private LocationService _locationService;
        public ObservableCollection<Location> AllLocations { get; set; } = new();
        public List<string> TourLanguages { get; set; }
        public CustomTourRequest CustomTourRequest
        {
            get => _customTourRequest;
            set
            {
                if( _customTourRequest == value) return;
                _customTourRequest = value;
                OnPropertyChanged();
            }
        }
        public Location Location
        {
            get => _customTourRequest.Location;
            set
            {
                if (_customTourRequest.Location == value) return;
                _customTourRequest.Location = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _customTourRequest.Description;
            set
            {
                if(_customTourRequest.Description == value) return;
                _customTourRequest.Description = value;
                OnPropertyChanged();
            }
        }
        public string TourLanguage
        {
            get
            {
                return _customTourRequest.TourLanguage switch
                {
                    Language.SERBIAN => "Srpski",
                    Language.SPANISH => "Španski",
                    Language.FRENCH => "Francuski",
                    _ => "Engleski"

                };
            }
            set
            {
                _customTourRequest.TourLanguage = value switch
                {
                    "Srpski" => Language.SERBIAN,
                    "Španski" => Language.SPANISH,
                    "Francuski" => Language.FRENCH,
                    _ => Language.ENGLISH
                };
                OnPropertyChanged(nameof(TourLanguage));
            }
        }
        public DateTime StartDate
        {
            get => _customTourRequest.StartDate;
            set
            {
                if(_customTourRequest.StartDate == value) return;
                _customTourRequest.StartDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndDate
        {
            get => _customTourRequest.EndDate;
            set
            {
                if (_customTourRequest.EndDate == value)  return;
                _customTourRequest.EndDate = value;
                OnPropertyChanged();
            }
        }
        public int GuestCount
        {
            get => _customTourRequest.GuestCount;
            set
            {
                if(_customTourRequest.GuestCount == value || value < 1 ) return;
                _customTourRequest.GuestCount = value;
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<CustomTourRequest> _customTourRequests = new();
        public ObservableCollection<CustomTourRequest> CustomTourRequests
        {
            get => _customTourRequests;
            set
            {
                if(value == _customTourRequests) return;
                _customTourRequests = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ComplexTourRequest> _complexTourRequests = new();
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests
        {
            get => _complexTourRequests;
            set
            {
                if (value == _complexTourRequests) return;
                _complexTourRequests = value;
                OnPropertyChanged();
            }
        }
        private ComplexTourRequest _selectedComplexTourRequest = new();
        public ComplexTourRequest SelectedComplexTourRequest
        {
            get => _selectedComplexTourRequest;
            set
            {
                if (value == _selectedComplexTourRequest) return;
                _selectedComplexTourRequest = value;
                OnPropertyChanged(nameof(SelectedComplexTourRequest));
            }
        }
        public string DateRange { get; set; } = string.Empty;
        public NavigationService NavService { get; set; }
        public RelayCommand TourRequestStatisticsCommand { get; set; }
        public RelayCommand NewTourRequestCommand { get; set; }
        public RelayCommand ComplexTourRequestDetailsCommand { get; set; }
        public RelayCommand NewComplexRequestCommand { get; set; }
        #endregion

        #region Konstruktori
        public CustomTourRequestViewModel(Guest2 user, NavigationService navigationService) 
        {
            TourLanguages = new()
            {
                "Engleski",
                "Srpski",
                "Francuski",
                "Španski"
            };
            this.NavService = navigationService;
            _user = user;
            _customTourRequestService = Injector.GetService<CustomTourRequestService>();
            _complexTourRequestService = Injector.GetService<ComplexTourRequestService>();
            _locationService = Injector.GetService<LocationService>();
            AllLocations = new(_locationService.GetAll());
            LoadTourRequestsByGuestId(_user.Id);
            LoadComplexTourRequestsByGuestId(_user.Id);
            CheckRequestValidity(CustomTourRequests.ToList());
            CheckComplexRequestsValidity(ComplexTourRequests.ToList(), _user.Id);
            CheckComplexRequestAcceptance(ComplexTourRequests.ToList(), _user.Id);

            TourRequestStatisticsCommand = new RelayCommand(TourRequestStatisticsCommandExecute, CanExecute_Command);
            NewTourRequestCommand = new RelayCommand(NewTourRequestCommandExecute, CanExecute_Command);
            ComplexTourRequestDetailsCommand = new RelayCommand(ComplexTourRequestDetailsCommandExecute, CanExecute_Command);
            NewComplexRequestCommand = new RelayCommand(NewComplexRequestCommandExecute, CanExecute_Command);
        }
        #endregion

        #region Akcije
        private void TourRequestStatisticsCommandExecute()
        {
            NavService.Navigate(new TourRequestStatistics(_user, NavService));
        }
        private void NewTourRequestCommandExecute()
        {
            NavService.Navigate(new CustomTourRequestCreation(_user, NavService));
        }
        private void ComplexTourRequestDetailsCommandExecute()
        {
            NavService.Navigate(new ComplexTourRequestDetails(_user, SelectedComplexTourRequest, NavService));
        }
        private void NewComplexRequestCommandExecute()
        {
            NavService.Navigate(new ComplexTourRequestCreation(_user, NavService));
        }
        private bool CanExecute_Command()
        {
            return true;
        }
        public void LoadTourRequestsByGuestId(int guestId)
        {
            CustomTourRequests = new(_customTourRequestService.GetAllByGuestId(guestId));
        }
        public void LoadComplexTourRequestsByGuestId(int guestId)
        {
            ComplexTourRequests = new(_complexTourRequestService.GetAllByGuestId(guestId));
        }
        public void CheckRequestValidity(List<CustomTourRequest> customTourRequests)
        {
            _customTourRequestService.CheckRequestValidity(customTourRequests);
        }
        public void CheckComplexRequestsValidity(List<ComplexTourRequest> complexTourRequests, int guestId)
        {
            _complexTourRequestService.CheckComplexRequestValidity(complexTourRequests, guestId);
        }
        public void CheckComplexRequestAcceptance(List<ComplexTourRequest> complexTourRequests, int guestId)
        {
            _complexTourRequestService.CheckComplexRequestAcceptance(complexTourRequests, guestId);
        }
        #endregion
    }
}
