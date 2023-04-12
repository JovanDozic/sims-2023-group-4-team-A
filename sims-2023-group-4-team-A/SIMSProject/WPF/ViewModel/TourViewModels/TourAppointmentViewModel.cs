using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModel.TourViewModels
{
    public class TourAppointmentViewMode : INotifyPropertyChanged
    {
        private readonly TourAppointment? _tourAppointment;
        public int Id { get; set; }
        public int TourId { get; set; }
        public DateTime Date { get; set; }
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

        public TourAppointmentViewMode()
        {
            _tourAppointment = new();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
