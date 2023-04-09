using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandler.UserFileHandler;
using System.Collections.Generic;

namespace SIMSProject.Repositories.UserRepositories
{
    public class UserRepo : IUserRepo
    {
        private List<Owner> _owners;
        private List<Guide> _guides;
        private List<Guest> _guests;

        public UserRepo()
        {
            _owners = new OwnerFileHandler().Load();
            _guides = new GuideFileHandler().Load();
            _guests = new GuestFileHandler().Load();
        }
        
        public List<Guest> GetAllGuests()
        {
            return _guests;
        }

        public List<Guide> GetAllGuides()
        {
            return _guides;
        }

        public List<Owner> GetAllOwners()
        {
            return _owners;
        }

        public Guest? GetGuestById(int id)
        {
            return _guests.Find(x => x.Id == id);
        }

        public Guide? GetGuideById(int id)
        {
            return _guides.Find(x => x.Id == id);
        }

        public Owner? GetOwnerById(int id)
        {
            return _owners.Find(x => x.Id == id);
        }
    }
}
