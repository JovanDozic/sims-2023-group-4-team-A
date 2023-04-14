using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class GuideRatingViewModel : INotifyPropertyChanged
    {
        private readonly User _user;
        private GuideRating _rating = new();
        private GuideRatingService _ratingService;
        public TourAppointment TourAppointment { get; set; }
        public TourReservation TourReservation
        {
            get => _rating.TourReservation;
            set
            {
                if(_rating.TourReservation == value) return;
                _rating.TourReservation = value;
                OnPropertyChanged();
            }
        }

        public int GuideKnowledge
        {
            get => _rating.GuideKnowledge;
            set
            {
                if( _rating.GuideKnowledge == value || value is < 1 or > 5) return;
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
        public double Overall
        {
            get => _rating.Overall;
            set
            {
                if (value == _rating.Overall) return;
                _rating.Overall = value;
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
        public GuideRatingViewModel(User user, TourAppointment tourAppointment)
        {
            _user = user;
            _ratingService = Injector.GetService<GuideRatingService>();
            TourAppointment = tourAppointment;
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
