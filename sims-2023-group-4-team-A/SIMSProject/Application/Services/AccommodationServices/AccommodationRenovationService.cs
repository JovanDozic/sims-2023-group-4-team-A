using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationRenovationService
    {
        private readonly IAccommodationRenovationRepo _repo;
        private readonly AccommodationReservationService _reservationService;

        public AccommodationRenovationService(IAccommodationRenovationRepo repo)
        {
            _repo = repo;
            _reservationService = Injector.GetService<AccommodationReservationService>();
        }

        public void SaveRenovation(AccommodationRenovation renovation)
        {
            _repo.Save(renovation);
        }

        public List<AccommodationRenovation> GetAll()
        {
            _repo.Load();
            return _repo.GetAll();
        }

        public List<AccommodationRenovation> GetAllByAccommodationId(int accommodationId)
        {
            return GetAll().FindAll(x => x.Accommodation.Id == accommodationId);
        }

        public List<AccommodationRenovation> GetAllUncanceledByAccommodationId(int accommodationId)
        {
            return GetAllByAccommodationId(accommodationId).FindAll(x => !x.IsCancelled);
        }

        private List<DateRange> GetSchedule(Accommodation accommodation)
        {
            List<DateRange> schedule = new();
            foreach (var renovation in GetAllUncanceledByAccommodationId(accommodation.Id))
            {
                schedule.Add(new DateRange(renovation.StartDate, renovation.EndDate));
            }
            return schedule;
        }

        public List<DateRange> GetAvailableDateRanges(Accommodation accommodation, DateTime startDate, DateTime endDate, int numberOfDays)
        {
            var reservationsSchedule = _reservationService.GetSchedule(accommodation);
            var renovationsSchedule = GetSchedule(accommodation);

            var dateRangesToCheck = GenerateAllPossibleDateRanges(startDate, endDate, numberOfDays);

            var availableDateRanges = dateRangesToCheck
                .Where(range => !DoesOverlapWithAnyRange(reservationsSchedule, range))
                .Where(range => !DoesOverlapWithAnyRange(renovationsSchedule, range))
                .ToList();

            return availableDateRanges;
        }


        private List<DateRange> GenerateAllPossibleDateRanges(DateTime startDate, DateTime endDate, int numberOfDays)
        {
            var dateRanges = new List<DateRange>();

            for (var date = startDate; date.AddDays(numberOfDays - 1) <= endDate; date = date.AddDays(1))
            {
                dateRanges.Add(new DateRange { StartDate = date, EndDate = date.AddDays(numberOfDays - 1) });
            }

            return dateRanges;
        }

        private bool DoesOverlapWithAnyRange(List<DateRange> ranges, DateRange rangeToCheck)
        {
            return ranges.Any(range => range.StartDate <= rangeToCheck.EndDate && range.EndDate >= rangeToCheck.StartDate);
        }

        public void CancelRenovation(AccommodationRenovation renovation)
        {
            renovation.IsCancelled = true;
            _repo.Update(renovation);
        }
    }
}
