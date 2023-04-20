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

        public Location GetLocation(string city, string country)
        {
            return _repo.GetByInfo(city, country) ?? _repo.Save(new Location(city, country));
        }
    }
}
