using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandler.UserFileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.UserRepositories
{
    public class OwnerRepo : IOwnerRepo
    {
        private OwnerFileHandler _fileHandler;
        private List<Owner> _owners;

        public OwnerRepo()
        {
            _fileHandler = new();
            _owners = _fileHandler.Load();
            // TODO: map accommodations
        }

        public List<Owner> GetAll()
        {
            return _owners;
        }

        public Owner GetById(int ownerId)
        {
            return _owners.Find(x => x.Id == ownerId);
        }

        public int NextId()
        {
            try
            {
                return _owners.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public Owner Save(Owner owner)
        {
            owner.Id = NextId();
            _owners.Add(owner);
            _fileHandler.Save(_owners);
            return owner;
        }

        public void SaveAll(List<Owner> owners)
        {
            _fileHandler.Save(owners);
            _owners = owners;
        }

        public void MapAccommodations()
        {
            // TODO: implement

        }
    }
}
