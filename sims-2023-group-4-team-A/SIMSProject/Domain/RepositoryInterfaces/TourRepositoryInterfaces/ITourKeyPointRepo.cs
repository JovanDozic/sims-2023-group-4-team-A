using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ITourKeyPointRepo
    {
        public List<TourKeyPoint> GetAll();
        public TourKeyPoint Save(TourKeyPoint tourKeyPoint);
        public void SaveAll(List<TourKeyPoint> tourKeyPoints);
    }
}
