using SIMSProject.Application.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2NotificationViewModel : ViewModelBase
    {
        private User _user;
        private readonly NotificationService _service;
        private Notification _selectedNotification = new();
        private ObservableCollection<Notification> _notifications = new();

        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set
            {
                if (_notifications == value) return;
                _notifications = value;
                OnPropertyChanged();
            }
        }
        public Notification SelectedNotification
        {
            get => _selectedNotification;
            set
            {
                if (_selectedNotification == value) return;
                _selectedNotification = value;
                OnPropertyChanged();
            }
        }
        private Visibility _yesButtonVisibility;
        public Visibility YesButtonVisibility
        {
            get => _yesButtonVisibility;
            set
            {
                if(_yesButtonVisibility == value) return;
                _yesButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _noButtonVisibility;
        public Visibility NoButtonVisibility
        {
            get => _noButtonVisibility;
            set
            {
                if (_noButtonVisibility == value) return;
                _noButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        public Guest2NotificationViewModel(User user)
        {
            _user = user;
            _service = Injector.GetService<NotificationService>();
            Notifications = new(_service.GetAllUnreadByUser(_user));
            SetButtonsState();
        }

        public void SetButtonsState()
        {
            if (SelectedNotification?.Title == "Potvrda prisustva")
            {
                YesButtonVisibility = Visibility.Visible;
                NoButtonVisibility = Visibility.Visible;
                return;
            }
            YesButtonVisibility = Visibility.Hidden;
            NoButtonVisibility = Visibility.Hidden;
        }

    }
}
