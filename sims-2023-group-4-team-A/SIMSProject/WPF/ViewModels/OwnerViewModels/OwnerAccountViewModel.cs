using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.OwnerViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccountViews;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerAccountViewModel : ViewModelBase
    {
        private App App = (App)System.Windows.Application.Current;
        private User _user;
        private OwnerAccountView _ownerAccountView;
        private AccommodationService _accommodationService;
        private OwnerRatingService _ratingService;
        private UserService _userService;


        public string CurrentThemeIcon { get => App.CurrentTheme == "Light" ? "SunIcon" : "MoonIcon"; }
        public string CurrentLanguageIcon { get => App.CurrentLanguage == "en-US" ? "EnglishIcon" : "SerbianIcon"; }
        public string RoleIcon { get => _user.Role == Domain.Models.UserRole.Owner ? "IsOwnerIcon" : "IsSuperOwnerIcon"; }
        public string RoleText
        {
            get
            {
                if (_user.Role == Domain.Models.UserRole.Owner)
                {
                    return App.CurrentLanguage == "en-US" ? "Owner" : "Vlasnik";
                }
                else
                {
                    return App.CurrentLanguage == "en-US" ? "Super Owner" : "Super Vlasnik";
                }
            }
        }
        public string Rating { get => (_user as Owner)?.Rating.ToString(); }
        public int TotalAccommodations { get; set; } = 0;
        public int TotalRatings { get; set; } = 0;
        public bool IsSuperOwner { get => _user.Role == Domain.Models.UserRole.SuperOwner; }


        public RelayCommand ChangeThemeCommand { get; }
        public RelayCommand ChangeLanguageCommand { get; }

        public OwnerAccountViewModel(User user, OwnerAccountView ownerAccountView)
        {
            _user = user;
            _ratingService = Injector.GetService<OwnerRatingService>();
            _accommodationService = Injector.GetService<AccommodationService>();
            _userService = Injector.GetService<UserService>();

            _ownerAccountView = ownerAccountView;

            ChangeThemeCommand = new RelayCommand(ChangeTheme);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);

            LoadInfo();
        }

        public void LoadInfo()
        {
            TotalAccommodations = _accommodationService.CountAllByOwnerId(_user.Id);
            TotalRatings = _ratingService.CountAllByOwnerId(_user.Id);
            OnPropertyChanged(nameof(TotalAccommodations));
            OnPropertyChanged(nameof(TotalRatings));
        }

        private void ChangeLanguage()
        {
            if (App.CurrentLanguage.Equals("en-US"))
            {
                App.ChangeLanguage("sr-LATN");
            }
            else
            {
                App.ChangeLanguage("en-US");
            }
            OnPropertyChanged(nameof(CurrentLanguageIcon));
            if (_user is Owner owner)
            {
                owner.SelectedLanguage = App.CurrentLanguage;
                _userService.UpdateOwner(owner);
            }
        }

        private void ChangeTheme()
        {
            if (App.CurrentTheme == "Light")
            {
                App.ChangeTheme("Dark");
                App.CurrentTheme = "Dark";
            }
            else
            {
                App.ChangeTheme("Light");
                App.CurrentTheme = "Light";
            }

            if (_user is Owner owner)
            {
                owner.SelectedTheme = App.CurrentTheme;
                _userService.UpdateOwner(owner);
            }

            OwnerView ownerView = new(_user, "NavBtnAccount");
            OwnerWindow ownerWindow = Window.GetWindow(_ownerAccountView) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(ownerView);
        }
    }
}
