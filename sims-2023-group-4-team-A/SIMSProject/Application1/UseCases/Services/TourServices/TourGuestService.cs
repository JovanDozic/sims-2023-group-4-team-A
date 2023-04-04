using SIMSProject.Domain.TourModels;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application1.UseCases.Services.TourServices
{
    public class TourGuestService
    {
        private readonly TourGuestRepository _repository;

        public TourGuestService()
        {
            _repository = new();
        }

        public void SignUpGuest(int guestId, int tourAppointmentId) 
        {
            TourGuest? tourGuest = _repository.GetAll().Find(x => x.GuestId == guestId && x.AppointmentId == tourAppointmentId);
            if (tourGuest == null) return;

            tourGuest.GuestStatus = "Prijavljen";
            _repository.SaveAll(_repository.GetAll());
        }


        public void MakeGuestPresent(int guestId, int tourAppointmentId, KeyPoint currentKeyPoint) 
        {
            TourGuest? tourGuest = _repository.GetAll().Find(x => x.GuestId == guestId && x.AppointmentId == tourAppointmentId);
            if (tourGuest == null) return;

            tourGuest.JoinedKeyPoint = currentKeyPoint;
            tourGuest.JoinedKeyPointId = currentKeyPoint.Id;
            
        }
    }
}
