using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System.Collections.Generic;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationRenovationService
    {
        private readonly IAccommodationRenovationRepo _repo;
        
        public AccommodationRenovationService(IAccommodationRenovationRepo repo)
        {
            _repo = repo;
        }

        public void SaveRenovation(AccommodationRenovation renovation)
        {
            _repo.Save(renovation);
        }

        public List<AccommodationRenovation> GetAll()
        {
            //_repo.Load();
            return _repo.GetAll();
        }

        public List<AccommodationRenovation> GetAllByAccommodationId(int accommodationId)
        {
            return GetAll().FindAll(x => x.Accommodation.Id == accommodationId);
        }
    }
}
