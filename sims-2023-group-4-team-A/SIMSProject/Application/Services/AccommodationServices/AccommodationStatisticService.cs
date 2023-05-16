using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationStatisticService
    {
        private readonly AccommodationService _accommodationService;
        private readonly AccommodationReservationService _reservationService;
        private readonly ReschedulingRequestService _requestService;
        // TODO: SelectedRenovation Service

        public AccommodationStatisticService()
        {
            _accommodationService = Injector.GetService<AccommodationService>();
            _reservationService = Injector.GetService<AccommodationReservationService>();
            _requestService = Injector.GetService<ReschedulingRequestService>();
        }

        public AccommodationStatistic GetYearlyStatistic(Accommodation accommodation, int year)
        {
            var reservations = _reservationService.GetAllByAccommodationId(accommodation.Id).FindAll(x => x.StartDate.Year == year);
            var requests = _requestService.GetAllByAccommodationId(accommodation.Id).FindAll(x => x.Reservation.StartDate.Year == year);

            AccommodationStatistic statistic = new()
            {
                Accommodation = accommodation,
                Type = Domain.Models.AccommodationStatisticType.YEARLY,
                Year = year,
                Month = 0,
                TotalReservations = reservations.Count,
                CancelledReservations = reservations.FindAll(x => x.Canceled).Count,
                RescheduledReservations = requests.Count,
                // TODO: statistic.RenovationRecommendation = ...
                OccupancyPercentage = CalculateOccupancyPercentage(accommodation, year)
            };

            return statistic;
        }

        public List<AccommodationStatistic> GetAllYearlyStatistics(Accommodation accommodation)
        {
            List<AccommodationStatistic> statistics = new();
            foreach (var year in _accommodationService.GetYearsOfExisting(accommodation))
            {
                statistics.Add(GetYearlyStatistic(accommodation, year));
            }

            double highestPercentage = statistics.Max(x => x.OccupancyPercentage);

            foreach (AccommodationStatistic statistic in statistics)
                if (statistic.OccupancyPercentage == highestPercentage && highestPercentage != 0)
                    statistic.Best = true;

            return statistics;
        }



        public AccommodationStatistic GetMonthlyStatistic(Accommodation accommodation, int year, int month)
        {
            var reservations = _reservationService.GetAllByAccommodationId(accommodation.Id)
                .FindAll(x => x.StartDate.Year == year && x.StartDate.Month == month);
            var requests = _requestService.GetAllByAccommodationId(accommodation.Id)
                .FindAll(x => x.Reservation.StartDate.Year == year && x.Reservation.StartDate.Month == month);

            AccommodationStatistic statistic = new()
            {
                Accommodation = accommodation,
                Type = Domain.Models.AccommodationStatisticType.MONTHLY,
                Year = year,
                Month = month,
                TotalReservations = reservations.Count,
                CancelledReservations = reservations.FindAll(x => x.Canceled).Count,
                RescheduledReservations = requests.Count,
                // TODO: statistic.RenovationRecommendation = ...
                OccupancyPercentage = CalculateOccupancyPercentage(accommodation, year, month)
            };

            return statistic;
        }

        public List<AccommodationStatistic> GetAllMonthlyStatistics(Accommodation accommodation, int year)
        {
            List<AccommodationStatistic> statistics = new();

            foreach (var month in _accommodationService.GetMonthsOfExisting(accommodation, year))
                statistics.Add(GetMonthlyStatistic(accommodation, year, month));

            double highestPercentage = statistics.Max(x => x.OccupancyPercentage);

            foreach (AccommodationStatistic statistic in statistics)
                if (statistic.OccupancyPercentage == highestPercentage && highestPercentage != 0)
                    statistic.Best = true;

            return statistics;
        }



        public int CalculateOccupancyPercentage(Accommodation accommodation, int year, int month)
        {
            int totalDaysInMonth = DateTime.DaysInMonth(year, month);
            int occupiedDays = 0;
            DateTime monthStartDate = new(year, month, 1);
            DateTime monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

            foreach (AccommodationReservation reservation in _reservationService.GetAllUncanceledByAccommodationId(accommodation.Id))
            {
                if (reservation.StartDate <= monthEndDate && reservation.EndDate >= monthStartDate)
                {
                    DateTime reservationStartDate = reservation.StartDate;
                    DateTime reservationEndDate = reservation.EndDate;

                    // Adjust the reservation dates
                    if (reservationStartDate < monthStartDate) reservationStartDate = monthStartDate;
                    if (reservationEndDate > monthEndDate) reservationEndDate = monthEndDate;

                    int reservationDays = (int)(reservationEndDate - reservationStartDate).TotalDays + 1;
                    occupiedDays += Math.Max(0, reservationDays);
                }
            }

            double occupancyPercentage = (double)occupiedDays / totalDaysInMonth * 100;
            //MessageBox.Show($"Percentage for month {month} / year {year}: {(int)Math.Round(occupancyPercentage)}% ({occupiedDays} out of {totalDaysInMonth})");
            return (int)Math.Round(occupancyPercentage);
        }

        public int CalculateOccupancyPercentage(Accommodation accommodation, int year)
        {
            int totalDaysInYear = DateTime.IsLeapYear(year) ? 366 : 365;
            int occupiedDays = 0;
            DateTime yearStartDate = new(year, 1, 1);
            DateTime yearEndDate = new(year, 12, 31);

            foreach (AccommodationReservation reservation in _reservationService.GetAllUncanceledByAccommodationId(accommodation.Id))
            {
                if (reservation.StartDate <= yearEndDate && reservation.EndDate >= yearStartDate)
                {
                    DateTime reservationStartDate = reservation.StartDate;
                    DateTime reservationEndDate = reservation.EndDate;

                    // Adjust the reservation dates
                    if (reservationStartDate < yearStartDate) reservationStartDate = yearStartDate;
                    if (reservationEndDate > yearEndDate) reservationEndDate = yearEndDate;

                    int reservationDays = (int)(reservationEndDate - reservationStartDate).TotalDays + 1;
                    occupiedDays += Math.Max(0, reservationDays);
                }
            }

            double occupancyPercentage = (double)occupiedDays / totalDaysInYear * 100;
            //MessageBox.Show($"Percentage for year {year}: {(int)Math.Round(occupancyPercentage)}% ({occupiedDays} out of {totalDaysInYear})");
            return (int)Math.Round(occupancyPercentage);
        }



    }
}
