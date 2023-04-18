using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces
{
    public interface INotificationRepo
    {
        public void Load();
        public Notification GetById(int notificationId);
        public List<Notification> GetAllByUser(User user);
        public List<Notification> GetAllUnreadByUser(User user);
        public List<Notification> GetAll();
        public int NextId();
        public Notification Save(Notification notification);
        public void SaveAll(List<Notification> notifications);
        public void Update(Notification notification);
    }
}
