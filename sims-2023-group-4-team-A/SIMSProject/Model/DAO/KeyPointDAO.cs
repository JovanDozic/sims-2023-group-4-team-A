using System;
using System.Collections.Generic;
using System.Linq;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models;
using SIMSProject.FileHandler;
using SIMSProject.Observer;

namespace SIMSProject.Model.DAO
{
    public class KeyPointDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly KeyPointFileHandler _fileHandler;
        private List<KeyPoint> _keyPoints;

        public KeyPointDAO()
        {
            _fileHandler = new KeyPointFileHandler();
            _keyPoints = _fileHandler.Load();
            _observers = new List<IObserver>();

            AssociateKeyPoints();
        }

        public int NextId()
        {
            try
            {
                return _keyPoints.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
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
            NotifyObservers();
            return keyPoint;
        }

        public void SaveAll(List<KeyPoint> keyPoints)
        {
            _fileHandler.Save(keyPoints);
            _keyPoints = keyPoints;
            NotifyObservers();
        }

        private void AssociateKeyPoints()
        {
            LocationFileHandler tourLocationFileHandler = new();
            List<Location> toursLocations = tourLocationFileHandler.Load();

            foreach (var keyPoint in _keyPoints)
            {
                AssociateLocation(keyPoint, toursLocations);
            }
        }

        private static void AssociateLocation(KeyPoint keyPoint, List<Location> toursLocations)
        {
            keyPoint.Location = toursLocations.Find(x => x.Id == keyPoint.LocationId) ?? throw new SystemException("Error!No matching location!");
        }

        // [OBSERVERS]
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}