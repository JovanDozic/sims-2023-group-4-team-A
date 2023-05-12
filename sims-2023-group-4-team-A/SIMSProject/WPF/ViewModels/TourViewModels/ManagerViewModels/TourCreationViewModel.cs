using SIMSProject.Application.Services;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModels.TourViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels
{
    public class TourCreationViewModel : ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourKeyPointService _tourKeyPointService;
        private readonly LocationService _locationService;
        private readonly KeyPointService _keyPointService;

        private Tour _tour = new();
        public Tour Tour
        {
            get => _tour;
            set
            {
                if (value != _tour)
                {
                    _tour = value;
                    OnPropertyChanged(nameof(Tour));
                }
            }
        }
        public int Id
        {
            get { return _tour.Id; }
        }

        public Guide Guide
        {
            get => _tour.Guide;
            set
            {
                if (_tour.Guide != value)
                {
                    _tour.Guide = value;
                    OnPropertyChanged(nameof(Guide));
                }

            }
        }

        public string Name
        {
            get { return _tour.Name; }
            set
            {
                if (_tour.Name != value)
                {
                    _tour.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Description
        {
            get { return _tour.Description; }
            set
            {
                if (value != _tour.Description)
                {
                    _tour.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public ObservableCollection<string> TourLanguages { get; set; }

        public string TourLanguage
        {
            get
            {
                return _tour.TourLanguage switch
                {
                    Language.ENGLISH => "Engleski",
                    Language.SERBIAN => "Srpski",
                    Language.SPANISH => "Španski",
                    _ => "Francuski"

                };
            }
            set
            {
                _tour.TourLanguage = value switch
                {
                    "Engleski" => Language.ENGLISH,
                    "Srpski" => Language.SERBIAN,
                    "Španski" => Language.SPANISH,
                    _ => Language.FRENCH
                };
                OnPropertyChanged(nameof(TourLanguage));
            }
        }

        public int MaxGuestNumber
        {
            get => _tour.MaxGuestNumber;
            set
            {
                if (value >= 1)
                {
                    _tour.MaxGuestNumber = value;
                    OnPropertyChanged(nameof(MaxGuestNumber));
                }
            }
        }

        public int Duration
        {
            get => _tour.Duration;
            set
            {
                if (value >= 1)
                {
                    _tour.Duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        public List<KeyPoint> KeyPoints
        {
            get => _tour.KeyPoints;
            set
            {
                if (value != _tour.KeyPoints)
                {
                    _tour.KeyPoints = value;
                    OnPropertyChanged(nameof(KeyPoints));
                }
            }
        }


        private List<TourAppointment> _appointments = new();
        public List<TourAppointment> Appointments
        {
            get => _appointments;
            set
            {
                if (value != _appointments)
                {
                    _appointments = value;
                    OnPropertyChanged(nameof(Appointments));
                }
            }
        }

        public List<string> Images
        {
            get => _tour.Images;
            set
            {
                if (value != _tour.Images)
                {
                    _tour.Images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }


        private int hours;
        public int Hours
        {
            get => hours;
            set
            {
                if (hours != value && value >= 0 && value <= 24)
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
                if (minutes != value && value >= 0 && value <= 60)
                {
                    minutes = value;
                    OnPropertyChanged(nameof(Hours));
                }

            }
        }

        public Location Location
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
        public ObservableCollection<Location> AllLocations { get; set; } = new();
        public ObservableCollection<KeyPoint> Keys { get; set; } = new();
        public KeyPoint? SelectedKeyPoint { get; set; }
        public DateTime SelectedAppointment { get; set; } = DateTime.Now;
        public TourCreationViewModel(Guide guide)
        {
            _tourService = Injector.GetService<TourService>();
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourKeyPointService = Injector.GetService<TourKeyPointService>();
            _locationService = Injector.GetService<LocationService>();
            _keyPointService = Injector.GetService<KeyPointService>();

            AllLocations = new(_locationService.FindAll());
            Guide = guide;

            TourLanguages = new ObservableCollection<string>
            {
                Tour.GetLanguage(Language.SERBIAN),
                Tour.GetLanguage(Language.ENGLISH),
                Tour.GetLanguage(Language.SPANISH),
                Tour.GetLanguage(Language.FRENCH)
            };
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
            if (MaxGuestNumber <= 0)
            {
                MessageBox.Show("Morate uneti broj gostiju  na turi!");
                return;
            }

            DateTime newDate = new(SelectedAppointment.Year, SelectedAppointment.Month, SelectedAppointment.Day, Hours, Minutes, 0);
            Appointments.Add(new(newDate, -1, MaxGuestNumber, -1, Guide.Id));
        }
    }
}
