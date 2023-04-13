using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModel.TourViewModels
{
    public class TourAppointmentViewModel : INotifyPropertyChanged
    {
        private TourAppointment? _tourAppointment;
        public TourAppointment TourAppointment
        {
            get => _tourAppointment;
            set
            {
                if (_tourAppointment != value)
                {
                    _tourAppointment = value;
                    OnPropertyChanged(nameof(TourAppointment));
                }
            }
        }

        public int Id
        {
            get => _tourAppointment.Id;
        }
        public int TourId
        {
            get => _tourAppointment.TourId;
        }
        public DateTime Date
        {
            get => _tourAppointment.Date;
            set
            {
                if(value != _tourAppointment.Date)
                {
                    _tourAppointment.Date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        public string TourStatus
        {
            get => _tourAppointment.TourStatus switch
            {
                Status.ACTIVE => "Aktivna",
                Status.INACTIVE => "Neaktivna",
                Status.CANCELED => "Otkazana",
                _ => "Završena"
            };
            set => _tourAppointment.TourStatus = value switch
            {
                "Aktivna" => Status.ACTIVE,
                "Neaktivna" => Status.INACTIVE,
                "Otkazana" => Status.CANCELED,
                _ => Status.COMPLETED
            };
        }

        public int CurrentKeyPointId
        {
            get => _tourAppointment.CurrentKeyPointId;
            set
            {
                    _tourAppointment.CurrentKeyPointId = value;
                    OnPropertyChanged(nameof(CurrentKeyPointId));
            }
        }
        public int AvailableSpots
        {
            get => _tourAppointment.AvailableSpots;
            set
            {
                    _tourAppointment.AvailableSpots  = value;
                    OnPropertyChanged(nameof(AvailableSpots));
            }
        }

        public Tour Tour 
        {
            get => _tourAppointment.Tour;
            set
            {
                _tourAppointment.Tour = value;
                OnPropertyChanged(nameof(Tour));
            }
        }
        public KeyPoint CurrentKeyPoint
        {
            get => _tourAppointment.CurrentKeyPoint;
            set
            {
                _tourAppointment.CurrentKeyPoint = value;
                OnPropertyChanged(nameof(CurrentKeyPoint));
            }
        }
        public List<Guest> Guests
        {
            get => _tourAppointment.Guests;
            set
            {
                _tourAppointment.Guests = value;
                OnPropertyChanged(nameof(Guests));
            }
        }

        public TourAppointmentViewModel(TourAppointment appointment)
        {
            _tourAppointment = appointment;
        }

        public TourAppointment GetAppointment()
        { return _tourAppointment; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
