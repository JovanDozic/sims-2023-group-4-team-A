using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.FileHandler;
using SIMSProject.Model;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SIMSProject.Repositories.TourRepositories
{
    public class KeyPointRepo: IKeyPointRepo
    {
        private readonly KeyPointFileHandler _fileHandler;
        private List<KeyPoint> _keyPoints;
        private readonly ILocationRepo _locationrepo;

        public KeyPointRepo(ILocationRepo locationRepo)
        {
            _fileHandler = new KeyPointFileHandler();
            _keyPoints = _fileHandler.Load();
            _locationrepo = locationRepo;

            MapKeyPoints();
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

        private void MapKeyPoints()
        {
            foreach (var keyPoint in _keyPoints)
            {
                keyPoint.Location = _locationrepo.GetAll().Find(x => x.Id == keyPoint.Location.Id) ?? throw new SystemException("Error!No matching location!");
            }
        }
    }
}
