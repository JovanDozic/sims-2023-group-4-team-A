using SIMSProject.Application.DTOs.TourDTOs;
using SIMSProject.Application.DTOs;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Domain.Injectors;

namespace SIMSProject.Application.Services.TourServices
{
    public class TourStatisticsService
    {
        private readonly ITourStatisticsRepo _tourStatisticsRepo;
        private readonly TourService _tourService;
        private readonly TourAppointmentService  _tourAppointmentService;

        public TourStatisticsService(ITourStatisticsRepo tourStatisticsRepo)
        {
            _tourService = Injector.GetService<TourService>();
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourStatisticsRepo = tourStatisticsRepo;
        }

        public Dictionary<int, VoucherUsageDTO> MapToursVoucherUsage()
        {
            Dictionary<int, VoucherUsageDTO> dictionary = new();
            foreach (var finished in _tourAppointmentService.GetToursWithFinishedAppointments())
            {
                dictionary.TryAdd(finished.Id, GetVoucherUsageByTour(finished.Id));
            }
            return dictionary;
        }
        public Dictionary<int, GuestAgeGroupsDTO> MapToursGuestAgeGroups()
        {
            Dictionary<int, GuestAgeGroupsDTO> dictionary = new();
            foreach (var finished in _tourAppointmentService.GetToursWithFinishedAppointments())
            {
                dictionary.TryAdd(finished.Id, SumGuestByAgeGroup(finished.Id));
            }
            return dictionary;
        }
        public VoucherUsageDTO GetVoucherUsageByTour(int tourId)
        {
            return _tourStatisticsRepo.GetVoucherUsageByTour(tourId);
        }

        public GuestAgeGroupsDTO SumGuestByAgeGroup(int tourId)
        {
            return _tourStatisticsRepo.SumGuestByAgeGroup(tourId);
        }

        public TourStatisticsDTO GetMostVisitedTour(int? targetYear = null)
        {
            return _tourStatisticsRepo.GetMostVisitedTour(targetYear);
        }
    }
}
