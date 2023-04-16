using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class TourReservationService
    {
        private readonly ITourReservationRepo _tourReservationRepo;

        public TourReservationService(ITourReservationRepo repo)
        {
            _tourReservationRepo = repo;
        }
        public List<TourReservation> GetAllByGuestId(int guestId)
        {
            return _tourReservationRepo.GetAllByGuestId(guestId);
        }
    }
}
