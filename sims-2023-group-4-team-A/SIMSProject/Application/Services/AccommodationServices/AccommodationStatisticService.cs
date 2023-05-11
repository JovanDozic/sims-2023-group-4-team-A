using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Repositories;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationStatisticService
    {
        private readonly AccommodationService _accommodationService;
        private readonly AccommodationReservationService _reservationService;
        private readonly ReschedulingRequestService _requestService;
        // TODO: Renovation Service

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
                Type = Domain.Models.AccommodationStatisticType.YEARLY,
                Year = year,
                Month = 0,
                TotalReservations = reservations.Count,
                CancelledReservations = reservations.FindAll(x => x.Canceled).Count,
                RescheduledReservations = requests.Count
                // TODO: statistic.RenovationRecommendation = ...
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
                Type = Domain.Models.AccommodationStatisticType.MONTHLY,
                Year = year,
                Month = month,
                TotalReservations = reservations.Count,
                CancelledReservations = reservations.FindAll(x => x.Canceled).Count,
                RescheduledReservations = requests.Count
                // TODO: statistic.RenovationRecommendation = ...
            };

            return statistic;
        }


        public List<AccommodationStatistic> GetAllMonthlyStatistics(Accommodation accommodation, int year)
        {
            List<AccommodationStatistic> statistics = new();
            foreach(var month in _accommodationService.GetMonthsOfExisting(accommodation, year))
            {
                statistics.Add(GetMonthlyStatistic(accommodation, year, month));
            }

            return statistics;
        }
    }
}
