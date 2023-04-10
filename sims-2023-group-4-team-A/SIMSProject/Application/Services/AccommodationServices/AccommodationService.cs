using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System.Collections.Generic;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationService
    {
        private readonly IAccommodationRepo _repo;

        public AccommodationService(IAccommodationRepo repo)
        {
            _repo = repo;
        }

        public List<Accommodation> GetAllByOwnerId(int ownerId)
        {
            return _repo.GetAllByOwnerId(ownerId);
        }
    }
}
