using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.TourRepositories
{
    public class VoucherRepo: IVoucherRepo
    {
        private readonly VoucherFileHandler _fileHandler;
        private List<Voucher> _vouchers;

        public VoucherRepo()
        {
            _fileHandler = new();
            _vouchers = _fileHandler.Load();
        }

        public int NextId() { return _vouchers.Count != 0 ? _vouchers.Max(x => x.Id) + 1 : 1; }
        public List<Voucher> GetAll() { return _vouchers; }
        public List<Voucher> GetVouchersByGuestId(int guestId)
        {
            return GetAll().FindAll(x => x.Guest.Id == guestId && DateTime.Compare(x.Expiration, DateTime.Now) > 0);
        }
        public Voucher GetById(int id)
        {
            return _vouchers.Find(x => x.Id == id);
        }
        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();

            _vouchers.Add(voucher);
            _fileHandler.Save(_vouchers);
            return voucher;
        }

        public void SaveAll(List<Voucher> appointments)
        {
            _fileHandler.Save(appointments);
            _vouchers = appointments;
        }

        public void Update(Voucher voucher)
        {
            Voucher voucherToUpdate = GetById(voucher.Id) ?? throw new Exception("Updating voucher failed!");
            int index = _vouchers.IndexOf(voucherToUpdate);
            _vouchers[index] = voucher;
            _fileHandler.Save(_vouchers);
        }
    }
}
