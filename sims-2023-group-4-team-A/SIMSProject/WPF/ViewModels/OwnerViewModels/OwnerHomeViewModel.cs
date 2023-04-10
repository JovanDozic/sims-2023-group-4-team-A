using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerHomeViewModel : INotifyPropertyChanged
    {
        private readonly Owner _owner;
        public Accommodation SelectedAccommodation { get; set; } = new();
        private readonly AccommodationViewModel _accommodationViewModel;

        //private AccommodationReservation _selectedReservation = new();
        //private readonly AccommodationReservationViewModel _accommodationReservationViewModel;

        public ObservableCollection<Accommodation> Accommodations => _accommodationViewModel.Accommodations;

        public OwnerHomeViewModel(Owner owner)
        {
            _owner = owner;
            _accommodationViewModel = new(_owner);
        }

        //public void DisplaySelected()
        //{
        //    MessageBox.Show("Selektovano: " + SelectedAccommodation.Id + " " + SelectedAccommodation.Name, "");
        //}



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
