using SIMSProject.Application.DTOs;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ITourRepo
    {
        public int NextId();
        public List<Tour> GetAll();
        public List<Tour> GetAll(int GuideId, Language language);
        public Tour GetById(int id);
        public Tour Save(Tour tour);
        public void SortBySuperGuide(int GuideId);
        public void SaveAll(List<Tour> tours);
        public List<Tour> GetToursWithSameLocation(Tour selectedTour);
        public List<TourRatingDTO> SearchRatingsByTourName(List<TourRatingDTO> ratings, string tourName);
        public int GetCurrentKeyPointIndex(TourAppointment appointment, Tour currentTour);
        public void SearchTours(string location, int searchDuration, int searchMaxGuests, string language, ObservableCollection<Tour> tours);
        public KeyPoint GetLastKeyPoint(TourAppointment appointment);
    }
}
