using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.FileHandlers.TourFileHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourKeyPointRepo: ITourKeyPointRepo
    {
        private readonly TourKeyPointFileHandler _fileHandler;
        private List<TourKeyPoint> _tourKeyPoints;

        public TourKeyPointRepo()
        {
            _fileHandler = new TourKeyPointFileHandler();
            _tourKeyPoints = _fileHandler.Load();
        }

        public List<TourKeyPoint> GetAll()
        {
            return _tourKeyPoints;
        }

        public TourKeyPoint Save(TourKeyPoint tourKeyPoint)
        {
            _tourKeyPoints.Add(tourKeyPoint);
            _fileHandler.Save(_tourKeyPoints);
            return tourKeyPoint;
        }

        public void SaveAll(List<TourKeyPoint> tourKeyPoints)
        {
            _fileHandler.Save(tourKeyPoints);
            _tourKeyPoints = tourKeyPoints;
        }
    }
}
