using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.FileHandler;
using SIMSProject.Model;
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
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }

        public Accommodation GetById(int accommodationId)
        {
            return _accommodations.Find(x => x.Id == accommodationId);
        }

        public List<Accommodation> GetByOwnerId(int ownerId)
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

        public void MapOwner()
        {
            // TODO: implement
            
        }

        public void MapLocation()
        {
            // TODO: implement
        }
    }
}
