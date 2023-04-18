using Microsoft.Win32.SafeHandles;
using SIMSProject.Application.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerNotificationViewModel : ViewModelBase
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

        public OwnerNotificationViewModel(User user)
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
