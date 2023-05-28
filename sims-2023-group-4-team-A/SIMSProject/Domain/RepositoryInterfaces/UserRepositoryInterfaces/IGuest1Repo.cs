using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces
{
    public interface IGuest1Repo
    {
        public Guest1 GetById(int guestId);
        public List<Guest1> GetAll();
        public int NextId();
        public Guest1 Save(Guest1 guest);
        public void SaveAll(List<Guest1> guests);
        public void Update(Guest1 guest);
    }
}
