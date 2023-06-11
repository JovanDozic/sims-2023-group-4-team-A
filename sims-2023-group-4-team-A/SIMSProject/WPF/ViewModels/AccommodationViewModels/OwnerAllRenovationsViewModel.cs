using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerAllRenovationsViewModel : ViewModelBase
    {
        private User _user;
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


        public OwnerAllRenovationsViewModel(User user, Accommodation accommodation)
        {
            _user = user;
            Accommodation = accommodation;

            _renovationService = Injector.GetService<AccommodationRenovationService>();

            Renovations = new(_renovationService.GetAllByAccommodationId(Accommodation.Id));
        }

        public void CancelRenovation()
        {
            MessageBox.Show((HoveredRenovation is null).ToString());
            if (HoveredRenovation is null) return;
            _renovationService.CancelRenovation(HoveredRenovation);
            OnPropertyChanged(nameof(HoveredRenovation));
        }
    }
}
