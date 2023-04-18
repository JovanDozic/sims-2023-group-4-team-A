using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class TourKeyPointService
    {
        private readonly ITourKeyPointRepo _repo;

        public TourKeyPointService(ITourKeyPointRepo repo)
        {
            _repo = repo;
        }

        public void CreateNewPairs(Tour tour)
        {
            foreach(var keyPoint in tour.KeyPoints)
            {
                _repo.Save(new(tour.Id, keyPoint.Id));
            }
        }
    }
}
