﻿using SIMSProject.Application.Services.TourServices;
using SIMSProject.Application.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SIMSProject.Domain.Models;
using SIMSProject.WPF.Views.Guest2Views;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class ComplexTourRequestViewModel : ViewModelBase
    {
        private Guest2 _user;
        private ComplexTourRequestService _complexTourRequestService;
        private CustomTourRequestService _customTourRequestService;
        private LocationService _locationService;
        private CustomTourRequest _customTourRequest = new();
        public CustomTourRequest CustomTourRequest
        {
            get => _customTourRequest;
            set
            {
                if (_customTourRequest == value) return;
                _customTourRequest = value;
                OnPropertyChanged();
            }
        }
        private ComplexTourRequest _complexTourRequest = new();
        public ComplexTourRequest ComplexTourRequest
        {
            get => _complexTourRequest;
            set
            {
                if (_complexTourRequest == value) return;
                _complexTourRequest = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Location> AllLocations { get; set; } = new();
        public List<string> TourLanguages { get; set; }
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
                if (_customTourRequest.Description == value) return;
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
                if (_customTourRequest.StartDate == value) return;
                _customTourRequest.StartDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndDate
        {
            get => _customTourRequest.EndDate;
            set
            {
                if (_customTourRequest.EndDate == value) return;
                _customTourRequest.EndDate = value;
                OnPropertyChanged();
            }
        }
        public int GuestCount
        {
            get => _customTourRequest.GuestCount;
            set
            {
                if (_customTourRequest.GuestCount == value || value < 1) return;
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
                if (value == _customTourRequests) return;
                _customTourRequests = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<CustomTourRequest> _tourRequestParts = new();
        public ObservableCollection<CustomTourRequest> TourRequestParts
        {
            get => _tourRequestParts;
            set
            {
                if (value == _tourRequestParts) return;
                _tourRequestParts = value;
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

        public ComplexTourRequestViewModel(Guest2 user, ComplexTourRequest complexTourRequest = null)
        {
            TourLanguages = new()
            {
                "Engleski",
                "Srpski",
                "Francuski",
                "Španski"
            };
            _user = user;
            _complexTourRequestService = Injector.GetService<ComplexTourRequestService>();
            _customTourRequestService = Injector.GetService<CustomTourRequestService>();
            _locationService = Injector.GetService<LocationService>();
            AllLocations = new(_locationService.GetAll());
            SelectedComplexTourRequest = complexTourRequest;
            
            
            //LoadTourRequestsByGuestId(_user.Id);
            //CheckRequestValidity(CustomTourRequests.ToList());
        }

        public void CreatePart()
        {
            CustomTourRequest newRequest = new(1, _user.Id, Location.Id, Description, _customTourRequest.TourLanguage, GuestCount, StartDate, EndDate, DateTime.Now, RequestStatus.ONHOLD, _complexTourRequestService.NextId());
            //_customTourRequestService.Save(newRequest);
            CustomTourRequests.Add(newRequest);
        }

        public void CreateRequest()
        {
            _complexTourRequest.Guest.Id = _user.Id;
            _complexTourRequest.Name = "Složen zahtev " + _complexTourRequestService.NextId();
            _complexTourRequest.Status = RequestStatus.ONHOLD;
            _complexTourRequestService.Save(_complexTourRequest);
            foreach (var tourRequest in CustomTourRequests)
            {
                _customTourRequestService.Save(tourRequest);
            }
            CustomTourRequests.Clear();
        }

        public void GetParts()
        {
            TourRequestParts =new ObservableCollection<CustomTourRequest>(_customTourRequestService.GetAllComplexTourParts(SelectedComplexTourRequest.Id));
        }
    }
}