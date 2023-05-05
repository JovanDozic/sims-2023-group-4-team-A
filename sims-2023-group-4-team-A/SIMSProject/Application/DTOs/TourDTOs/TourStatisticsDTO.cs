using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.DTOs
{
    public class TourStatisticsDTO
    {
        public Tour Tour { get; set; } = new();
        public int TotalAppointments { get; set; }
        public int TotalGuests { get; set; }
        public TourStatisticsDTO()
        {
        }

        public TourStatisticsDTO(Tour tour, int totalAppointemnts, int totalGuests)
        {
            Tour = tour;
            TotalAppointments = totalAppointemnts;
            TotalGuests = totalGuests;
        }
    }

}
