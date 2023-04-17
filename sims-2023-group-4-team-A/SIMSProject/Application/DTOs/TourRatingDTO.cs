using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.DTOs
{
    public class TourRatingDTO
    {
        public Tour Tour { get; set; } = new();
        public List<TourAppontmentRatingDTO> Ratings { get; set; } = new();
        public double OverallRating { get; set; }
        
        public TourRatingDTO()
        {
        }
        public TourRatingDTO(Tour tour, List<TourAppontmentRatingDTO> ratings)
        {
            Tour = tour;
            Ratings = ratings;
            OverallRating = CalcuateRating();
        }
        private double CalcuateRating()
        {
            return (double) Ratings.Sum(x => x.Raiting.Overall) / Ratings.Count;
        }
    }
}
