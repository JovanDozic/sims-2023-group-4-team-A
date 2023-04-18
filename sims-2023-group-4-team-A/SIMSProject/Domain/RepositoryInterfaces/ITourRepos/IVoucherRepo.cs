using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.ITourRepos
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
