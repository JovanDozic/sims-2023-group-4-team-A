using Microsoft.TeamFoundation.Common;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using SIMSProject.Application.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.Guest2;
using SIMSProject.WPF.Views;
using SIMSProject.WPF.Views.Guest2Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class HomepageViewModel : ViewModelBase
    {
        #region Polja
        private Guest2 _user;
        private readonly NotificationService _service;
        public NavigationService NavService { get; set; }
        public RelayCommand NavigateToShowAndSearchToursPageCommand { get; set; }
        public RelayCommand NavigateToMyReservationsPageCommand { get; set; }
        public RelayCommand NavigateToTourRequestsPageCommand { get; set; }
        public RelayCommand NavigateToMyVouchersPageCommand { get; set; }
        public RelayCommand NavigateToMyNotificationsPageCommand { get; set; }
        public RelayCommand NavigateLogOutCommand { get; set; } 
        public Guest2HomeView Guest2HomeView { get; set; }
        
        private SolidColorBrush _button1Color;
        public SolidColorBrush Button1Color
        {
            get { return _button1Color; }
            set
            {
                _button1Color = value;
                OnPropertyChanged(nameof(Button1Color));
            }
        }
        private SolidColorBrush _button2Color;
        public SolidColorBrush Button2Color
        {
            get { return _button2Color; }
            set
            {
                _button2Color = value;
                OnPropertyChanged(nameof(Button2Color));
            }
        }
        private SolidColorBrush _button3Color;
        public SolidColorBrush Button3Color
        {
            get { return _button3Color; }
            set
            {
                _button3Color = value;
                OnPropertyChanged(nameof(Button3Color));
            }
        }
        private SolidColorBrush _button4Color;
        public SolidColorBrush Button4Color
        {
            get { return _button4Color; }
            set
            {
                _button4Color = value;
                OnPropertyChanged(nameof(Button4Color));
            }
        }
        private SolidColorBrush _button5Color;
        public SolidColorBrush Button5Color
        {
            get { return _button5Color; }
            set
            {
                _button5Color = value;
                OnPropertyChanged(nameof(Button5Color));
            }
        }
        #endregion
        
        #region Konstruktori
        public HomepageViewModel(User user, Guest2HomeView guest2HomeView, NavigationService navigationService)
        {
            _user = (Guest2)user;
            Guest2HomeView = guest2HomeView;
            NavService = navigationService;
            _service = Injector.GetService<NotificationService>();

            CheckTourPresenceNotifications();

            NavService.Navigate(new ShowAndSearchTours(_user, NavService));
            Button1Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff6700"));

            NavigateToShowAndSearchToursPageCommand = new RelayCommand(Execute_NavigateToShowAndSearchToursPageCommand, CanExecute_Command);
            NavigateToMyReservationsPageCommand = new RelayCommand(Execute_NavigateToMyReservationsPageCommand, CanExecute_Command);
            NavigateToTourRequestsPageCommand = new RelayCommand(Execute_NavigateToTourRequestsPageCommand, CanExecute_Command);
            NavigateToMyVouchersPageCommand = new RelayCommand(Execute_NavigateToMyVouchersPageCommand, CanExecute_Command);
            NavigateToMyNotificationsPageCommand = new RelayCommand(Execute_NavigateToMyNotificationsPageCommand, CanExecute_Command);
            NavigateLogOutCommand = new RelayCommand(Execute_NavigateLogOutCommand, CanExecute_Command);
        }
        #endregion

        #region Akcije
        
        private void Execute_NavigateToShowAndSearchToursPageCommand()
        {
            ShowAndSearchTours showAndSearchTours = new ShowAndSearchTours(_user, NavService);
            NavService.Navigate(showAndSearchTours);
            MakeButtonsTransparent();
            Button1Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff6700"));
        }
        private void Execute_NavigateToMyReservationsPageCommand()
        {
            TourReservations tourReservations = new TourReservations(_user, NavService);
            NavService.Navigate(tourReservations);
            MakeButtonsTransparent();
            Button2Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff6700"));
        }
        private void Execute_NavigateToTourRequestsPageCommand()
        {
            MyTourRequests myTourRequests = new MyTourRequests(_user, NavService);
            NavService.Navigate(myTourRequests);
            MakeButtonsTransparent();
            Button3Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff6700"));
        }
        private void Execute_NavigateToMyVouchersPageCommand()
        {
            VouchersDisplay vouchersDisplay = new VouchersDisplay(_user);
            NavService.Navigate(vouchersDisplay);
            MakeButtonsTransparent();
            Button4Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff6700"));

        }
        private void Execute_NavigateToMyNotificationsPageCommand()
        {
            Guest2NotificationView guest2NotificationView = new Guest2NotificationView(_user);
            NavService.Navigate(guest2NotificationView);
            MakeButtonsTransparent();
            Button5Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff6700"));
        }
        public void Execute_NavigateLogOutCommand()
        {
            _user = new Guest2(0, "<null>", "<null>", DateTime.Now);
            SignInView window = new();
            window.Show();
            Guest2HomeView homeWindow = Window.GetWindow(Guest2HomeView) as Guest2HomeView ?? new(_user);
            homeWindow.Close();
        }
        private bool CanExecute_Command()
        {
            return true;
        }
        private void MakeButtonsTransparent()
        {
            Button1Color = new SolidColorBrush(Colors.Transparent);
            Button2Color = new SolidColorBrush(Colors.Transparent);
            Button3Color = new SolidColorBrush(Colors.Transparent);
            Button4Color = new SolidColorBrush(Colors.Transparent);
            Button5Color = new SolidColorBrush(Colors.Transparent);
        }

        private void CheckTourPresenceNotifications()
        {
            if (_service.GetAllUnreadByUser(_user).FindAll(x => x.Title == "Potvrda prisustva").Count() != 0)
            {
                Button5Color = new SolidColorBrush(Colors.Red);
            }
        }

        #endregion
    }
}
