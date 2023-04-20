using SIMSProject.Domain.Models;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.FileHandlers;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories
{
    public class LocationRepo : ILocationRepo
    {
        private LocationFileHandler _fileHandler;
        private List<Location> _locations;

        public LocationRepo()
        {
            _fileHandler = new();
            _locations = _fileHandler.Load();
        }

        public List<Location> GetAll()
        {
            return _locations;
        }

        public Location GetById(int locationId)
        {
            return _locations.Find(x => x.Id == locationId);
        }

        public Location? GetByInfo(string city, string country)
        {
            return _locations.Find(x => x.City == city && x.Country == country);
        }

        public int NextId()
        {
            return _locations.Count > 0 ? _locations.Max(x => x.Id) + 1 : 1;
        }

        public Location Save(Location location)
        {
            location.Id = NextId();
            _locations.Add(location);
            _fileHandler.Save(_locations);
            return location;
        }

        public void SaveAll(List<Location> locations)
        {
            _fileHandler.Save(locations);
            _locations = locations;
        }
    }
}
