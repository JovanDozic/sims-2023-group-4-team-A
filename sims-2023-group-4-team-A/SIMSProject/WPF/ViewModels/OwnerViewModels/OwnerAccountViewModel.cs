using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;

namespace SIMSProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerAccountViewModel : ViewModelBase
    {
        private readonly User _user;
        private readonly OwnerRatingService _ratingService;
        private readonly AccommodationService _accommodationService;
        private int _totalAccommodations = 0;
        private int _totalRatings = 0;

        public string DisplayRole => User.GetRole(_user.Role);
        public string DisplayRating => (_user is Owner owner) ? owner.Rating.ToString("N2") : "<null>";
        public string DisplayTotalInfo => $"{_totalAccommodations} Smeštaja - {_totalRatings} Ocena";
        public string DisplayRemaining
        {
            get
            {
                if (_user.Role == UserRole.Owner && _totalRatings < Consts.SuperOwnerMinimumRatingCount)
                    return $"Nedostaje vam još {Consts.SuperOwnerMinimumRatingCount - _totalRatings} ocena do statusa Super Vlasnik!";
                else if (_user.Role == UserRole.Owner)
                    return "Potrebna vam je ocena veća od 4.5 za status Super Vlasnik";
                return string.Empty;
            }
        }

        public OwnerAccountViewModel(User user)
        {
            _user = user;
            _ratingService = Injector.GetService<OwnerRatingService>();
            _accommodationService = Injector.GetService<AccommodationService>();

            LoadInfo();
        }

        public void LoadInfo()
        {
            _totalAccommodations = _accommodationService.CountAllByOwnerId(_user.Id);
            _totalRatings = _ratingService.CountAllByOwnerId(_user.Id);
        }

        public bool IsSuperOwner()
        {
            return _ratingService.IsSuperOwner(_user);
        }
    }
}
