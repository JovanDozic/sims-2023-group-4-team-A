using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SIMSProject.Repositories
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly NotificationFileHandler _fileHandler;
        private readonly IUserRepo _userRepo;
        private List<Notification> _notifications;

        public NotificationRepo(IUserRepo userRepo)
        {
            _fileHandler = new();
            _notifications = new();
            _userRepo = userRepo;

            Load();
            MarkExpiredAsRead();
        }

        public void Load()
        {
            _notifications = _fileHandler.Load();
            MapUsers();
        }

        public Notification GetById(int notificationId)
        {
            return _notifications.FirstOrDefault(x => x.Id == notificationId);
        }

        public List<Notification> GetAllByUser(User user)
        {
            return GetAll().FindAll(x => x.User.Id == user.Id && x.User.Role == user.Role);
        }

        public List<Notification> GetAllUnreadByUser(User user)
        {
            return GetAllByUser(user).FindAll(x => x.IsRead == false);
        }

        public List<Notification> GetAll()
        {
            return _notifications;
        }

        public int NextId()
        {
            return _notifications.Count > 0 ? _notifications.Max(x => x.Id) + 1 : 1;
        }

        public Notification Save(Notification notification)
        {
            notification.Id = NextId();
            _notifications.Add(notification);
            _fileHandler.Save(_notifications);
            return notification;
        }

        public void SaveAll(List<Notification> notifications)
        {
            _fileHandler.Save(notifications);
            _notifications = notifications;
        }

        public void Update(Notification notification)
        {
            Notification notificationToUpdate = GetById(notification.Id) ?? throw new Exception("Updating notification failed!");
            int index = _notifications.IndexOf(notificationToUpdate);
            _notifications[index] = notification;
            _fileHandler.Save(_notifications);
        }

        private void MapUsers()
        {
            foreach(var notification in _notifications)
            {
                notification.User = _userRepo.GetByIdAndRole(notification.User.Id, notification.User.Role);
            }
        }

        private void MarkExpiredAsRead()
        {
            _notifications.ForEach(n => n.IsRead |= n.ExpirationDate?.Date < DateTime.Now.Date);
            SaveAll(_notifications);
        }
    }
}
