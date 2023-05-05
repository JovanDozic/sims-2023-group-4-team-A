using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.FileHandlers.AccommodationFileHandlers;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    public class GuestRatingRepo : IGuestRatingRepo
    {
        private readonly GuestRatingFileHandler _fileHandler;
        private readonly IAccommodationReservationRepo _reservationRepo;
        private List<GuestRating> _ratings;

        public GuestRatingRepo(IAccommodationReservationRepo reservationRepo)
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

        public List<GuestRating> GetAll()
        {
            return _ratings;
        }

        public GuestRating GetById(int ratingId)
        {
            return _ratings.Find(x => x.Id == ratingId);
        }

        public List<GuestRating> GetAllByGuestId(int guestId)
        {
            return _ratings.FindAll(x => x.Reservation.Guest.Id == guestId);
        }

        public double GetOverallByGuestId(int guestId)
        {
            var ratings = GetAllByGuestId(guestId);
            return ratings.Count > 0 ? ratings.Average(x => x.Overall) : 0;
        }

        public int NextId()
        {
            return _ratings.Count > 0 ? _ratings.Max(x => x.Id) + 1 : 1;
        }

        public GuestRating Save(GuestRating rating)
        {
            rating.Id = NextId();
            _ratings.Add(rating);
            _fileHandler.Save(_ratings);
            return rating;
        }

        public void SaveAll(List<GuestRating> ratings)
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

        public GuestRating GetByReservationId(int reservationId)
        {
            return _ratings.Find(x => x.Reservation.Id == reservationId);
        }
    }
}
