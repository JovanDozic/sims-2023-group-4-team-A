using SIMSProject.Model.DAO;
using SIMSProject.Model;
using System.Collections.Generic;

namespace SIMSProject.Controller
{
    public class AddressController
    {
        private AddressDAO _addresses;
        public Address Address;

        public AddressController()
        {
            _addresses = new();
            Address = new();
        }

        public List<Address> GetAll()
        {
            return _addresses.GetAll();
        }

        public void SaveAll(List<Address> addresses)
        {
            _addresses.SaveAll(addresses);
        }

        public Address Create(Address address)
        {
            return _addresses.Save(address);
        }

        public Address GetById(int id)
        {
            return _addresses.Get(id);
        }
    }
}
