using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.DTOs.TourDTOs
{
    public class GuestAgeGroupsDTO
    {
        public int Minors { get; set; } // less then 18 years old
        public int Adults { get; set; } // between 18 and 50 years old
        public int Seniors { get; set;} //older then 50

        public GuestAgeGroupsDTO()
        {
        }
        public GuestAgeGroupsDTO(int minors, int adults, int seniors)
        {
            Minors = minors;
            Adults = adults;
            Seniors = seniors;
        }
    }
}
