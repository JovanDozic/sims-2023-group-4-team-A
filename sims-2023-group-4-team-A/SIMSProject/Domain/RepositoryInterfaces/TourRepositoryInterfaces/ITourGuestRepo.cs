using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ITourGuestRepo
    {
        public List<TourGuest> GetAll();
        public TourGuest Save(TourGuest tourGuest);
        public void SaveAll(List<TourGuest> tourGuests);
        public List<TourGuest> GetGuests(int tourAppointmentId);
        public List<TourGuest> GetPresentGuests();
        public List<TourGuest> GetAllPendingByUser(User user);
        public TourGuest GetTourGuest(int appointmentId, int guestId);
    }
}
