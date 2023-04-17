using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class BaseTourGuestViewModel : ViewModelBase
    {
        private readonly TourGuest _tourGuest;
        public int AppointmentId
        {
            get => _tourGuest.TourAppointmentId;

            set
            {
                _tourGuest.TourAppointmentId = value;
                OnPropertyChanged(nameof(AppointmentId));

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

        public KeyPoint JoinedKeyPoint
        {
            get => _tourGuest.JoinedKeyPoint; set
            {
                _tourGuest.JoinedKeyPoint = value;
                OnPropertyChanged(nameof(JoinedKeyPoint));
            }
        }

        public BaseTourGuestViewModel()
        {
            _tourGuest = new();
        }

        public BaseTourGuestViewModel(TourGuest tourGuest)
        {
            _tourGuest = tourGuest;
        }
    }
}
