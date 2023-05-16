using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationRenovationViewModel : ViewModelBase
    {
        private User _user;
        private readonly AccommodationRenovationService _renovationService;
        private AccommodationRenovation _renovation = new();

        public AccommodationRenovation Renovation
        {
            get => _renovation;
            set
            {
                if (value == _renovation) return;
                _renovation = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AccommodationRenovation> Renovations = new();


        public AccommodationRenovationViewModel(User user)
        {
            _user = user;
            _renovationService = Injector.GetService<AccommodationRenovationService>();

            LoadRenovations();
        }

        public void LoadRenovations()
        {
            Renovations = new(_renovationService.GetAll());
        }

        public ObservableCollection<AccommodationRenovation> LoadRenovationsByAccommodation(Accommodation accommodation)
        {
            return new ObservableCollection<AccommodationRenovation>(_renovationService.GetAllByAccommodationId(accommodation.Id));
        }
    }
}
