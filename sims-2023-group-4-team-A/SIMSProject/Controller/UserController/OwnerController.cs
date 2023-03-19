using SIMSProject.Model.DAO.UserModelDAO;
using SIMSProject.Model.UserModel;
using System.Collections.Generic;

namespace SIMSProject.Controller.UserController
{
    public class OwnerController
    {
        private OwnerDAO _owners;
        public Owner Owner;

        public OwnerController()
        {
            _owners = new();
            Owner = new();
        }

        public List<Owner> GetAll()
        {
            return _owners.GetAll();
        }

        public void SaveAll(List<Owner> owner)
        {
            _owners.SaveAll(owner);
        }

        public Owner Create(Owner owner)
        {
            return _owners.Save(owner);
        }

        public Owner GetByID(int id)
        {
            return _owners.Get(id);
        }
    }
}
