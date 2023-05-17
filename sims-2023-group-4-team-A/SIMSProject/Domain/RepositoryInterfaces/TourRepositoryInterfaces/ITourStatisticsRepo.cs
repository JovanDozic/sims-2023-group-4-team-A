using SIMSProject.Application.DTOs;
using SIMSProject.Application.DTOs.TourDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ITourStatisticsRepo
    {
        public VoucherUsageDTO GetVoucherUsageByTour(int tourId);
        public GuestAgeGroupsDTO SumGuestByAgeGroup(int tourId);
        public TourStatisticsDTO GetMostVisitedTour(int? targetYear = null);
    }
}
