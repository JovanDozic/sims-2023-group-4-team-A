using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ITourGuestRepo
    {
        public List<TourGuest> GetAll();
        public TourGuest Save(TourGuest tourGuest);
        public void SaveAll(List<TourGuest> tourGuests);
        public List<TourGuest> GetGuests(int tourAppointmentId);
        public TourGuest? GetTourGuest(TourGuest tourGuest);
        public List<TourGuest> GetPresentGuests();
    }
}
