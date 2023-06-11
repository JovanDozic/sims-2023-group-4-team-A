using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerAllRenovationsViewModel : ViewModelBase
    {
        private User _user;
        private OwnerAccommodationDetails _detailsView;
        private AccommodationRenovationService _renovationService;
        private AccommodationRenovation? _hoveredRenovation;

        public Accommodation Accommodation { get; set; }
        public ObservableCollection<AccommodationRenovation> Renovations { get; set; } = new();
        public AccommodationRenovation? HoveredRenovation
        {
            get => _hoveredRenovation;
            set
            {
                if (_hoveredRenovation == value) return;
                _hoveredRenovation = value;
                OnPropertyChanged(nameof(HoveredRenovation));
            }
        }


        public OwnerAllRenovationsViewModel(User user, Accommodation accommodation, OwnerAccommodationDetails detailsView)
        {
            _user = user;
            _detailsView = detailsView;
            Accommodation = accommodation;

            _renovationService = Injector.GetService<AccommodationRenovationService>();

            Renovations = new(_renovationService.GetAllByAccommodationId(Accommodation.Id));
        }

        public void CancelRenovation()
        {
            if (HoveredRenovation is null) return;
            _renovationService.CancelRenovation(HoveredRenovation);
            OnPropertyChanged(nameof(HoveredRenovation));
            _detailsView.ReloadRenovations();
        }
    }
}
