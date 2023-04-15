using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;
using SIMSProject.Domain.Models;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class BaseTourViewModel : ViewModelBase
    {
        protected Tour _tour;

        public Tour Tour
        {
            get => _tour;
            set
            {
                if(value != _tour)
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

        public int GuideId
        {
            get => _tour.GuideId;
            set
            {
                if (_tour.GuideId != value)
                {
                    _tour.GuideId = value;
                    OnPropertyChanged(nameof(GuideId));
                }

            }
        }

        public int LocationId
        {
            get => _tour.LocationId;
            set
            {
                if (value != _tour.LocationId)
                {
                    _tour.LocationId = value;
                    OnPropertyChanged(nameof(LocationId));
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

        public Location Location
        {
            get { return _tour.Location; }
            set
            {
                if (_tour.Location != value)
                {
                    _tour.Location = value;
                    LocationId = value.Id;
                    OnPropertyChanged(nameof(Location));
                }

            }
        }
        

        public List<KeyPoint> KeyPoints
        {
            get => _tour.KeyPoints;
            set 
            {   
                if(value != _tour.KeyPoints)
                {
                    _tour.KeyPoints = value;
                    OnPropertyChanged(nameof(KeyPoints));
                }
            }
        }

        public List<TourAppointment> Appointments
        {
            get => _tour.Appointments;
            set 
            {       
                if(value != _tour.Appointments)
                {
                    _tour.Appointments = value;
                    OnPropertyChanged(nameof(Appointments));
                }
            }
        }

        public List<string> Images
        {
            get => _tour.Images;
            set
            {
                if(value != _tour.Images)
                {
                    _tour.Images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }
        public BaseTourViewModel(Tour tour)
        {
            _tour = tour;
        }

        public string KeyPointsToString()
        {
            return _tour.KeyPointsToString();
        }

    }
}
