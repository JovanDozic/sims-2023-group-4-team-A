using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationViewModel : INotifyPropertyChanged
    {
        private Owner _owner;
        private readonly AccommodationService _accommodationService;
        public ObservableCollection<Accommodation> Accommodations = new();

        public AccommodationViewModel(Owner owner)
        {
            _owner = owner;
            _accommodationService = Injector.GetService<AccommodationService>();
            LoadAccommodations();
        }

        public void LoadAccommodations()
        {
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllByOwnerId(_owner.Id));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

