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
        public TourReservation Save(TourReservation tourReservation);
        public void SaveAll(List<TourReservation> tourReservations);
    }
}
