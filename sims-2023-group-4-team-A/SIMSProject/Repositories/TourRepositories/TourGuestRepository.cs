using SIMSProject.Domain.TourModels;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.FileHandler;
using SIMSProject.Model.UserModel;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourGuestRepository
    {
        private readonly TourGuestFileHandler _fileHandler;
        private List<TourGuest> _tourGuests;

        public TourGuestRepository()
        {
            _fileHandler = new TourGuestFileHandler();
            _tourGuests = _fileHandler.Load();

            AssociateTourGuests();
        }

        public List<TourGuest> GetAll()
        {
            return _tourGuests;
        }

        public TourGuest Save(TourGuest tourGuest)
        {
            _tourGuests.Add(tourGuest);
            _fileHandler.Save(_tourGuests);
            return tourGuest;
        }

        public void SaveAll(List<TourGuest> tourGuests)
        {
            _fileHandler.Save(tourGuests);
            _tourGuests = tourGuests;
        }

        private void AssociateTourGuests()
        {
            KeyPointFileHandler keyPointFileHandler = new();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();
            TourAppointmentFileHandler dateHandler = new();
            List<TourAppointment> appointments = dateHandler.Load();
            GuestFileHandler guestFileHandler = new();
            List<Guest> tourGuests = guestFileHandler.Load();

            foreach (TourGuest tourGuest in _tourGuests)
            {
                AssociateAppointment(tourGuest, appointments);
                AssociateJoinedKeyPoint(tourGuest, keyPoints);
                AssociateGuest(tourGuests, tourGuest);

            }
        }

        private static void AssociateGuest(List<Guest> tourGuests, TourGuest tourGuest)
        {
            tourGuest.Guest = tourGuests.Find(x => x.Id == tourGuest.GuestId) ?? throw new System.Exception("Error!No matching guest!");
        }

        private static void AssociateJoinedKeyPoint(TourGuest tourGuest, List<KeyPoint> keyPoints)
        {
            if (tourGuest.JoinedKeyPointId == -1)
                return;

            tourGuest.JoinedKeyPoint = keyPoints.Find(x => x.Id == tourGuest.JoinedKeyPointId) ?? throw new System.Exception("Error!No matching keyPoint!");
        }

        private static void AssociateAppointment(TourGuest tourGuest, List<TourAppointment> appointments)
        {
            tourGuest.Appointment = appointments.Find(x => x.Id == tourGuest.AppointmentId) ?? throw new System.Exception("Error!No matching appointment!");
        }
        public List<TourGuest> GetGuests(int tourAppointmentId)
        {
            return _tourGuests.FindAll(x => x.AppointmentId == tourAppointmentId);
        }
    }
}
