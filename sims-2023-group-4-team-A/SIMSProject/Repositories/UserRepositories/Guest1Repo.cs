using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandlers.UserFileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.UserRepositories
{
    internal class Guest1Repo: IGuest1Repo
    {
        private Guest1FileHandler _fileHandler;
        private List<Guest1> _guests;

        public Guest1Repo()
        {
            _fileHandler = new();
            _guests = _fileHandler.Load();
        }

        public List<Guest1> GetAll()
        {
            return _guests;
        }

        public Guest1 GetById(int guestId)
        {
            return _guests.Find(x => x.Id == guestId);
        }

        public int NextId()
        {
            return _guests.Count > 0 ? _guests.Max(x => x.Id) + 1 : 1;
        }

        public Guest1 Save(Guest1 guest)
        {
            guest.Id = NextId();
            _guests.Add(guest);
            _fileHandler.Save(_guests);
            return guest;
        }

        public void SaveAll(List<Guest1> guests)
        {
            _fileHandler.Save(guests);
            _guests = guests;
        }

        public void Update(Guest1 guest)
        {
            Guest1 guestToUpdate = GetById(guest.Id) ?? throw new System.Exception("Updating guest failed!");
            int index = _guests.IndexOf(guestToUpdate);
            _guests[index] = guest;
            _fileHandler.Save(_guests);
        }
    }
}
