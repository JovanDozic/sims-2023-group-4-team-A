using SIMSProject.Application.Services;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class CustomTourRequestViewModel : ViewModelBase
    {
        private Guest _user;
        private CustomTourRequest _customTourRequest = new();
        private CustomTourRequestService _customTourRequestService;
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
                    Language.ENGLISH => "Engleski",
                    Language.SERBIAN => "Srpski",
                    Language.SPANISH => "Španski",
                    _ => "Francuski"

                };
            }
            set
            {
                _customTourRequest.TourLanguage = value switch
                {
                    "Engleski" => Language.ENGLISH,
                    "Srpski" => Language.SERBIAN,
                    "Španski" => Language.SPANISH,
                    _ => Language.FRENCH
                };
                OnPropertyChanged();
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
                if(_customTourRequest.GuestCount == value || value < 1) return;
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
        public string DateRange { get; set; } = string.Empty;
        //private ObservableCollection<string> _requestStatusSource = new();
        //public ObservableCollection<string> RequestStatusSource
        //{
        //    get => _requestStatusSource;
        //    set
        //    {
        //        if(value == _requestStatusSource) return;
        //        _requestStatusSource = value;
        //        OnPropertyChanged();
        //    }
        //}

        public CustomTourRequestViewModel(Guest user) 
        {
            TourLanguages = new()
            {
                "Srpski",
                "Engleski",
                "Francuski",
                "Španski"
            };
            _user = user;
            _customTourRequestService = Injector.GetService<CustomTourRequestService>();
            _locationService = Injector.GetService<LocationService>();
            AllLocations = new(_locationService.FindAll());
            LoadTourRequestsByGuestId(_user.Id);
            CheckRequestValidity(CustomTourRequests.ToList());

            //RequestStatusSource = new ObservableCollection<string>
            //{
            //    CustomTourRequest.GetStatus(RequestStatus.ONHOLD),
            //    CustomTourRequest.GetStatus(RequestStatus.INVALID),
            //    CustomTourRequest.GetStatus(RequestStatus.ACCEPTED)
            //};
        }
        //public void LoadDatePicker(object sender)
        //{
        //    if (sender is DatePicker datePicker)
        //    {
        //        datePicker.SelectedDate = DateTime.Today.AddDays(2);
        //        datePicker.DisplayDateStart = DateTime.Today.AddDays(2);
        //    }
        //}
        public void CreateRequest()
        {
            _customTourRequest.Guest.Id = _user.Id;
            _customTourRequest.RequestCreateDate = DateTime.Now;
            _customTourRequest.RequestStatus = RequestStatus.ONHOLD;
            _customTourRequestService.Save(_customTourRequest);
        }

        public void LoadTourRequestsByGuestId(int guestId)
        {
            CustomTourRequests = new(_customTourRequestService.GetAllByGuestId(guestId));
        }
        public void CheckRequestValidity(List<CustomTourRequest> customTourRequests)
        {
            _customTourRequestService.CheckRequestValidity(customTourRequests);
        }
    }
}
