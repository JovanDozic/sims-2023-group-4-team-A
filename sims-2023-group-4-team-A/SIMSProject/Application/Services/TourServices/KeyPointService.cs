using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class KeyPointService
    {
        private readonly IKeyPointRepo _repo;

        public KeyPointService(IKeyPointRepo repo)
        {
            _repo = repo;
        }

        public List<KeyPoint> FindAll()
        {
            return _repo.GetAll();
        }
    }
}
