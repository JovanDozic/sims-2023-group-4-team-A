using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application1.Services.TourServices
{
    public class TourKeyPointService
    {
        private readonly TourKeyPointRepo _repository;

        public TourKeyPointService()
        {
            _repository = new();
        }

        public void CreateNewPairs(Tour tour)
        {
            foreach(var keyPoint in tour.KeyPoints)
            {
                _repository.Save(new(tour.Id, keyPoint.Id));
            }
        }
    }
}
