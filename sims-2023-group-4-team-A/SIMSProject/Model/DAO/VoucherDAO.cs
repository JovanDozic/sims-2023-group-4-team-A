using SIMSProject.FileHandler;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model.DAO
{
    public class VoucherDAO: ISubject
    {
        private List<IObserver> _observers;
        private readonly VoucherFileHandler _fileHandler;
        private List<Voucher> _vouchers;

        public VoucherDAO()
        {
            _fileHandler = new();
            _vouchers = _fileHandler.Load();
            _observers = new();
        }

        public int NextId() { return _vouchers.Max(x => x.Id) + 1; }
        public List<Voucher> GetAll() { return _vouchers; }
        public List<Voucher> GetVouchersByGuestId(int guestId)
        {
            return GetAll().FindAll(x => x.GuestId == guestId && DateTime.Compare(x.Expiration, DateTime.Now) > 0); 
        }
        public Voucher Get(int id)
        {
            return _vouchers.Find(x => x.Id == id);
        }

        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();
           
            _vouchers.Add(voucher);
            _fileHandler.Save(_vouchers);
            NotifyObservers();
            return voucher;
        }

        public void SaveAll(List<Voucher> appointments)
        {
            _fileHandler.Save(appointments);
            _vouchers = appointments;
            NotifyObservers();
        }

        public void Delete(Voucher voucher)
        {
            _vouchers.Remove(voucher);
            _fileHandler.Save(_vouchers);
            NotifyObservers();
        }

        public void DeleteExpired()
        {
            _vouchers.RemoveAll(x => DateTime.Compare(x.Expiration, DateTime.Now) < 0);
            _fileHandler.Save(_vouchers);
            NotifyObservers();
        }



        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }

        
    }
}
