using System.Collections.Generic;
using SIMSProject.Model;
using SIMSProject.Model.DAO;
namespace SIMSProject.Controller
{
    public class TourKeyPointController
    {
        private TourKeyPointDAO _tourKeyPoints;
        public TourKeyPoint TourKeyPoint;

        public TourKeyPointController()
        {
            _tourKeyPoints = new();
            TourKeyPoint = new();
        }

        public List<TourKeyPoint> GetAll()
        {
            return _tourKeyPoints.GetAll();
        }

        public void SaveAll(List<TourKeyPoint> tourKeyPoints)
        {
            _tourKeyPoints.SaveAll(tourKeyPoints);
        }

        public TourKeyPoint Create(TourKeyPoint tourKeyPoint)
        {
            return _tourKeyPoints.Save(tourKeyPoint);
        }
    }
}
