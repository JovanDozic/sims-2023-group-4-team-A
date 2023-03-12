﻿using SIMSProject.Observer;
using SIMSProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMSProject.Model.DAO
{
    public class KeyPointDAO : ISubject
    {
        private List<IObserver> _observers;
        private KeyPointRepository _repository;
        private List<KeyPoint> _keyPoints;

        public KeyPointDAO()
        {
            _repository = new();
            _keyPoints = _repository.Load();
            _observers = new();


            TourLocationRepository tourLocationRepository = new();
            TourRepository tourRepository = new();
            TourKeyPointRepository tourKeyPointRepository = new();

            List<TourLocation> toursLocations = tourLocationRepository.Load();
            List<Tour> tours = tourRepository.Load();
            List<TourKeyPoint> tourKeyPoints = tourKeyPointRepository.Load();

            foreach(var keyPoint in _keyPoints)
            {
                keyPoint.Location = toursLocations.Find(x => x.Id == keyPoint.LocationId);

                foreach(var tour in tours)
                {
                    foreach(var pair in tourKeyPoints)
                    {
                        if(pair.TourId == tour.Id)
                        {
                            keyPoint.Tours.Add(tour);
                        }
                    }
                }
            }
            

        }

        public int NextId() { return _keyPoints.Max(x => x.Id) + 1; }
        public List<KeyPoint> GetAll() { return _keyPoints; }

        public KeyPoint Save(KeyPoint keyPoint)
        {
            keyPoint.Id = NextId();
            _keyPoints.Add(keyPoint);
            _repository.Save(_keyPoints);
            NotifyObservers();
            return keyPoint;
        }

        public void SaveAll(List<KeyPoint> keyPoints)
        {
            _repository.Save(keyPoints);
            _keyPoints = keyPoints;
            NotifyObservers();
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }

}
