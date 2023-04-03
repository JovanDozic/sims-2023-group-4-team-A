using System.Collections.Generic;
using System.Linq;
using SIMSProject.Observer;
using SIMSProject.FileHandler;
using SIMSProject.Controller;
using System.Windows.Navigation;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Model.UserModel;
using Microsoft.VisualBasic;
using System;

namespace SIMSProject.Model.DAO
{
    public class TourAppointmentDAO : ISubject
    {
        private List<IObserver> _observers;
        private readonly TourAppointmentFileHandler _fileHandler;
        private List<TourAppointment> _tourAppointments;

        public TourAppointmentDAO()
        {
            _fileHandler = new();
            _tourAppointments = _fileHandler.Load();
            _observers = new();

            AssociateAppointments();

        }

        public int NextId() { return _tourAppointments.Max(x => x.Id) + 1; }
        public List<TourAppointment> GetAll() { return _tourAppointments; }
        public TourAppointment Get(int id)
        {
            return _tourAppointments.Find(x => x.Id == id);
        }

        public TourAppointment Save(TourAppointment appointment)
        {
            appointment.Id = NextId();
            appointment.TourStatus = "Neaktivna";
            _tourAppointments.Add(appointment);
            _fileHandler.Save(_tourAppointments);
            NotifyObservers();
            return appointment;
        }

        public void SaveAll(List<TourAppointment> appointments)
        {
            _fileHandler.Save(appointments);
            _tourAppointments = appointments;
            NotifyObservers();
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
            List<TourAppointment> appointments = new();
            foreach(TourAppointment appointment in GetAll())
            {
                if(appointment.TourId == tourId)
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        public void GoToNextKeyPoint(int appointmentId, KeyPoint nextKeyPoint)
        {
            TourAppointment? appointment = Get(appointmentId) ?? throw new SystemException("Error!Can't find appointment!");
            appointment.CurrentKeyPoint = nextKeyPoint;
            appointment.CurrentKeyPointId = nextKeyPoint.Id;
            SaveAll(_tourAppointments);
        }
        public void UpdateAvailableSpots(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = _tourAppointments.Find(x => x.Id == appointment.Id) ?? throw new SystemException("Error!Can't find appointment!");
            oldAppointment.AvailableSpots = appointment.AvailableSpots;
            _fileHandler.Save(_tourAppointments);
            NotifyObservers();
        }

        private static bool IsCancelable(TourAppointment appointment)
        { 
            return DateTime.Now.AddHours(-48) > appointment.Date;
        }

        public bool CancelAppointment(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = Get(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");

            if(!IsCancelable(oldAppointment))
            { 
                return false; 
            }
            
            oldAppointment.TourStatus = "Otkazana";
            _fileHandler.Save(_tourAppointments);
            return true;
        }

        public void StartLiveTracking(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = Get(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");

            oldAppointment.TourStatus = "Aktivna";
            oldAppointment.CurrentKeyPoint = appointment.CurrentKeyPoint;
            oldAppointment.CurrentKeyPointId = appointment.CurrentKeyPointId;
            _fileHandler.Save(_tourAppointments);
        }

        public void StopLiveTracking(int appointmentId)
        {
            TourAppointment? toEnd = Get(appointmentId) ?? throw new ArgumentException("Error!Can't find appointment!");
            toEnd.TourStatus = "Završena";
            _fileHandler.Save(_tourAppointments);
            NotifyObservers();
        }

        public TourAppointment InitializeTour(TourAppointment appointment, Tour tour)
        {
            TourAppointment? oldAppointment = _tourAppointments.Find(x => x.Id ==  appointment.Id) ?? throw new SystemException("Error!Can't find appointment!");
            oldAppointment.Tour = tour;
            oldAppointment.TourId = tour.Id;
            oldAppointment.AvailableSpots = tour.MaxGuestNumber;
            _fileHandler.Save(_tourAppointments);
            return oldAppointment;
        }

        public List<TourAppointment> FindTodaysAppointments(int TourId)
        {
            return _tourAppointments.FindAll(x => x.TourId == TourId && x.Date.Date == DateTime.Now.Date);
        }

        public List<TourAppointment> FindByTour(int TourId)
        {
            return _tourAppointments.FindAll(x => x.TourId == TourId);
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }

        
    }
}
