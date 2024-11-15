﻿using SIMSProject.Application.DTOs;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.FileHandlers.TourFileHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.TourRepositories
{
    internal class GuideRatingRepo : IGuideRatingRepo
    {
        public readonly GuideRatingFileHandler _fileHandler;
        private readonly ITourReservationRepo _reservationRepo;
        private List<GuideRating> _guideRatings;

        public GuideRatingRepo(ITourReservationRepo reservationRepo)
        {
            _fileHandler = new GuideRatingFileHandler();
            _guideRatings = _fileHandler.Load();
            _reservationRepo = reservationRepo;
            MapTourReservations();
        }

        public List<GuideRating> GetAll()
        {
            return _guideRatings;
        }

        public List<GuideRating> GetAll(int appointmentId)
        {
            return _guideRatings.Where(x => x.TourReservation.TourAppointment.Id == appointmentId && !x.Reported).ToList();
        }

        public List<GuideRating> GetAllByGuideId(int guideId)
        {
            return _guideRatings.FindAll(x=>x.TourReservation.TourAppointment.Tour.Guide.Id == guideId);
        }

        public GuideRating GetById(int id)
        {
            return _guideRatings.Find(x => x.Id ==  id) ?? throw new ArgumentException("Rating could not be found");
        }

        public double GetOverallByGuideId(int guideId)
        {
            var ratings = GetAllByGuideId(guideId);
            return ratings.Count > 0 ? ratings.Average(x => x.Overall) : 0;
        }

        public int NextId()
        {
            return _guideRatings.Count > 0 ? _guideRatings.Max(x => x.Id) + 1 : 1;
        }

        public GuideRating Save(GuideRating guideRating)
        {
            guideRating.Id = NextId();
            _guideRatings.Add(guideRating);
            _fileHandler.Save(_guideRatings);
            return guideRating;
        }

        public void SaveAll(List<GuideRating> guideRatings)
        {
            _fileHandler.Save(guideRatings);
            _guideRatings = guideRatings;
        }

        private void MapTourReservations()
        {
            foreach (var guideRating in _guideRatings)
            {
                guideRating.TourReservation = _reservationRepo.GetById(guideRating.TourReservation.Id);
            }
        }
    }
}
