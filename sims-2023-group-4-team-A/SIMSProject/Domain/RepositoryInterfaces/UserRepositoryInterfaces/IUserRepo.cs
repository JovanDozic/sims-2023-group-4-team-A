using SIMSProject.Model.UserModel;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces
{
    public interface IUserRepo
    {
        public Owner? GetOwnerById(int id);
        public List<Owner> GetAllOwners();

        public Guide? GetGuideById(int id);
        public List<Guide> GetAllGuides();

        public Guest? GetGuestById(int id);
        public List<Guest> GetAllGuests();

    }
}
