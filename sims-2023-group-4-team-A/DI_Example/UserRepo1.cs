using System.Runtime.CompilerServices;

namespace DI_Example
{
    public class UserRepo1 : IUserRepo
    {
        private UserFileHandler1 _fileHandler;
        private List<User> _users;

        public UserRepo1()
        {
            _fileHandler = new();
            _users = _fileHandler.Load();
        }

        public void Add(User user)
        {
            _users.Add(user);
            _fileHandler.Save(_users);
        }

        public List<User> GetAll()
        {
            return _users;
        }


    }
}
