using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.ITourRepos
{
    public interface ITourGuestRepo
    {
        public List<TourGuest> GetAll();
        public TourGuest Save(TourGuest tourGuest);
        public void SaveAll(List<TourGuest> tourGuests);
        public List<TourGuest> GetGuests(int tourAppointmentId);
    }
}
