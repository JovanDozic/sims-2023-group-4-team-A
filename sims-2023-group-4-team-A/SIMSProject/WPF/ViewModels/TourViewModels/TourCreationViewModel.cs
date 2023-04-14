using SIMSProject.Application.Services;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public new Location Location
        {
            get { return _tour.Location; }
            set
            {
                if (_tour.Location != value)
                {
                    _tour.Location = value;
                    LocationId = value.Id;
                    Keys.Clear();
                    foreach (var point in _keyPointService.FindAll().FindAll(x => x.LocationId == _tour.LocationId))
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

            Guide = guide;
            AllLocations = new(_locationService.FindAll());
        }

        public void CreateTour()
        {
            _tourService.CreateTour(_tour);
            _tourAppointmentService.CreateAppointments(_tour.Appointments, _tour);
            _tourKeyPointService.CreateNewPairs(_tour);
            MessageBox.Show("Tura uspešno kreirana.");

        }

        public bool IsValid()
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
    }
}
