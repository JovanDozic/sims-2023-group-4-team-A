using SIMSProject.Domain.Injectors;
using SIMSProject.View.GuideViews;
using SIMSProject.View;
using System;
using System.Windows;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.Models;
using SIMSProject.WPF.Views.OwnerViews;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.WPF.Views;
using SIMSProject.WPF.Views.Guest1;
using SIMSProject.WPF.Views.Guest1.MainView;
using SIMSProject.WPF.Views.Guest2Views;

namespace SIMSProject.WPF.ViewModels.ApplicationViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private string _username = string.Empty;
        private readonly UserService _userService;
        private readonly OwnerRatingService _ownerRatingService;
        private readonly AccommodationReservationService _reservationService;

        public string Username
        {
            get => _username;
            set
            {
                if (value == _username) return;
                _username = value;
                OnPropertyChanged();
            }
        }

        public SignInViewModel()
        {
            _userService = Injector.GetService<UserService>();
            _ownerRatingService = Injector.GetService<OwnerRatingService>();
            _reservationService = Injector.GetService<AccommodationReservationService>();
        }

        public bool SignIn(string password)
        {
            try
            {
                User? user = _userService.GetUser(Username, password) as User
                             ?? throw new Exception("Dogodila se greška prilikom logovanja.");

                OpenWindow(user);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

        private void OpenWindow(User user)
        {
            if (user == null) return;
            switch (user.Role)
            {
                case UserRole.Owner or UserRole.SuperOwner:
                    user = _ownerRatingService.UpdateOwnerInfo(user);
                    //OwnerHomeViewOld ownerWindow = new(_user
                    //    ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    //ownerWindow.Show();

                    //OwnerView ownerWindow = new(_user ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    //ownerWindow.Show();

                    OwnerWindow ownerWindow = new(user);
                    ownerWindow.Show();
                    break;
                case UserRole.Guide or UserRole.SuperGuide:
                    var guide = user as Guide ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference).");
                    if (guide.Quit)
                    {
                        MessageBox.Show("Dali ste otkaz! Vaš nalog je blokiran!");
                        return;
                    }
                    GuideHomeWindow guideWindow = new(guide);
                    guideWindow.Show();
                    break;
                case UserRole.Guest1 or UserRole.SuperGuest:
                    user = _reservationService.UpdateGuestInfo(user);
                    MainWindow guestWind = new(user as Guest1 ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    guestWind.Show();
                    break;                 
                case UserRole.Guest2:
                    Guest2HomeView guest2HomeView = new(user as Guest2 ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    guest2HomeView.Show();
                    break;
                    

            }
        }

        public void DirectSignIn(string username, string password)
        {
            Username = username;
            SignIn(password);
        }
    }
}
