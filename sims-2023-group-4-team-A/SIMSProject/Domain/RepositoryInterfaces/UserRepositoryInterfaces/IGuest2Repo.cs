using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces
{
    public interface IGuest2Repo
    {
        public Guest2 GetById(int guestId);
        public List<Guest2> GetAll();
        public int NextId();
        public Guest2 Save(Guest2 guest);
        public void SaveAll(List<Guest2> guests);
        public void Update(Guest2 guest);
    }
}
