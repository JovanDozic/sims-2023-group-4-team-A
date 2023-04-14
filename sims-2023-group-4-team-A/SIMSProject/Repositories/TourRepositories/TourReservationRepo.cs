using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.FileHandler;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourReservationRepo : ITourReservationRepo
    {
        private readonly TourReservationFileHandler _fileHandler;
        private List<TourReservation> _tourReservations;
        public TourReservationRepo()
        {
            _fileHandler = new TourReservationFileHandler();
            _tourReservations = _fileHandler.Load();
            
        }
        public List<TourReservation> GetAll()
        {
            return _tourReservations;
        }

        public int NextId()
        {
            return _tourReservations.Count > 0 ? _tourReservations.Max(x => x.Id) + 1 : 1;
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            tourReservation.Id = NextId();
            _tourReservations.Add(tourReservation);
            _fileHandler.Save(_tourReservations);
            return tourReservation;
        }

        public void SaveAll(List<TourReservation> tourReservations)
        {
            _fileHandler.Save(tourReservations);
            _tourReservations = tourReservations;
        }
    }
}
