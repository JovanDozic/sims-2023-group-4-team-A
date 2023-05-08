using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.DTOs
{
    public class TourAppointmentRatingDTO
    {
        public GuideRating Rating { get; set; } = new();
        public TourGuest User { get; set; } = new();

        public TourAppointmentRatingDTO()
        {
        }
        public TourAppointmentRatingDTO(GuideRating rating, TourGuest guest)
        {
            User = guest;
            Rating = rating;
        }
    }
}
