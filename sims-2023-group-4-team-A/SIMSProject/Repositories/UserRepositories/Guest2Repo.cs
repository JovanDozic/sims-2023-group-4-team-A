using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandlers.UserFileHandler;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.UserRepositories
{
    public class Guest2Repo : IGuest2Repo
    {
        private Guest2FileHandler _fileHandler;
        private List<Guest2> _guests;

        public Guest2Repo()
        {
            _fileHandler = new();
            _guests = _fileHandler.Load();
        }

        public List<Guest2> GetAll()
        {
            return _guests;
        }

        public Guest2 GetById(int guestId)
        {
            return _guests.Find(x => x.Id == guestId);
        }

        public int NextId()
        {
            return _guests.Count > 0 ? _guests.Max(x => x.Id) + 1 : 1;
        }

        public Guest2 Save(Guest2 guest)
        {
            guest.Id = NextId();
            _guests.Add(guest);
            _fileHandler.Save(_guests);
            return guest;
        }

        public void SaveAll(List<Guest2> guests)
        {
            _fileHandler.Save(guests);
            _guests = guests;
        }

        public void Update(Guest2 guest)
        {
            Guest2 guestToUpdate = GetById(guest.Id) ?? throw new System.Exception("Updating guest failed!");
            int index = _guests.IndexOf(guestToUpdate);
            _guests[index] = guest;
            _fileHandler.Save(_guests);
        }
    }
}
