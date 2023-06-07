using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ITourReservationRepo
    {
        public int NextId();
        public List<TourReservation> GetAll();
        public List<TourReservation> GetAllByGuestId(int guestId);
        public TourReservation GetById(int reservationId);
        public TourReservation Save(TourReservation tourReservation);
        public void SaveAll(List<TourReservation> tourReservations);
        public void Update (TourReservation tourReservation);
        public void UpdateToVoucherWon(List<TourReservation> tourReservations);
        public IEnumerable<TourReservation> GetCompletedReservationsByTour(int tourId);
        public IEnumerable<TourReservation> GetCompletedReservations(int? targetYear);
        
    }
}
