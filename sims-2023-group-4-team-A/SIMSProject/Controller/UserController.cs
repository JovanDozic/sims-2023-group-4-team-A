using SIMSProject.Model;
using SIMSProject.Model.DAO;
using System.Collections.Generic;

namespace SIMSProject.Controller
{
    public class UserController
    {
        private UserDAO _users;
        public User User;

        public UserController()
        {
            _users = new();
            User = new();
        }

        public List<User> GetAll()
        {
            return _users.GetAll();
        }

        public void SaveAll(List<User> users)
        {
            _users.SaveAll(users);
        }

        public User Create(User user)
        {
            return _users.Save(user);
        }

        public User GetById(int id)
        {
            return _users.Get(id);
        }
    }
}
