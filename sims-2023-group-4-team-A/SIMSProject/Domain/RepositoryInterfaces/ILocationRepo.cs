using SIMSProject.Domain.Models;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces
{
    public interface ILocationRepo
    {
        public Location GetById(int locationId);
        public List<Location> GetAll();
        public int NextId();
        public Location Save(Location location);
        public void SaveAll(List<Location> locations);
    }
}
