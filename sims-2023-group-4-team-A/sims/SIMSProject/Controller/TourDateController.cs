using System.Collections.Generic;
using SIMSProject.Model.DAO;
using SIMSProject.Model;
using System;

namespace SIMSProject.Controller
{
    public class TourDateController
    {
        private TourDateDAO _tourDates;
        public TourDate TourDate;

        public TourDateController()
        {
            _tourDates = new();
            TourDate = new();
        }

        public TourDate GetById(int id)
        {
            return _tourDates.Get(id);
        }

        public List<TourDate> GetAll()
        {
            return _tourDates.GetAll();
        }

        public List<TourDate> GetAllByTourId(int tourId)
        {
            return _tourDates.GetAllByTourId(tourId);
        }

        public void SaveAll(List<TourDate> tourDates)
        {
            _tourDates.SaveAll(tourDates);
        }

        public TourDate Create(TourDate tourDate)
        {
            return _tourDates.Save(tourDate);
        }

        public List<TourDate> FindTodaysByTour(int TourId)
        {
            return _tourDates.FindTodaysDates(TourId);
        }

        public void AdvanceToNext(int dateId, KeyPoint SelectedKeyPoint)
        {
            _tourDates.AdvanceToNextKeyPoint(dateId, SelectedKeyPoint);
        }

        public void StartTourLiveTracking(TourDate tourDate)
        {
            _tourDates.StartLiveTracking(tourDate);
        }

        public void StopTourLiveTracking(int dateId) 
        {
            _tourDates.EndTourDate(dateId);
        }

        public TourDate InitializeTour(TourDate tourDate, Tour tour)
        {
            return _tourDates.InitializeTour(tourDate, tour);
        }
        public void UpdateAvailableSpots(TourDate tourDate)
        {
            _tourDates.UpdateAvailableSpots(tourDate);
        }
    }
}
