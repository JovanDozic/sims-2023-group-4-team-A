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
    public class TourGuestService
    {
        private readonly ITourGuestRepo _repo;

        public TourGuestService(ITourGuestRepo repo)
        {
            _repo = repo;
        }

        public void SignUpGuest(int guestId, int tourAppointmentId)
        {
            TourGuest? tourGuest = _repo.GetAll().Find(x => x.Guest.Id == guestId && x.TourAppointment.Id == tourAppointmentId);
            if (tourGuest == null) return;

            tourGuest.GuestStatus = GuestAttendance.PENDING;
            _repo.SaveAll(_repo.GetAll());
        }
        public void MakeGuestPresent(int guestId, int tourAppointmentId, KeyPoint currentKeyPoint)
        {
            TourGuest? tourGuest = _repo.GetAll().Find(x => x.Guest.Id == guestId && x.TourAppointment.Id == tourAppointmentId);
            if (tourGuest == null) return;

            tourGuest.JoiningPoint = currentKeyPoint;
        }

        public List<TourGuest> GetGuests(TourAppointment appointment)
        {
            return _repo.GetGuests(appointment.Id);
        }
        public TourGuest GetGuest(TourAppointment appointment, int guestId)
        {
            return GetGuests(appointment).Find(x=>x.Guest.Id == guestId);
        }
        public TourGuest Save(TourGuest tourGuest)
        {
            return _repo.Save(tourGuest);
        }
    }
}
