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
    public class TourGuestViewModel : INotifyPropertyChanged
    {
        private readonly TourGuest _tourGuest;
        public int AppointmentId
        {
            get => _tourGuest.AppointmentId;

            set
            {
                _tourGuest.AppointmentId = value;
                OnPropertyChanged(nameof(AppointmentId));

            }
        }
        public int GuestId
        {
            get => _tourGuest.GuestId;
            set
            {
                _tourGuest.GuestId = value;
                OnPropertyChanged(nameof(GuestId));
            }
        }
        public int JoinedKeyPointId
        {
            get => _tourGuest.JoinedKeyPointId;
            set
            {
                _tourGuest.JoinedKeyPointId = value;
                OnPropertyChanged(nameof(JoinedKeyPointId));
            }
        }
        public string GuestStatus
        {
            get
            {
                return _tourGuest.GuestStatus switch
                {
                    GuestAttendance.PRESENT => "Prisutan",
                    GuestAttendance.ABSENT => "Odsutan",
                    _ => "Prijavljen"
                };
            }
            set
            {
                _tourGuest.GuestStatus = value switch
                {
                    "Prisutan" => GuestAttendance.PRESENT,
                    "Odsutan" => GuestAttendance.ABSENT,
                    _ => GuestAttendance.PENDING
                };
                OnPropertyChanged(nameof(GuestStatus));
            }

        }

        public Guest Guest
        {
            get => _tourGuest.Guest;
            set
            {
                _tourGuest.Guest = value;
                OnPropertyChanged(nameof(Guest));
            }
        }
        public TourAppointment Appointment
        {
            get => (TourAppointment)_tourGuest.Appointment;
            set
            {
                _tourGuest.Appointment = value;
                OnPropertyChanged(nameof(Appointment));
            }
        }

        public KeyPoint JoinedKeyPoint
        {
            get => _tourGuest.JoinedKeyPoint; set
            {
                _tourGuest.JoinedKeyPoint = value;
                OnPropertyChanged(nameof(JoinedKeyPoint));
            }
        }

        public TourGuestViewModel()
        {
            _tourGuest = new();
        }

        public TourGuestViewModel(TourGuest tourGuest)
        {
            _tourGuest = tourGuest;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
