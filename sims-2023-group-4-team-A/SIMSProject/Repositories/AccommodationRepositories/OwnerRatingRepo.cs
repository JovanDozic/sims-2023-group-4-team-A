﻿using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.FileHandlers.AccommodationFileHandlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    public class OwnerRatingRepo : IOwnerRatingRepo
    {
        private readonly OwnerRatingFileHandler _fileHandler;
        private readonly IAccommodationReservationRepo _reservationRepo;
        private List<OwnerRating> _ratings;

        public OwnerRatingRepo(IAccommodationReservationRepo reservationRepo)
        {
            _fileHandler = new();
            _ratings = new();
            _reservationRepo = reservationRepo;

            Load();
        }

        public void Load()
        {
            _ratings = _fileHandler.Load();
            MapAccommodationReservations();
        }

        public List<OwnerRating> GetAll()
        {
            return _ratings;
        }

        public List<OwnerRating> GetAllByOwnerId(int ownerId)
        {
            return _ratings.FindAll(x => x.Reservation.Accommodation.Owner.Id == ownerId);
        }

        public OwnerRating GetById(int ratingId)
        {
            return _ratings.Find(x => x.Id == ratingId);
        }

        public double GetOverallByOwnerId(int ownerId)
        {
            var ratings = GetAllByOwnerId(ownerId);
            return ratings.Count > 0 ? ratings.Average(x => x.Overall) : 0;
        }

        public int NextId()
        {
            return _ratings.Count > 0 ? _ratings.Max(x => x.Id) : 1;
        }

        public OwnerRating Save(OwnerRating rating)
        {
            rating.Id = NextId();
            _ratings.Add(rating);
            _fileHandler.Save(_ratings);
            return rating;
        }

        public void SaveAll(List<OwnerRating> ratings)
        {
            _fileHandler.Save(ratings);
            _ratings = ratings;
        }

        private void MapAccommodationReservations()
        {
            foreach (var rating in _ratings)
            {
                rating.Reservation = _reservationRepo.GetById(rating.Reservation.Id);
            }
        }

        public OwnerRating GetByReservationId(int reservationId)
        {
            return _ratings.Find(x => x.Reservation.Id == reservationId);
        }
    }
}