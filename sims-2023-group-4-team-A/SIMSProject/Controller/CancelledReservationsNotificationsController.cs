using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model;
using SIMSProject.Model.DAO;


namespace SIMSProject.Controller
{
    public class CancelledReservationsNotificationsController
    {

        private readonly CancelledReservationsNotificationsDAO _cancelledReservationsNotificationsDAO;
        public CancelledReservationsNotifications CancelledReservationsNotifications;

        public CancelledReservationsNotificationsController()
        {
            _cancelledReservationsNotificationsDAO = new CancelledReservationsNotificationsDAO();
            CancelledReservationsNotifications = new CancelledReservationsNotifications();
        }

        public List<CancelledReservationsNotifications> GetAll()
        {
            return _cancelledReservationsNotificationsDAO.GetAll();
        }

        public void SaveAll(List<CancelledReservationsNotifications> reservation)
        {
            _cancelledReservationsNotificationsDAO.SaveAll(reservation);
        }

        public CancelledReservationsNotifications Create(CancelledReservationsNotifications notification)
        {
            return _cancelledReservationsNotificationsDAO.Save(notification);
        }

        public void UpdateExisting(CancelledReservationsNotifications updatedNotifications)
        {
            var notifications = _cancelledReservationsNotificationsDAO.GetAll();
            var notification = notifications.Find(x => x.Id == updatedNotifications.Id);
            if (notification != null)
            {
                var index = notifications.IndexOf(notification);
                notifications.Remove(notification);
                notifications.Insert(index, updatedNotifications);
            }
            else
            {
                Create(updatedNotifications);
            }

            SaveAll(notifications);
        }

        public void Update(CancelledReservationsNotifications updatedNotifications)
        {
            var notifications = _cancelledReservationsNotificationsDAO.GetAll();
            int index = notifications.FindIndex(s => s.Id == updatedNotifications.Id);
            if (index != -1)
            {
                notifications[index] = updatedNotifications;
            }
            SaveAll(notifications);

        }

    }
}
