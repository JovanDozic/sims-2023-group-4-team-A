using SIMSProject.Application.DTOs;
using SIMSProject.Application.DTOs.TourDTOs;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class TourService
    {
        private readonly ITourRepo _repo;
        private readonly TourRatingService _tourRatingService;
        private readonly TourKeyPointService _tourKeyPointService;
        
        public TourService(ITourRepo repo)
        {
            _repo = repo;
            _tourRatingService = Injector.GetService<TourRatingService>();
            _tourKeyPointService = Injector.GetService<TourKeyPointService>();
        }
        public List<Tour> GetTours()
        {
            return _repo.GetAll();
        }
        public List<TourRatingDTO> GetRatings()
        {
            List<TourRatingDTO> tourRatings = new();

            foreach(var tour in _repo.GetAll())
            {
                List<TourAppointmentRatingDTO> ratings = _tourRatingService.MapRatingsByTour(tour.Id);
                if (ratings.Count == 0) continue;
                tourRatings.Add(new(tour, ratings));
            }
            return tourRatings;
        }
        public List<DateTime> GetRatedDatesByTour(TourRatingDTO rating)
        {
            return _tourRatingService.GetRatedDatesByTourRating(rating);
        }

        public List<TourRatingDTO> SearchRatings(string tourName)
        {
            return _repo.SearchRatingsByTourName(GetRatings(), tourName);
        }

        public void CreateTour(Tour tour)
        {
            _tourKeyPointService.CreateNewPairs(_repo.Save(tour));
        }

        public KeyPoint GetNextKeyPoint(TourAppointment appointment)
        {
            var currentTour = _repo.GetById(appointment.Tour.Id);
            int currentIndex = _repo.GetCurrentKeyPointIndex(appointment, currentTour);
            var indexOutOfRange = currentIndex < 0 || currentIndex >= currentTour.KeyPoints.Count - 1;
            return indexOutOfRange ? null : currentTour.KeyPoints[currentIndex + 1];
        }

        public KeyPoint GetLastKeyPoint(TourAppointment appointment)
        {
            return _repo.GetLastKeyPoint(appointment);
        }

        public List<Tour> GetToursWithSameLocation(Tour tour)
        {
            return _repo.GetToursWithSameLocation(tour);
        }
        public void SearchTours(string location, int searchDuration, int searchMaxGuests, string language, ObservableCollection<Tour> tours)
        {
            _repo.SearchTours(location, searchDuration, searchMaxGuests, language, tours);
        }

        public void SortBySuperGuide(int GuideId)
        {
            _repo.SortBySuperGuide(GuideId);
        }

        public void FlagSuperGuide(int GuideId, Language language)
        {
            _repo.GetAll(GuideId, language).ForEach(x => { x.IsSuperGuide = true; });
            _repo.SaveAll(_repo.GetAll());
        }
    }
}
