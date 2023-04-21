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

namespace SIMSProject.WPF.ViewModels.ApplicationViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private string _username = string.Empty;
        private readonly UserService _userService;
        private readonly OwnerRatingService _ownerRatingService;

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
        public bool GuestSignIn(string password)
        {
            try
            {
                User? user = _userService.GetUser(Username, password) as User
                             ?? throw new Exception("Dogodila se greška prilikom logovanja.");
                OpenGuestWindow(user);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void OpenGuestWindow(User user)
        {
            Main guestWind = new(user as Guest ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
            guestWind.Show();
        }

        private void OpenWindow(User user)
        {
            if (user == null) return;
            switch (user.Role)
            {
                case UserRole.Owner or UserRole.SuperOwner:
                    user = _ownerRatingService.UpdateOwnerInfo(user);
                    OwnerHomeView ownerWindow = new(user
                        ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    ownerWindow.Show();
                    break;
                case UserRole.Guide or UserRole.SuperGuide:
                    GuideHomeWindow guideWindow = new(user as Guide
                        ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    guideWindow.Show();
                    break;
                case UserRole.Guest or UserRole.SuperGuest:
                    GuestInitialWindow guestWindow = new(user as Guest
                        ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    guestWindow.Show();
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
