using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class GuideRatingViewModel : ViewModelBase
    {
        private readonly User _user;
        private GuideRating _rating = new();
        private GuideRatingService _ratingService;
        public TourReservation TourReservation
        {
            get => _rating.TourReservation;
            set
            {
                if (_rating.TourReservation == value) return;
                _rating.TourReservation = value;
                _rating.TourReservation.Id = value.Id;
                OnPropertyChanged();
            }
        }

        public int GuideKnowledge
        {
            get => _rating.GuideKnowledge;
            set
            {
                if (_rating.GuideKnowledge == value || value is < 1 or > 5) return;
                _rating.GuideKnowledge = value;
                OnPropertyChanged();
            }
        }

        public int LanguageProficiency
        {
            get => _rating.LanguageProficiency;
            set
            {
                if (_rating.LanguageProficiency == value || value is < 1 or > 5) return;
                _rating.LanguageProficiency = value;
                OnPropertyChanged();
            }
        }
        public int TourEntertainmentRating
        {
            get => _rating.TourEntertainmentRating;
            set
            {
                if (_rating.TourEntertainmentRating == value || value is < 1 or > 5) return;
                _rating.TourEntertainmentRating = value;
                OnPropertyChanged();
            }
        }
        public int OrganizationQualityRating
        {
            get => _rating.OrganizationQualityRating;
            set
            {
                if (_rating.OrganizationQualityRating == value || value is < 1 or > 5) return;
                _rating.OrganizationQualityRating = value;
                OnPropertyChanged();
            }
        }

        public List<string> ImageURLs
        {
            get => _rating.ImageURLs;
            set
            {
                if (_rating.ImageURLs == value) return;
                _rating.ImageURLs = value;
                OnPropertyChanged();
            }
        }
        public string ImageURLsCSV
        {
            get => _rating.ImageURLsCSV;
            set
            {
                if(_rating.ImageURLsCSV == value) return;
                _rating.ImageURLsCSV = value;
                OnPropertyChanged();
            }
        }
        
        public string Comment
        {
            get => _rating.Comment;
            set
            {
                if (_rating.Comment == value) return;
                _rating.Comment = value;
                OnPropertyChanged();
            }
        }
        public GuideRatingViewModel(User user, TourReservation tourReservation)
        {
            _user = user;
            _ratingService = Injector.GetService<GuideRatingService>();
            TourReservation = tourReservation;
            
        }

        public void AddImageToGuideRating(string imageUrl)
        {
            ImageURLs.Add(imageUrl);
        }

        public void LeaveRating(int guideId)
        {
            //TourReservation.TourAppointment.Tour.GuideId = TourReservation.TourAppointment.Tour.Guide.Id;
            TourReservation.GuideRated = true;
            _ratingService.LeaveRating(_rating, guideId);
        }
    }
}
