using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Repositories.TourRepositories;
using SIMSProject.WPF.ViewModels.TourViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class VoucherService
    {
        private readonly IVoucherRepo _repo;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourGuestService _tourGuestService;

        public VoucherService(IVoucherRepo repo)
        {
            _repo = repo;
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
        }

        public void DeleteExpired()
        {
            _repo.GetAll().RemoveAll(x => DateTime.Compare(x.Expiration, DateTime.Now) < 0);
            _repo.SaveAll(_repo.GetAll());
        }

        public void GiveVouchers(List<TourGuest> guests, ObtainingReason reason, int guideId = -1)
        {
            foreach (var guest in guests)
            {
                _repo.Save(new(guest.Guest.Id, guideId, reason, false));
            }
        }

        public void GiveVouchersForQuitting(Guide guide, ObtainingReason reason)
        {
            foreach (var appointment in _tourAppointmentService.GetAllUpcoming(guide.Id))
            {
                var guests = _tourGuestService.GetGuests(appointment);
                GiveVouchers(guests, reason);
            }
            _repo.UpdateExistingVouchers(guide.Id);
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
