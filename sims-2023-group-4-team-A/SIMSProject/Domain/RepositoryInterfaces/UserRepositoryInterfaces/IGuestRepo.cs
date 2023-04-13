using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces
{
    public interface IGuestRepo
    {
        public Guest GetById(int guestId);
        public List<Guest> GetAll();
        public int NextId();
        public Guest Save(Guest guest);
        public void SaveAll(List<Guest> guests);
        public void Update(Guest guest);
    }
}
