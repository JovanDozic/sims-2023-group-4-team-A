using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.ITourRepos
{
    public interface IKeyPointRepo
    {
        public int NextId();
        public List<KeyPoint> GetAll();
        public KeyPoint Save(KeyPoint keyPoint);
        public void SaveAll(List<KeyPoint> keyPoints);
    }
}
