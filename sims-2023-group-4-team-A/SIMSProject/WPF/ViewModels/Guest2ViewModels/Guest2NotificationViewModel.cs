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

        public Guest2NotificationViewModel(User user)
        {
            _user = user;
            _service = Injector.GetService<NotificationService>();

            LoadNotifications();
        }

        public void LoadNotifications()
        {
            Notifications = new(_service.GetAllUnreadByUser(_user));
        }

        public bool CanBeMarkedAsRead()
        {
            if (SelectedNotification == null) return false;

            if (SelectedNotification.ExpirationDate == null) return true;
            return false;
        }

        public void MarkAsRead()
        {
            if (SelectedNotification == null || !CanBeMarkedAsRead()) return;
            _service.MarkAsRead(SelectedNotification);

            LoadNotifications();
        }
    }
}
