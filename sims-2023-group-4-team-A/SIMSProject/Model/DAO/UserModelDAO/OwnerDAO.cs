using System.Collections.Generic;
using System.Linq;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Model.UserModel;
using SIMSProject.Observer;

namespace SIMSProject.Model.DAO.UserModelDAO
{
    public class OwnerDAO
    {
        private readonly List<IObserver> _observers;
        private readonly OwnerFileHandler _fileHandler;
        private List<Owner> _owners;

        public OwnerDAO()
        {
            _fileHandler = new OwnerFileHandler();
            _owners = _fileHandler.Load();
            _observers = new List<IObserver>();

            var accommodations = new AccommodationDAO().GetAll();
            foreach (var owner in _owners)
            {
                owner.Accommodations = accommodations.FindAll(x => x.Owner.Id == owner.Id);
            }
        }

        public int NextId()
        {
            return _owners.Max(x => x.Id) + 1;
        }

        public List<Owner> GetAll()
        {
            return _owners;
        }

        public Owner Save(Owner user)
        {
            user.Id = NextId();
            _owners.Add(user);
            _fileHandler.Save(_owners);
            NotifyObservers();
            return user;
        }

        public void SaveAll(List<Owner> users)
        {
            _fileHandler.Save(users);
            _owners = users;
            NotifyObservers();
        }

        public Owner Get(int id)
        {
            return _owners.Find(x => x.Id == id);
        }

        // [OBSERVERS]
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}