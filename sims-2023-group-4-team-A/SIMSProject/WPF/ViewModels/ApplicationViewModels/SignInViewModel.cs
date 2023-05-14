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
                if (user.Username.Equals("marko") && user.Password.Equals("marko"))
                {
                    OpenGuestWindow(user);
                }
                else if (user.Username.Equals("anja") && user.Password.Equals("anja"))
                {
                    OpenGuest2Window(user);
                }
                else
                {
                    OpenWindow(user);
                }
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
            MainWindow guestWind = new(user as Guest ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
            guestWind.Show();
        }

        private void OpenGuest2Window(User user)
        {
            Guest2HomeView guest2HomeView = new(user as Guest ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
            guest2HomeView.Show();
        }

        private void OpenWindow(User user)
        {
            if (user == null) return;
            switch (user.Role)
            {
                case UserRole.Owner or UserRole.SuperOwner:
                    user = _ownerRatingService.UpdateOwnerInfo(user);
                    //OwnerHomeViewOld ownerWindow = new(user
                    //    ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    //ownerWindow.Show();

                    //OwnerView ownerWindow = new(user ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    //ownerWindow.Show();

                    OwnerWindow ownerWindow = new(user);
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
