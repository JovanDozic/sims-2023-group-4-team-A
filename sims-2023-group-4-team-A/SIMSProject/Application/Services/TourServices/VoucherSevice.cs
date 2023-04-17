using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class VoucherSevice
    {
        private readonly IVoucherRepo _repo;

        public VoucherSevice(IVoucherRepo repo)
        {
            _repo = repo;
        }

        public void DeleteExpired()
        {
            _repo.GetAll().RemoveAll(x => DateTime.Compare(x.Expiration, DateTime.Now) < 0);
            _repo.SaveAll(_repo.GetAll());
        }

        public void GiveVouchers(List<TourGuest> guests, ObtainingReason reason)
        {
            foreach (var guest in guests)
            {
                _repo.Save(new(guest.GuestId, reason, false));
            }
        }
        public List<Voucher> GetVouchersByGuestId(int guestId)
        {
            return _repo.GetVouchersByGuestId(guestId);
        }
        public void Update(Voucher voucher)
        {
            _repo.Update(voucher);
        }
    }
}
