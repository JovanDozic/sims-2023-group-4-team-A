using SIMSProject.Application.DTOs;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace SIMSProject.Application.Services.TourServices
{
    public class GuideRatingService
    {
        private readonly IGuideRatingRepo _ratingRepo;
        private readonly ITourReservationRepo _tourReservationRepo;
        private readonly IGuideRepo _guideRepo;
        private readonly ITourGuestRepo _tourGuestRepo;
        private readonly ITourAppointmentRepo _tourAppointmentRepo;
        public GuideRatingService(IGuideRatingRepo ratingRepo, IGuideRepo guideRepo, ITourReservationRepo tourReservationRepo, ITourGuestRepo tourGuestRepo, ITourAppointmentRepo tourAppointmentRepo)
        {
            _ratingRepo = ratingRepo;
            _tourReservationRepo = tourReservationRepo;
            _guideRepo = guideRepo;
            _tourGuestRepo = tourGuestRepo;
            _tourAppointmentRepo = tourAppointmentRepo;
        
        }
        public void LeaveRating(GuideRating guideRating, int guideId)
        {
            _ratingRepo.Save(guideRating);
            _tourReservationRepo.Update(guideRating.TourReservation);
            UpdateGuideTotalRating(guideId);
        }

        public Guide GetById(int guideId)
        {
            return _guideRepo.GetById(guideId);
        }
        public void UpdateGuideTotalRating(int guideId)
        {
            var ratings = _ratingRepo.GetByGuideId(guideId);
            GetById(guideId).Rating = ratings.Average(x => x.Overall);
            _guideRepo.Update(GetById(guideId));
        }


        public List<TourAppontmentRatingDTO> MapRatingsByTour(int tourId)
        {
            List<TourAppontmentRatingDTO> tourRatings = new();
            List<TourGuest> users = _tourGuestRepo.GetAll();

            foreach(var rating in _ratingRepo.GetAll())
            {
                TourGuest? guest = users.Find(x => rating.TourReservation.TourAppointment.Id == x.TourAppointmentId && _tourAppointmentRepo.GetById(x.TourAppointmentId).Tour.Id == tourId);
                if(guest == null) continue;

                tourRatings.Add(new(rating, guest));
            }
            return tourRatings;
        }
    }
}
