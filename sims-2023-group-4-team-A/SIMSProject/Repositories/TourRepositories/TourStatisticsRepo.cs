using SIMSProject.Application.DTOs.TourDTOs;
using SIMSProject.Application.DTOs;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourStatisticsRepo: ITourStatisticsRepo
    {
        private readonly ITourReservationRepo _tourReservationRepo;
        private readonly ITourGuestRepo _tourGuestRepo;

        public TourStatisticsRepo(ITourReservationRepo tourReservationRepo, ITourGuestRepo tourGuestRepo)
        {
            _tourReservationRepo = tourReservationRepo;
            _tourGuestRepo = tourGuestRepo;
        }
        public VoucherUsageDTO GetVoucherUsageByTour(int tourId)
        {
            return _tourReservationRepo.GetCompletedReservationsByTour(tourId)
               .Join(_tourGuestRepo.GetPresentGuests(),
               tr => new { KeyOne = tr.TourAppointment.Id, KeyTwo = tr.GuestId },
               tg => new { KeyOne = tg.TourAppointment.Id, KeyTwo = tg.Guest.Id },
               (tr, tg) => new { tr.VoucherUsed, tr.GuestNumber })
               .GroupBy(x => 1)
               .Select(group => new
               {
                   used = group.Sum(x => x.VoucherUsed ? x.GuestNumber : 0),
                   unused = group.Sum(x => !x.VoucherUsed ? x.GuestNumber : 0)
               })
               .Select(x => new VoucherUsageDTO(x.used, x.unused))
               .FirstOrDefault();
        }
        public GuestAgeGroupsDTO SumGuestByAgeGroup(int tourId)
        {
            return _tourReservationRepo.GetCompletedReservationsByTour(tourId)
               .Join(_tourGuestRepo.GetPresentGuests(),
               tr => new { KeyOne = tr.TourAppointment.Id, KeyTwo = tr.GuestId },
               tg => new { KeyOne = tg.TourAppointment.Id, KeyTwo = tg.Guest.Id },
               (tr, tg) => new { tg.Guest.Birthday, tg.TourAppointment.Date, tr.GuestNumber })
               .GroupBy(x => 1)
               .Select(group => new GuestAgeGroupsDTO
               {
                   Minors = group.Sum(x => (x.Date - x.Birthday <= TimeSpan.FromDays(365 * 18)) ? x.GuestNumber : 0),
                   Adults = group.Sum(x => (x.Date - x.Birthday > TimeSpan.FromDays(365 * 18) && x.Date - x.Birthday <= TimeSpan.FromDays(365 * 50)) ? x.GuestNumber : 0),
                   Seniors = group.Sum(x => (x.Date - x.Birthday > TimeSpan.FromDays(365 * 50)) ? x.GuestNumber : 0)
               }).FirstOrDefault();
        }
        public TourStatisticsDTO GetMostVisitedTour(int? targetYear = null)
        {
            return _tourReservationRepo.GetCompletedReservations(targetYear)
               .Join(_tourGuestRepo.GetPresentGuests(),
               tr => new { KeyOne = tr.TourAppointment.Id, KeyTwo = tr.GuestId },
               tg => new { KeyOne = tg.TourAppointment.Id, KeyTwo = tg.Guest.Id },
               (tr, tg) => new { tr.TourAppointment.Tour, tr.GuestNumber, tr.TourAppointment })
               .GroupBy(joined => joined.Tour)
               .Select(group => new TourStatisticsDTO
               {
                   Tour = group.Key,
                   TotalAppointments = group.DistinctBy(x => x.TourAppointment).Count(),
                   TotalGuests = group.Sum(x => x.GuestNumber)
               }).OrderByDescending(result => result.TotalGuests)
               .FirstOrDefault();
        }
    }
}
