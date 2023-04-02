using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Serializer;
using SIMSProject.Model;

namespace SIMSProject.FileHandler
{
    public class CancelledReservationsNotificationsFileHandler
    {
        private const string FilePath = "../../../Resources/Data/CancelledReservationsNotifications.csv";
        private readonly Serializer<CancelledReservationsNotifications> _serializer;

        public CancelledReservationsNotificationsFileHandler()
        {
            _serializer = new Serializer<CancelledReservationsNotifications>();
        }

        public List<CancelledReservationsNotifications> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<CancelledReservationsNotifications> CancelledReservationsNotifications)
        {
            _serializer.ToCSV(FilePath, CancelledReservationsNotifications);
        }
    }
}
