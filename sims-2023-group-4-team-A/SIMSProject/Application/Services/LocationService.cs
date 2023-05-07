using SIMSProject.Domain.Models;
using SIMSProject.Domain.RepositoryInterfaces;
using System.Collections.Generic;

namespace SIMSProject.Application.Services
{
    public class LocationService
    {
        private readonly ILocationRepo _repo;

        public LocationService(ILocationRepo repo)
        {
            _repo = repo;
        }

        public List<Location> FindAll()
        {
            return _repo.GetAll();
        }

        public Location GetLocation(Location location)
        {
            return _repo.GetByInfo(location.City, location.Country) ?? _repo.Save(new Location(location.City, location.Country));
        }
    }
}
