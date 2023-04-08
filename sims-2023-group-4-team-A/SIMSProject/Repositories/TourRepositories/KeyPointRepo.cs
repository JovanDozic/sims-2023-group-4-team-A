using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.FileHandler;
using SIMSProject.Model;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.TourRepositories
{
    public class KeyPointRepo: IKeyPointRepo
    {
        private readonly KeyPointFileHandler _fileHandler;
        private List<KeyPoint> _keyPoints;

        public KeyPointRepo()
        {
            _fileHandler = new KeyPointFileHandler();
            _keyPoints = _fileHandler.Load();

            AssociateKeyPoints();
        }

        public int NextId()
        {
           return _keyPoints.Count > 0 ?_keyPoints.Max(x => x.Id) + 1 : 1;
        }

        public List<KeyPoint> GetAll()
        {
            return _keyPoints;
        }

        public KeyPoint Save(KeyPoint keyPoint)
        {
            keyPoint.Id = NextId();
            _keyPoints.Add(keyPoint);
            _fileHandler.Save(_keyPoints);
            return keyPoint;
        }

        public void SaveAll(List<KeyPoint> keyPoints)
        {
            _fileHandler.Save(keyPoints);
            _keyPoints = keyPoints;
        }

        private void AssociateKeyPoints()
        {
            LocationFileHandler tourLocationFileHandler = new();
            List<Location> toursLocations = tourLocationFileHandler.Load();
            TourFileHandler tourFileHandler = new();
            List<Tour> tours = tourFileHandler.Load();
            TourKeyPointFileHandler tourKeyPointFileHandler = new();
            List<TourKeyPoint> tourKeyPoints = tourKeyPointFileHandler.Load();

            foreach (var keyPoint in _keyPoints)
            {
                AssociateLocation(keyPoint, toursLocations);
                AssociateTours(keyPoint, tours, tourKeyPoints);
            }
        }

        private static void AssociateLocation(KeyPoint keyPoint, List<Location> toursLocations)
        {
            keyPoint.Location = toursLocations.Find(x => x.Id == keyPoint.LocationId) ?? throw new SystemException("Error!No matching location!");
        }

        private static void AssociateTours(KeyPoint keyPoint, List<Tour> tours, List<TourKeyPoint> tourKeyPoints)
        {
            List<TourKeyPoint> pairs = tourKeyPoints.FindAll(x => x.KeyPointId == keyPoint.Id);
            foreach (var pair in pairs)
            {
                Tour? matchingTour = tours.Find(x => x.Id == pair.TourId) ?? throw new SystemException("Error!No matching tour!");
                keyPoint.Tours.Add(matchingTour);
            }
        }


    }
}
