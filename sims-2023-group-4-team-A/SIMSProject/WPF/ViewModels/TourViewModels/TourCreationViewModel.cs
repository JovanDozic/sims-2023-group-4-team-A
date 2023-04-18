using SIMSProject.Application.Services;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class TourCreationViewModel : BaseTourViewModel
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourKeyPointService _tourKeyPointService;
        private readonly LocationService _locationService;
        private readonly KeyPointService _keyPointService;


        private int hours;
        public int Hours
        {
            get => hours;
            set
            {
                if(hours != value && (value >= 0 && value <= 24))
                {
                    hours = value;
                    OnPropertyChanged(nameof(Hours));
                }
                
            }
        }

        private int minutes;
        public int Minutes
        {
            get => minutes;
            set
            {
                if (minutes != value && (value >=0 && value <= 60))
                {
                    minutes = value;
                    OnPropertyChanged(nameof(Hours));
                }

            }
        }

        public new Location Location
        {
            get { return _tour.Location; }
            set
            {
                if (_tour.Location != value)
                {
                    _tour.Location = value;
                    Location.Id = value.Id;
                    Keys.Clear();
                    foreach (var point in _keyPointService.FindAll().FindAll(x => x.Location.Id == _tour.Location.Id))
                    {
                        Keys.Add(point);
                    }
                    OnPropertyChanged(nameof(Location));
                }

            }
        }
        public List<string> TourLanguages { get; set; }
        public ObservableCollection<Location> AllLocations { get; set; } = new();
        public ObservableCollection<KeyPoint> Keys { get; set; } = new();
        public KeyPoint? SelectedKeyPoint { get; set; }
        public DateTime SelectedAppointment { get; set; } = DateTime.Now;
        public TourCreationViewModel(Guide guide) : base(new())
        {
            TourLanguages = new()
            {
                "Srpski",
                "Engleski",
                "Francuski",
                "Španski"
            };

            _tourService = Injector.GetService<TourService>();
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourKeyPointService = Injector.GetService<TourKeyPointService>();
            _locationService = Injector.GetService<LocationService>();
            _keyPointService = Injector.GetService<KeyPointService>();

            AllLocations = new(_locationService.FindAll());
            Guide = guide;
        }

        public void CreateTour()
        {
            _tourService.CreateTour(_tour);
            _tourAppointmentService.CreateAppointments(Appointments.ToList(), _tour);
            _tourKeyPointService.CreateNewPairs(_tour);
            MessageBox.Show("Tura uspešno kreirana.");
        }

        public bool IsNotValid()
        {
            return KeyPoints.Count < 2 || Images.Count < 1;
        }

        public void AddKeyPoint()
        {
            if (SelectedKeyPoint == null)
            {
                MessageBox.Show("Nije moguće izabrati ključnu tačku koja ne postoji!");
                return;
            }
            KeyPoints.Add(SelectedKeyPoint);
        }

        public void AddAppointment()
        {
            if(MaxGuestNumber <= 0)
            {
                MessageBox.Show("Morate uneti broj gostiju  na turi!");
                return;
            }

            int seconds = 0;
            DateTime newDate = new(SelectedAppointment.Year, SelectedAppointment.Month, SelectedAppointment.Day, Hours, Minutes, seconds);
            Appointments.Add(new(newDate, -1, MaxGuestNumber, -1, Guide.Id));
        }
    }
}
