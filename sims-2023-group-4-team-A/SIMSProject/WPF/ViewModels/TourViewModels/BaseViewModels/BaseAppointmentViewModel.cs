using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.TourViewModels.BaseViewModels
{
    public class BaseAppointmentViewModel : ViewModelBase
    {
        private TourAppointment? _tourAppointment = new();
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
        public Tour Tour
        {
            get => _tourAppointment.Tour;
        }
        public DateTime Date
        {
            get => _tourAppointment.Date;
            set
            {
                if (value != _tourAppointment.Date)
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

        public int AvailableSpots
        {
            get => _tourAppointment.AvailableSpots;
            set
            {
                _tourAppointment.AvailableSpots = value;
                OnPropertyChanged(nameof(AvailableSpots));
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
        public List<Guest> Guests = new();

        /* get => _tourAppointment.Guests;
         set
         {
             _tourAppointment.Guests = value;
             OnPropertyChanged(nameof(Guests));
         }*/


        public BaseAppointmentViewModel()
        {

        }
        public BaseAppointmentViewModel(TourAppointment appointment)
        {
            TourAppointment = appointment;
        }

    }
}
