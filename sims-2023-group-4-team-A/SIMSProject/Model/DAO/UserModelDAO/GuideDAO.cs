using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Model.UserModel;
using SIMSProject.Observer;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Model.DAO.UserModelDAO
{
    public class GuideDAO
    {
        private List<IObserver> _observers;
        private GuideFileHandler _fileHandler;
        private List<Guide> _guides;

        public GuideDAO()
        {
            _fileHandler = new();
            _guides = _fileHandler.Load();
            _observers = new();
        }

        public int NextId() { return _guides.Max(x => x.Id) + 1; }

        public List<Guide> GetAll() { return _guides; }

        public Guide Save(Guide user)
        {
            user.Id = NextId();
            _guides.Add(user);
            _fileHandler.Save(_guides);
            NotifyObservers();
            return user;
        }

        public void SaveAll(List<Guide> users)
        {
            _fileHandler.Save(users);
            _guides = users;
            NotifyObservers();
        }

        public Guide Get(int id)
        {
            return _guides.Find(x => x.Id == id);
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
