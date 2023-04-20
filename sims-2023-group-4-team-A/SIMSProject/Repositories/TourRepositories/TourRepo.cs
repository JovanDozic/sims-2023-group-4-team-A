using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.Models;
using SIMSProject.FileHandlers.UserFileHandler;
using SIMSProject.Domain.RepositoryInterfaces;
using System.Windows;
using System.Collections.ObjectModel;
using SIMSProject.FileHandlers.TourFileHandlers;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourRepo: ITourRepo
    {
        private readonly TourFileHandler _fileHandler;
        
        private List<Tour> _tours;
        private readonly ITourKeyPointRepo _tourKeyPointRepo;
        private readonly IKeyPointRepo _keyPointRepo;
        private readonly ILocationRepo _locationRepo;

        public TourRepo(ITourKeyPointRepo tourKeyPointRepo, IKeyPointRepo keyPointRepo, ILocationRepo locationRepo)
        {
            _fileHandler = new TourFileHandler();
            _tours = _fileHandler.Load();
            _keyPointRepo = keyPointRepo;
            _locationRepo = locationRepo;
            _tourKeyPointRepo = tourKeyPointRepo;
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
            }
        }

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

        public List<Tour> GetToursWithSameLocation(Tour selectedTour)
        {
            return _tours.FindAll(x => x.Location.Id == selectedTour.Location.Id && x.Id != selectedTour.Id);
        }

        public void SearchTours(string locationAndLanguage, int searchDuration, int searchMaxGuests, ObservableCollection<Tour> tours)
        {
            tours.Clear();
            foreach (var tour in new ObservableCollection<Tour>
                (GetAll()))
                tours.Add(tour);

            if (locationAndLanguage == "Lokacija jezik") locationAndLanguage = string.Empty;
            string[] searchValues = locationAndLanguage.Split(" ");

            List<Tour> searchResults = tours.ToList();

            // Removing all by location and language
            foreach (string value in searchValues)
                searchResults.RemoveAll(x => !x.ToStringSearch().ToLower().Contains(value.ToLower()));

            // Removing by numbers
            if (searchDuration > 0) searchResults.RemoveAll(x => x.Duration != searchDuration);
            if (searchMaxGuests > 0) searchResults.RemoveAll(x => x.MaxGuestNumber < searchMaxGuests);

            tours.Clear();
            foreach (var searchResult in searchResults)
                tours.Add(searchResult);
        }
    }
}
