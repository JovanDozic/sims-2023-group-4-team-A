using SIMSProject.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model;
using SIMSProject.Observer;

namespace SIMSProject.Controller
{
    public class KeyPointController
    {
        private KeyPointDAO _keyPoints;
        public KeyPoint KeyPoint;

        public KeyPointController()
        {
            _keyPoints = new();
            KeyPoint = new();
        }

        public List<KeyPoint> GetAll()
        {
            return _keyPoints.GetAll();
        }

        public void SaveAll(List<KeyPoint> keyPoints)
        {
            _keyPoints.SaveAll(keyPoints);
        }

        public KeyPoint Create(KeyPoint keyPoint)
        {
            return _keyPoints.Save(keyPoint);
        }

        public void Subscribe(IObserver observer)
        {
            _keyPoints.Subscribe(observer);
        }
    }
}
