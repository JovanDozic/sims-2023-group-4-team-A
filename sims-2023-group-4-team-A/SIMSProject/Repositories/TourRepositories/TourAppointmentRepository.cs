using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Model.UserModel;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourAppointmentRepository
    {
        private readonly TourAppointmentFileHandler _fileHandler;
        private List<TourAppointment> _tourAppointments;

        public TourAppointmentRepository()
        {
            _fileHandler = new();
            _tourAppointments = _fileHandler.Load();
            AssociateAppointments();

        }

        public int NextId() { return _tourAppointments.Max(x => x.Id) + 1; }
        public List<TourAppointment> GetAll() { return _tourAppointments; }
        public TourAppointment Get(int id)
        {
            return _tourAppointments.Find(x => x.Id == id);
        }

        public TourAppointment Save(TourAppointment appointment, Tour tour)
        {
            appointment.Id = NextId();
            SaveTour(appointment, tour);
            _tourAppointments.Add(appointment);
            _fileHandler.Save(_tourAppointments);
            return appointment;
        }

        private static void SaveTour(TourAppointment appointment, Tour tour)
        {
            appointment.Tour = tour;
            appointment.TourId = tour.Id;
        }

        public void SaveAll(List<TourAppointment> appointments)
        {
            _fileHandler.Save(appointments);
            _tourAppointments = appointments;
        }


        private void AssociateAppointments()
        {
            GuestFileHandler guestFileHandler = new();
            List<Guest> guests = guestFileHandler.Load();
            TourGuestFileHandler tourGuestFileHandler = new();
            List<TourGuest> tourGuests = tourGuestFileHandler.Load();
            KeyPointFileHandler keyPointFileHandler = new();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();
            TourFileHandler tourFileHandler = new();
            List<Tour> tours = tourFileHandler.Load();


            foreach (TourAppointment appointment in _tourAppointments)
            {
                AssociateTour(appointment, tours);
                AssociateCurrenKeyPoint(appointment, keyPoints);
                AssociateGuests(appointment, guests, tourGuests);
            }
        }

        private static void AssociateGuests(TourAppointment appointment, List<Guest> guests, List<TourGuest> tourGuests)
        {
            List<TourGuest> pairs = tourGuests.FindAll(x => x.AppointmentId == appointment.Id);
            foreach (var pair in pairs)
            {
                Guest? matchingGuest = guests.Find(x => x.Id == pair.GuestId) ?? throw new SystemException("Error!No matching guest!");
                appointment.Guests.Add(matchingGuest);
            }
        }

        private static void AssociateCurrenKeyPoint(TourAppointment appointment, List<KeyPoint> keyPoints)
        {
            if (appointment.CurrentKeyPointId == -1)
                return;

            appointment.CurrentKeyPoint = keyPoints.Find(x => x.Id == appointment.CurrentKeyPointId) ?? throw new SystemException("Error!No matching key point!");
        }

        private static void AssociateTour(TourAppointment appointment, List<Tour> tours)
        {
            appointment.Tour = tours.Find(x => x.Id == appointment.TourId) ?? throw new SystemException("Error!No matching tour!");
        }

        public List<TourAppointment> GetAllByTourId(int tourId)
        {
            return GetAll().FindAll(x => x.TourId == tourId && DateTime.Compare(x.Date, DateTime.Now) > 0);
        }
        public List<TourAppointment> FindTodaysAppointments(int TourId)
        {
            return _tourAppointments.FindAll(x => x.TourId == TourId && x.Date.Date == DateTime.Now.Date);
        }
    }
}
