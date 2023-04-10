using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.FileHandler;
using SIMSProject.Repositories.UserRepositories;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    public class AccommodationRepo : IAccommodationRepo
    {
        private AccommodationFileHandler _fileHandler;
        private List<Accommodation> _accommodations;

        public AccommodationRepo()
        {
            _fileHandler = new();
            _accommodations = _fileHandler.Load();

            // TODO: call mapping functions
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
            try
            {
                return _accommodations.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
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

        public void MapOwners()
        {
            OwnerRepo repo = new();
            foreach (var accommodation in _accommodations)
            {
                accommodation.Owner = repo.GetById(accommodation.Owner.Id);
            }
        }

        public void MapLocations()
        {
            // TODO: implement
            LocationRepo repo = new();
            foreach (var accommodation in _accommodations)
            {
                accommodation.Location = repo.GetById(accommodation.Location.Id);
            }
        }
    }
}
