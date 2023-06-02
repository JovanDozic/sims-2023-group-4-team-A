using SIMSProject.Application.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private User _user;
        private NotificationService _notificationService;

        public ObservableCollection<Notification> Notifications { get; set; }
        public Notification Notification { get; set; } = new();

        public NotificationViewModel(User user)
        {
            _user = user;

            _notificationService = Injector.GetService<NotificationService>();

            Notifications = new(_notificationService.GetAllUnreadByUser(_user));
        }

        public void MarkNotificationAsRead()
        {
            _notificationService.MarkAsRead(Notification);
            Notifications.Remove(Notification);
        }
    }
}
