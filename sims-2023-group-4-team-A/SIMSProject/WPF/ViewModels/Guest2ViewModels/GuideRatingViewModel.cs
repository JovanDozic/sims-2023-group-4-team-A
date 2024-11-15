﻿using Microsoft.Win32;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class GuideRatingViewModel : ViewModelBase
    {
        #region Polja
        private readonly User _user;
        private int _guideId;
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
        private string _imageURL;
        public string ImageURL
        {
            get => _imageURL;
            set
            {
                if (_imageURL == value) return;
                _imageURL = value;
                OnPropertyChanged();
            }
        }
        private Visibility _lblCommentRequiredVisibility;
        public Visibility LblCommentRequiredVisibility
        {
            get => _lblCommentRequiredVisibility;
            set
            {
                if (value == _lblCommentRequiredVisibility) return;
                _lblCommentRequiredVisibility = value;
                OnPropertyChanged(nameof(LblCommentRequiredVisibility));
            }
        }
        private Visibility _lblSuccessfullyRatedVisibility;
        public Visibility LblSuccessfullyRatedVisibility
        {
            get => _lblSuccessfullyRatedVisibility;
            set
            {
                if (value == _lblSuccessfullyRatedVisibility) return;
                _lblSuccessfullyRatedVisibility = value;
                OnPropertyChanged(nameof(LblSuccessfullyRatedVisibility));
            }
        }
        private Visibility _lblURLAddedVisibility;
        public Visibility LblURLAddedVisibility
        {
            get => _lblURLAddedVisibility;
            set
            {
                if (value == _lblURLAddedVisibility) return;
                _lblURLAddedVisibility = value;
                OnPropertyChanged(nameof(LblURLAddedVisibility));
            }
        }
        private bool _isRatingEnabled;
        public bool IsRatingEnabled
        {
            get => _isRatingEnabled;
            set
            {
                if(value == _isRatingEnabled) return;
                _isRatingEnabled = value;
                OnPropertyChanged(nameof(IsRatingEnabled));
            }
        }
        public NavigationService NavService { get; set; }
        public RelayCommand RateGuideCommand { get; set; }
        public RelayCommand AddImageCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        #endregion

        #region Konstruktori
        public GuideRatingViewModel(User user, TourReservation tourReservation, int guideId, NavigationService navigationService)
        {
            _user = user;
            _guideId = guideId;
            NavService = navigationService;
            _ratingService = Injector.GetService<GuideRatingService>();

            LblCommentRequiredVisibility = Visibility.Hidden;
            LblSuccessfullyRatedVisibility = Visibility.Hidden;
            LblURLAddedVisibility = Visibility.Hidden;
            IsRatingEnabled = true;

            RateGuideCommand = new RelayCommand(RateGuideExecute, CanExecute_Command);
            AddImageCommand = new RelayCommand(AddImageExecute, CanExecute_Command);
            GoBackCommand = new RelayCommand(GoBackExecute, CanExecute_Command);

            TourReservation = tourReservation;
            
        }
        #endregion

        #region Akcije
        public void RateGuideExecute()
        {
            if (!string.IsNullOrWhiteSpace(Comment))
            {
                TourReservation.GuideRated = true;
                _ratingService.LeaveRating(_rating, _guideId);
                LblCommentRequiredVisibility = Visibility.Hidden;
                LblSuccessfullyRatedVisibility = Visibility.Visible;
                LblURLAddedVisibility = Visibility.Hidden;
                IsRatingEnabled = false;
                return;
            }
            LblCommentRequiredVisibility = Visibility.Visible;
            LblURLAddedVisibility = Visibility.Hidden;

        }
       
        public void AddImageExecute()
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            bool? result = openFileDialog.ShowDialog();
            if (result is not true || result is null) return;
            if (ImageURLs.Find(x => x.Equals(openFileDialog.FileName)) != null) return;
            ImageURLs.Add(openFileDialog.FileName);
            LblURLAddedVisibility = Visibility.Visible;
        }

        private void GoBackExecute()
        {
            NavService.GoBack();
        }
        private bool CanExecute_Command()
        {
            return true;
        }
        #endregion
    }
}
