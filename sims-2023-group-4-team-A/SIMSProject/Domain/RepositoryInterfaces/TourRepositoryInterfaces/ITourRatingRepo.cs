using SIMSProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ITourRatingRepo
    {
        public List<TourAppointmentRatingDTO> MapRatingsByTour(int tourId);
        public List<DateTime> GetRatedDatesByTourRating(TourRatingDTO tourRating);
    }
}
