using SIMSProject.FileHandler;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.Domain.Models;
using SIMSProject.FileHandler.UserFileHandler;
using System.Windows;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourRepo: ITourRepo
    {
        private readonly TourFileHandler _fileHandler;
        
        private List<Tour> _tours;

        public TourRepo()
        {
            _fileHandler = new TourFileHandler();
            _tours = _fileHandler.Load();
            AssociateTours();
        }

        public int NextId()
        {
                return _tours.Count > 0 ?_tours.Max(x => x.Id) + 1 : 1;
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public Tour GetById(int id)
        {
            return _tours.Find(x => x.Id == id);
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            SaveAppointments(tour);
            _tours.Add(tour);
            _fileHandler.Save(_tours);
            return tour;
        }
        private static void SaveAppointments(Tour tour)
        {
            foreach (var appointment in tour.Appointments)
            {
                appointment.TourId = tour.Id;
            }
        }

        public void SaveAll(List<Tour> tours)
        {
            _fileHandler.Save(tours);
            _tours = tours;
        }

        private void AssociateTours()
        {
            GuideFileHandler guideFileHandler = new();
            List<Guide> guides = guideFileHandler.Load();
            LocationFileHandler tourLocationFileHandler = new();
            List<Location> tourLocations = tourLocationFileHandler.Load();
            TourAppointmentFileHandler tourAppointmentFileHandler = new();
            List<TourAppointment> tourDates = tourAppointmentFileHandler.Load();
            TourKeyPointFileHandler tourKeyPointFileHandler = new();
            KeyPointFileHandler keyPointFileHandler = new();
            List<TourKeyPoint> tourKeyPoints = tourKeyPointFileHandler.Load();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();

            foreach (var tour in _tours)
            {
                AssociateGuide(tour, guides);
                AssociateLocation(tour, tourLocations);
                AssociateAppointments(tour, tourDates);
                AssociateKeyPoints(tour, tourKeyPoints, keyPoints);
            }
        }

        private static void AssociateGuide(Tour tour, List<Guide> guides)
        {
            tour.Guide = guides.Find(x => x.Id == tour.GuideId) ?? throw new SystemException("Error! No matching guide!");
        }

        private static void AssociateLocation(Tour tour, List<Location> tourLocations)
        {
            tour.Location = tourLocations.Find(x => x.Id == tour.LocationId) ?? throw new SystemException("Error! No matching location!");
        }

        private static void AssociateAppointments(Tour tour, List<TourAppointment> tourDates)
        {
            tour.Appointments.AddRange(tourDates.FindAll(x => x.TourId == tour.Id));
        }

        private static void AssociateKeyPoints(Tour tour, List<TourKeyPoint> tourKeyPoints, List<KeyPoint> keyPoints)
        {
            List<TourKeyPoint> pairs = tourKeyPoints.FindAll(x => x.TourId == tour.Id);
            foreach (var pair in pairs)
            {
                KeyPoint? matchingKeyPoint = keyPoints.Find(x => x.Id == pair.KeyPointId) ?? throw new SystemException("Error!No matching key point!");
                tour.KeyPoints.Add(matchingKeyPoint);
            }
        }

        public List<Tour> SearchLocations(string locationId)
        {
            return _tours.Where(tour => tour.LocationId.Equals(locationId)).ToList();
        }

        public List<Tour> SearchDurations(string duration)
        {
            return _tours.Where(tour => tour.Duration.Equals(duration)).ToList();
        }

        public List<Tour> SearchLanguages(string language)
        {
            return _tours.Where(tour => tour.TourLanguage.Equals(language)).ToList();
        }

        public List<Tour> SearchMaxGuests(string maxGuests)
        {
            return _tours.Where(tour => tour.MaxGuestNumber.Equals(maxGuests)).ToList();
        }

        public List<Tour> FindTodaysTours()
        {
            return _tours.FindAll(x => x.Appointments.Any(x => x.Date.Date == DateTime.Today.Date));
        }
        public List<Tour> GetToursWithSameLocation(Tour selectedTour)
        {
            return _tours.FindAll(x => x.LocationId == selectedTour.LocationId && x.Id != selectedTour.Id);
        }


    }
}
