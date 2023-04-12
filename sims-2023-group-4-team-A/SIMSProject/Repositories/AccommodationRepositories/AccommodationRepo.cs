using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandler;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    public class AccommodationRepo : IAccommodationRepo
    {
        private readonly AccommodationFileHandler _fileHandler;
        private readonly ILocationRepo _locationRepo;
        private readonly IOwnerRepo _ownerRepo;
        private List<Accommodation> _accommodations;

        public AccommodationRepo(ILocationRepo locationRepo, IOwnerRepo ownerRepo)
        {
            _fileHandler = new();
            _accommodations = new();
            _locationRepo = locationRepo;
            _ownerRepo = ownerRepo;

            Load();
        }

        public void Load()
        {
            _accommodations = _fileHandler.Load();
            MapOwners();
            MapLocations();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }

        public Accommodation GetById(int accommodationId)
        {
            return _accommodations.Find(x => x.Id == accommodationId);
        }

        public List<Accommodation> GetAllByOwnerId(int ownerId)
        {
            return _accommodations.FindAll(x => x.Owner.Id == ownerId);
        }

        public int NextId()
        {
            return _accommodations.Count > 0 ? _accommodations.Max(x => x.Id) + 1 : 1;
        }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations.Add(accommodation);
            _fileHandler.Save(_accommodations);
            return accommodation;
        }

        public void SaveAll(List<Accommodation> accommodations)
        {
            _fileHandler.Save(accommodations);
            _accommodations = accommodations;
        }

        private void MapOwners()
        {
            foreach (var accommodation in _accommodations)
            {
                accommodation.Owner = _ownerRepo.GetById(accommodation.Owner.Id);
            }
        }

        private void MapLocations()
        {
            foreach (var accommodation in _accommodations)
            {
                accommodation.Location = _locationRepo.GetById(accommodation.Location.Id);
            }
        }
    }
}
