using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface IVoucherRepo
    {
        public int NextId();
        public List<Voucher> GetAll();
        public List<Voucher> GetVouchersByGuestId(int guestId);
        public Voucher GetById(int id);
        public Voucher Save(Voucher voucher);
        public void SaveAll(List<Voucher> appointments);
        public void Update(Voucher voucher);
    }
}
