using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.Guest1ViewModels
{
    public class SuperGuestViewModel: ViewModelBase
    {
        private readonly User _user = new();
        private readonly AccommodationReservationService _reservationService;
        private Guest _guest = new();

        public string Username
        {
            get => _user.Username;
            set
            {
                if (_user.Username == value) return;
                _user.Username = value;
                OnPropertyChanged();
            }
        }
        public UserRole Status
        {
            get => _user.Role;
            set
            {
                if (_user.Role == value) return;
                _user.Role = value;
                OnPropertyChanged();
            }
        }
        public int BonusPoints
        {
            get => _guest.BonusPoints;
            set
            {
                if (_guest.BonusPoints == value) return;
                _guest.BonusPoints = value;
                OnPropertyChanged();
            }
        }

        public SuperGuestViewModel(User user)
        {
            _user = user;
            _reservationService = Injector.GetService<AccommodationReservationService>();
            _guest = _reservationService.GetGuestByUser(user);
        }
    }
}
