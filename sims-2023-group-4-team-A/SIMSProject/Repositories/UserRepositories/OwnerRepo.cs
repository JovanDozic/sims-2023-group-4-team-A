using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandler.UserFileHandler;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.UserRepositories
{
    public class OwnerRepo : IOwnerRepo
    {
        private OwnerFileHandler _fileHandler;
        // TODO: private readonly IOwnerRepo _ratingRepo;
        private List<Owner> _owners;

        public OwnerRepo(/* TODO: IGuestRatingRepo ratingRepo */)
        {
            _fileHandler = new();
            // TODO: _ratingRepo = ratingRepo;
            _owners = _fileHandler.Load();

            CalculateRating();
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
            return _owners.Count > 0 ? _owners.Max(x => x.Id) + 1 : 1;
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

        private void CalculateRating()
        {
            foreach (var owner in _owners)
            {
                // TODO: implement
            }
        }
    }
}
