using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.ITourRepos
{
    public interface ITourKeyPointRepo
    {
        public List<TourKeyPoint> GetAll();
        public TourKeyPoint Save(TourKeyPoint tourKeyPoint);
        public void SaveAll(List<TourKeyPoint> tourKeyPoints);
    }
}
