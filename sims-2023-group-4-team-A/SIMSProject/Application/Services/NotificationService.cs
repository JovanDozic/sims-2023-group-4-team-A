using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces;
using System.Collections.Generic;

namespace SIMSProject.Application.Services
{
    public class NotificationService
    {
        private readonly INotificationRepo _repo;

        public NotificationService(INotificationRepo repo)
        {
            _repo = repo;
        }

        public List<Notification> GetAllByUser(User user)
        {
            return _repo.GetAllByUser(user);
        }

        public List<Notification> GetAllUnreadByUser(User user)
        {
            return _repo.GetAllUnreadByUser(user);
        }

        public void CreateNotification(Notification notification)
        {
            notification.IsRead = false;
            _repo.Save(notification);
        }

        public void MarkAsRead(Notification notification)
        {
            _repo.Load();
            var readNotification = _repo.GetById(notification.Id);
            readNotification.IsRead = true;
            _repo.Update(readNotification);
        }

        public bool AnyUnreadNotifications(User user)
        {
            return _repo.GetAllUnreadByUser(user).Count > 0;
        }


    }
}
