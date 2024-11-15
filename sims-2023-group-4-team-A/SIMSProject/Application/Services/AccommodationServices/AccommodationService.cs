﻿using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationService
    {
        private readonly IAccommodationRepo _repo;
        private readonly OwnerRatingService _ratingService;
        private AccommodationReservationService _reservationService;
        private AccommodationRenovationService _renovationService;

        public AccommodationService(IAccommodationRepo repo)
        {
            _repo = repo;
            _ratingService = Injector.GetService<OwnerRatingService>();
            _reservationService = Injector.GetService<AccommodationReservationService>();
            _renovationService = Injector.GetService<AccommodationRenovationService>();
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
        public List<AccommodationReservation> CheckReservations(List<AccommodationReservation> reservations, DateTime startDate, DateTime endDate)
        {
            var conflictingReservations = reservations.FindAll(r => startDate < r.EndDate && r.StartDate < endDate);

            return conflictingReservations; 
        }

        public void SearchForFreeAccommodations(ObservableCollection<Accommodation> accommodations, DateTime startDate, DateTime endDate, int daysNum, int guestNum)
        {
            accommodations.Clear();
            foreach (var acc in new ObservableCollection<Accommodation>(_repo.GetAll()))
                accommodations.Add(acc);

            List<Accommodation> searchResults = accommodations.ToList();

            var reservedAccommodations = _reservationService.GetAllUncancelled().FindAll(r => startDate < r.EndDate && r.StartDate < endDate).Select(r => r.Accommodation);
            var renovatingAccommodations = _renovationService.GetAllUncanceled().Where(r => startDate < r.EndDate && r.StartDate < endDate).Select(r => r.Accommodation);

            var commonAccommodations = reservedAccommodations.Union(renovatingAccommodations).ToList();

            var accommodationsToExclude = commonAccommodations.Select(a => a.Id).ToList();

            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                searchResults.RemoveAll(a => accommodationsToExclude.Contains(a.Id));
            
            searchResults.RemoveAll(a => a.MaxGuestNumber < guestNum);
            searchResults.RemoveAll(a => a.MinReservationDays > daysNum);

            accommodations.Clear();
            foreach (var searchResult in searchResults)
                accommodations.Add(searchResult);
        }

        public List<AccommodationRenovation> CheckRenovations(List<AccommodationRenovation> renovations, DateTime startDate, DateTime endDate)
        {
            var conflictingRenovations = renovations.FindAll(r => startDate < r.EndDate && r.StartDate < endDate);

            return conflictingRenovations;
        }
        public List<DateRange> FindReservedOrRenovatingDateRanges(Accommodation accommodation)
        {
            var reservedDateRanges = _reservationService.GetAllUncanceledByAccommodationId(accommodation.Id)
                .Select(r => new DateRange(r.StartDate, r.EndDate))
                .ToList();

            var renovatingDateRanges = _renovationService.GetAllUncanceledByAccommodationId(accommodation.Id)
                .Select(r => new DateRange(r.StartDate, r.EndDate))
                .ToList();

            var allDateRanges = reservedDateRanges.Concat(renovatingDateRanges).ToList();
            var mergedDateRanges = MergeDateRanges(allDateRanges);

            return mergedDateRanges;
        }


        private List<DateRange> MergeDateRanges(List<DateRange> dateRanges)
        {
            dateRanges.Sort((d1, d2) => d1.StartDate.CompareTo(d2.StartDate));

            var mergedRanges = new List<DateRange>();

            var currentRange = dateRanges[0];
            for (int i = 1; i < dateRanges.Count; i++)
            {
                var nextRange = dateRanges[i];

                if (currentRange.EndDate >= nextRange.StartDate)
                {
                    currentRange = new DateRange(currentRange.StartDate, nextRange.EndDate);
                }
                else
                {
                    mergedRanges.Add(currentRange);
                    currentRange = nextRange;
                }
            }

            mergedRanges.Add(currentRange);

            return mergedRanges;
        }

        public List<Accommodation> GetAllByLocation(Location location)
        {
            return GetAll().FindAll(a => a.Location.Id == location.Id) ?? new();
        }

        public bool UserHasAccommodationsInLocation(User user, Location location)
        {
            return GetAllByOwnerId(user.Id).Any(a => a.Location.Id == location.Id);
        }

        internal AccommodationType GetMostUsedTypeBuUser(User user)
        {
            var accommodations = GetAllByOwnerId(user.Id);
            var types = accommodations.Select(a => a.Type).ToList();
            var mostUsedType = types.GroupBy(t => t).OrderByDescending(t => t.Count()).First().Key;
            return mostUsedType;
        }
    }
}
