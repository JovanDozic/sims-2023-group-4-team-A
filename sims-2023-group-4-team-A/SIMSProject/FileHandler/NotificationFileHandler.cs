using SIMSProject.Domain.Models;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandler
{
    public class NotificationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/notifications.csv";
        private readonly Serializer<Notification> _serializer;

        public NotificationFileHandler()
        {
            _serializer = new Serializer<Notification>();
        }

        public List<Notification> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Notification> notifications)
        {
            _serializer.ToCSV(FilePath, notifications);
        }
    }
}
