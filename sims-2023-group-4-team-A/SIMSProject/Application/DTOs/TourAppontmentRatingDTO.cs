using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.DTOs
{
    public class TourAppontmentRatingDTO
    {
        public GuideRating Rating { get; set; }
        public TourGuest User { get; set; }

        public TourAppontmentRatingDTO()
        {
        }
        public TourAppontmentRatingDTO(GuideRating rating, TourGuest guest)
        {
            User = guest;
            Rating = rating;
        }
    }
}
