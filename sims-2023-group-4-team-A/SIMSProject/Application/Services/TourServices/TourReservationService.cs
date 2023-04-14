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
        private readonly ITourReservationRepo _repo;

        public TourReservationService(ITourReservationRepo repo)
        {
            _repo = repo;
        }
    }
}
