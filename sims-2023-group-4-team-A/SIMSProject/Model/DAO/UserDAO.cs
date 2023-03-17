using SIMSProject.Observer;
using SIMSProject.FileHandler;
using System.Collections.Generic;
using System.Linq;
using SIMSProject.Model.UserModel;

namespace SIMSProject.Model.DAO
{
    public class UserDAO : ISubject
    {
        private List<IObserver> _observers;
        private UserFileHandler _fileHandler;
        private List<User> _users;

        public UserDAO()
        {
            _fileHandler = new();
            _users = _fileHandler.Load();
            _observers = new();
        }

        public int NextId() { return _users.Max(x => x.Id) + 1; }
        
        public List<User> GetAll() { return _users; }

        public User Save(User user)
        {
            user.Id = NextId();
            _users.Add(user);
            _fileHandler.Save(_users);
            NotifyObservers();
            return user;
        }

        public void SaveAll(List<User> users)
        {
            _fileHandler.Save(users);
            _users = users;
            NotifyObservers();
        }

        public User Get(int id)
        {
            return _users.Find(x => x.Id == id);
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
