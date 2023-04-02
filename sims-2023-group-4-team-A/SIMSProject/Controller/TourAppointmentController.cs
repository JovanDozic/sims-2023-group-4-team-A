﻿using System.Collections.Generic;
using SIMSProject.Model.DAO;
using SIMSProject.Model;
using System;

namespace SIMSProject.Controller
{
    public class TourAppointmentController
    {
        private TourAppointmentDAO _tourAppointments;
        public TourAppointment TourDate;

        public TourAppointmentController()
        {
            _tourAppointments = new();
            TourDate = new();
        }

        public TourAppointment GetById(int id)
        {
            return _tourAppointments.Get(id);
        }

        public List<TourAppointment> GetAll()
        {
            return _tourAppointments.GetAll();
        }

        public List<TourAppointment> GetAllByTourId(int tourId)
        {
            return _tourAppointments.GetAllByTourId(tourId);
        }

        public void SaveAll(List<TourAppointment> tourAppointments)
        {
            _tourAppointments.SaveAll(tourAppointments);
        }

        public TourAppointment Create(TourAppointment appointment)
        {
            return _tourAppointments.Save(appointment);
        }

        public List<TourAppointment> FindTodaysByTour(int tourId)
        {
            return _tourAppointments.FindTodaysAppointments(tourId);
        }

        public void AdvanceToNext(int appointmentId, KeyPoint selectedKeyPoint)
        {
            _tourAppointments.GoToNextKeyPoint(appointmentId, selectedKeyPoint);
        }

        public void StartTourLiveTracking(TourAppointment appointment)
        {
            _tourAppointments.StartLiveTracking(appointment);
        }

        public void StopTourLiveTracking(int appointmentId) 
        {
            _tourAppointments.StopLiveTracking(appointmentId);
        }

        public TourAppointment InitializeTour(TourAppointment appointment, Tour tour)
        {
            return _tourAppointments.InitializeTour(appointment, tour);
        }
        public void UpdateAvailableSpots(TourAppointment appointment)
        {
            _tourAppointments.UpdateAvailableSpots(appointment);
        }
    }
}