using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.ITourRepos
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
    }
}
