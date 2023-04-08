using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application1.Services.TourServices
{
    public class VoucherSevice
    {
        private readonly VoucherRepo _voucherRepository;

        public VoucherSevice()
        {
            _voucherRepository = new();
        }

        public void DeleteExpired()
        {
            _voucherRepository.GetAll().RemoveAll(x => DateTime.Compare(x.Expiration, DateTime.Now) < 0);
            _voucherRepository.SaveAll(_voucherRepository.GetAll());
        }

        public void GiveVouchers(List<TourGuest> guests, ObtainingReason reason)
        {
            foreach (var guest in guests)
            {
                _voucherRepository.Save(new(guest.GuestId, reason));
            }
        }
    }
}
