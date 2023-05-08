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
        private TourAppointment _tourAppointment = new();
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


        public string KPString
        {
            get => CurrentKeyPoint.ToString();
        }

        public KeyPoint CurrentKeyPoint
        {
            get => _tourAppointment.CurrentKeyPoint;
            set
            {
                if (value == _tourAppointment.CurrentKeyPoint) return;
                _tourAppointment.CurrentKeyPoint = value;
                OnPropertyChanged(nameof(CurrentKeyPoint));
                OnPropertyChanged(nameof(KPString));
            }
        }
        public List<Guest> Guests = new();

        public BaseAppointmentViewModel()
        {

        }
        public BaseAppointmentViewModel(TourAppointment appointment)
        {
            TourAppointment = appointment;
        }

    }
}
