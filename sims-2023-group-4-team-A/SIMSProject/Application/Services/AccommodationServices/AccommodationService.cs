using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Model;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationService
    {
        private readonly IAccommodationRepo _repo;
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly OwnerRatingService _ratingService;

        public AccommodationService(IAccommodationRepo repo)
        {
            _repo = repo;
            _accommodationReservationService = Injector.GetService<AccommodationReservationService>();
            _ratingService = Injector.GetService<OwnerRatingService>();
        }

        public void ReloadAccommodations()
        {
            _repo.Load();
        }

        public List<Accommodation> CalculateRatings(List<Accommodation> accommodations)
        {
            foreach(var accommodation in accommodations)
            {
                accommodation.Rating = _ratingService.CalculateRating(accommodation);
            }
            return accommodations;
        }

        public List<Accommodation> GetAllByOwnerId(int ownerId)
        {
            var accommodations = _repo.GetAllByOwnerId(ownerId);
            return CalculateRatings(accommodations);
        }

        public List<Accommodation> GetAll()
        {
            var accommodations = _repo.GetAll();
            return CalculateRatings(accommodations);
        }

        public void RegisterAccommodation(Accommodation accommodation)
        {
            _repo.Save(accommodation);
        }

        public int CountAllByOwnerId(int ownerId)
        {
            return _repo.GetAllByOwnerId(ownerId).Count;
        }

        public List<int> GetYearsOfExisting(Accommodation accommodation)
        {
            var years = Enumerable.Range(
                accommodation.DateCreated.Year, 
                DateTime.Now.Year - accommodation.DateCreated.Year + 1).ToList();
            years.Reverse();
            return years;
        }

        public List<int> GetMonthsOfExisting(Accommodation accommodation, int year)
        {
            List<int> months = new();

            if (year == accommodation.DateCreated.Year)
                for (int month = accommodation.DateCreated.Month; month <= 12; month++)
                    months.Add(month);
            else if (year == DateTime.Now.Year)
                for (int month = 1; month <= DateTime.Now.Month; month++)
                    months.Add(month);
            else if (year > accommodation.DateCreated.Year && year < DateTime.Now.Year)
                for (int month = 1; month <= 12; month++)
                    months.Add(month);

            return months;
        }


        public void Search(ObservableCollection<Accommodation> accommodations, string nameTypeLocation, int durationSearch, int maxGuestsSearch)
        {
            accommodations.Clear();
            foreach(var acc in new ObservableCollection<Accommodation>(_repo.GetAll()))
                accommodations.Add(acc);

            if (nameTypeLocation == "Naziv/Tip/Lokacija") nameTypeLocation = string.Empty;
            string[] searchValues = nameTypeLocation.Split(" ");
            List<Accommodation> searchResults = accommodations.ToList();

            foreach (string value in searchValues)
                searchResults.RemoveAll(x => !x.ToString().ToLower().Contains(value.ToLower()));

            // Removing by numbers
            if (durationSearch > 0) searchResults.RemoveAll(x => x.MinReservationDays > durationSearch);
            if (maxGuestsSearch > 0) searchResults.RemoveAll(x => x.MaxGuestNumber < maxGuestsSearch);


            accommodations.Clear();
            foreach (var searchResult in searchResults)
                accommodations.Add(searchResult);
        }
        public List<AccommodationReservation> CheckReservations(List<AccommodationReservation> reservations, DateTime startDate, DateTime endDate, int accommodationId)
        {
            var conflictingReservation = reservations.FindAll(r => r.Accommodation.Id == accommodationId && (startDate < r.EndDate && r.StartDate < endDate));

            return conflictingReservation; 
        }
       
    }
}
