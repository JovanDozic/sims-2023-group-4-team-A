using SIMSProject.Domain.Injectors;
using SIMSProject.View.GuideViews;
using SIMSProject.View;
using System;
using System.Windows;
using SIMSProject.Domain.Models.UserModels;
using System.ComponentModel;
using SIMSProject.Domain.Models;
using System.Runtime.CompilerServices;
using SIMSProject.WPF.Views.OwnerViews;
using SIMSProject.Model;
using SIMSProject.Application.Services.UserServices;

namespace SIMSProject.WPF.ViewModels.ApplicationViewModels
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        private string _username = string.Empty;
        private readonly UserService _userService;

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
        }

        public void SignIn(string password)
        {
            try
            {
                User? user = _userService.GetUser(Username, password) as User
                             ?? throw new Exception("Dogodila se greška prilikom logovanja.");
                OpenWindow(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenWindow(User user)
        {
            if (user == null) return;
            switch (user.Role)
            {
                case UserRole.Owner or UserRole.SuperOwner:
                    OwnerHomeView ownerWindow = new(user as User
                        ?? throw new Exception("Greska prilikom inicijalizacije korisnika (null reference)."));
                    ownerWindow.Show();
                    break;
                case UserRole.Guide or UserRole.SuperGuide:
                    GuideInitialWindow guideWindow = new(user as Guide
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
