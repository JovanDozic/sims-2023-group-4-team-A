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
        private readonly ITourReservationRepo _tourReservationRepo;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourGuestService _tourGuestService;
        

        public VoucherService(IVoucherRepo repo, ITourReservationRepo tourReservationRepo)
        {
            _repo = repo;
            _tourReservationRepo = tourReservationRepo;
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

        public void WinVoucher(int guestId)
        {
            List<TourReservation> reservations = GetReservationsFromPastYear(guestId);

            if (reservations.Count/5 >= 1)
            {
                for (int i = 0; i < reservations.Count/5; i++)
                {
                    _repo.Save(new Voucher(guestId, -1, ObtainingReason.WON, false));
                    _tourReservationRepo.UpdateToVoucherWon(reservations);
                }
            }
            
        }

        public List<TourReservation> GetReservationsFromPastYear(int guestId)
        {
            List<TourReservation> pastYearReservations = _tourReservationRepo.GetAll().FindAll(x => x.IsVoucherWon == false && x.GuestId == guestId && x.TourAppointment.Date >= DateTime.Now.AddYears(-1) && x.TourAppointment.Date <= DateTime.Now && x.TourAppointment.TourStatus == Status.COMPLETED);
            List<TourReservation> reservations = new List<TourReservation>(); 
            foreach (var reservation in pastYearReservations)
            {
                foreach (var tourGuest in _tourGuestService.GetGuests(reservation.TourAppointment))
                {
                    if (tourGuest.Guest.Id == guestId && tourGuest.GuestStatus == GuestAttendance.PRESENT)
                    {
                        reservations.Add(reservation);
                    }
                }
            }
            return reservations;
        }
    }
}
