using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces
{
    public interface IOwnerRepo
    {
        public Owner GetById(int ownerId);
        public List<Owner> GetAll();
        public int NextId();
        public Owner Save(Owner owner);
        public void SaveAll(List<Owner> owners);
        public void Update(Owner owner);
    }
}
