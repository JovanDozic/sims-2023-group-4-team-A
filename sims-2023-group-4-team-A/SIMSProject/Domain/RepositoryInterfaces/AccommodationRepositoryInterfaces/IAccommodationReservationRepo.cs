using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IAccommodationReservationRepo
    {
        public AccommodationReservation GetById(int reservationId);
        public List<AccommodationReservation> GetAllByAccommodationId(int accommodationId);
        public List<AccommodationReservation> GetAll();
        public int NextId();
        public AccommodationReservation Save(AccommodationReservation reservation);
        public void SaveAll(List<AccommodationReservation> reservations);
        public void Update(AccommodationReservation reservation);
    }
}
