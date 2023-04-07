using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using SIMSProject.View.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.WPF.ViewModel.TourViewModels
{
    public class TourViewModel : INotifyPropertyChanged
    {
        private readonly Tour _tour;
        private readonly TourService _tourService = new();

        public int Id
        {
            get { return _tour.Id; }
        }

        public int GuideId
        {
            get =>  _tour.GuideId;
            set
            {
                    _tour.GuideId = value;
                    OnPropertyChanged(nameof(GuideId));
            }
        }

        public int LocationId
        {
            get => _tour.LocationId;
            set
            {
                    _tour.LocationId = value;
                    OnPropertyChanged(nameof(LocationId));
            }
        }

        public string Name 
        {
            get { return _tour.Name; }
            set { _tour.Name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Description
        {
            get { return _tour.Description; }
            set { _tour.Description = value; OnPropertyChanged(nameof(Description)); }
        }

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

        public Guide Guide
        { 
            get => _tour.Guide;
            set { _tour.Guide = value; OnPropertyChanged(nameof(Guide));}
        }

        public Location Location
        {
            get { return _tour.Location; }
            set 
            {
                if (_tour.Location != value)
                {
                    _tour.Location = value;
                    LocationId = value.Id;


                    KeyPoints.Clear();

                    foreach (var point in GuideInitialWindow.keyPointController.GetAll().FindAll(x => x.LocationId == value.Id))
                    {
                        KeyPoints.Add(point);
                    }
                    OnPropertyChanged(nameof(Location));
                }
                
            }
        }

        public List<KeyPoint> Keys
        {
            get => _tour.KeyPoints;
            set { _tour.KeyPoints = value; OnPropertyChanged(nameof(Keys)); }
        }

        public List<TourAppointment> Appointments
        {
            get => _tour.Appointments;
            set { _tour.Appointments = value; OnPropertyChanged(nameof(Appointments)); }
        }

        public List<string> Images
        {
            get => _tour.Images;
            set { _tour.Images = value; OnPropertyChanged(nameof(Images)); }
        }

        public List<string> TourLanguages { get; set; }
        public ObservableCollection<Location> AllLocations { get; set; } = new();
        public ObservableCollection<KeyPoint> KeyPoints { get; set; } = new();
        public KeyPoint? SelectedKeyPoint { get; set; }
        public DateTime SelectedAppointment { get; set; } = DateTime.Now;

        public TourViewModel()
        {
            _tour = new();
            TourLanguages = new()
            {
                "Srpski",
                "Engleski",
                "Francuski",
                "Španski"
            };
            AllLocations = new(GuideInitialWindow.locationController.GetAll());
        }

        public TourViewModel(Tour tour)
        {
            _tour = tour;
            TourLanguages = new()
            {
                "Srpski",
                "Engleski",
                "Francuski",
                "Španski"
            };
        }

        public void CreateTour()
        {
            _tourService.CreateTour(_tour);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
