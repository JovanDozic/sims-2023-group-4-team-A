using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface IKeyPointRepo
    {
        public int NextId();
        public List<KeyPoint> GetAll();
        public KeyPoint Save(KeyPoint keyPoint);
        public void SaveAll(List<KeyPoint> keyPoints);
    }
}
