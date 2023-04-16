using SIMSProject.FileHandler;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.Domain.Models;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Domain.RepositoryInterfaces;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourRepo: ITourRepo
    {
        private readonly TourFileHandler _fileHandler;
        private List<Tour> _tours;
        private readonly ITourKeyPointRepo _tourKeyPointRepo;
        private readonly IKeyPointRepo _keyPointRepo;
        private readonly ILocationRepo _locationRepo;
        //private readonly ITourAppointmentRepo _tourAppointmentRepo;

        public TourRepo(ITourKeyPointRepo tourKeyPointRepo, IKeyPointRepo keyPointRepo, ILocationRepo locationRepo /*ITourAppointmentRepo tourAppointmentRepo*/)
        {
            _fileHandler = new TourFileHandler();
            _tours = _fileHandler.Load();
            _keyPointRepo = keyPointRepo;
            _locationRepo = locationRepo;
            _tourKeyPointRepo = tourKeyPointRepo;
            //_tourAppointmentRepo = tourAppointmentRepo;

            MapTours();
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
            _tours.Add(tour);
            _fileHandler.Save(_tours);
            return tour;
        }
        public void SaveAll(List<Tour> tours)
        {
            _fileHandler.Save(tours);
            _tours = tours;
        }

        private void MapTours()
        {
            foreach (var tour in _tours)
            {
                MapLocation(tour);
                MapKeyPoints(tour);
                //MapAppointemnts(tour);
            }
        }

        //private void MapAppointemnts(Tour tour)
        //{
        //    foreach(var appointment in _tourAppointmentRepo.GetAll())
        //    {
        //        if(appointment.Tour.Id == tour.Id)
        //            tour.Appointments.Add(appointment);
        //    }
        //}


        private  void MapLocation(Tour tour)
        {
            tour.Location = _locationRepo.GetAll().Find(x => x.Id == tour.Location.Id) ?? throw new SystemException("Error!No matching location!");
        }

        private  void MapKeyPoints(Tour tour)
        {
            List<TourKeyPoint> pairs = _tourKeyPointRepo.GetAll().FindAll(x => x.TourId == tour.Id);
            foreach (var pair in pairs)
            {
                KeyPoint? matchingKeyPoint = _keyPointRepo.GetAll().Find(x => x.Id == pair.KeyPointId) ?? throw new SystemException("Error!No matching key point!");
                tour.KeyPoints.Add(matchingKeyPoint);
            }
        }

        public List<Tour> SearchLocations(string locationId)
        {
            return _tours.Where(tour => tour.Location.Id.Equals(locationId)).ToList();
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
            return _tours.FindAll(x => x.Appointments.Any(x => (DateTime.Compare(x.Date.Date, DateTime.Now.Date) == 0 || x.TourStatus == Status.ACTIVE)));
        }
        public List<Tour> GetToursWithSameLocation(Tour selectedTour)
        {
            return _tours.FindAll(x => x.Location.Id == selectedTour.Location.Id && x.Id != selectedTour.Id);
        }


    }
}
