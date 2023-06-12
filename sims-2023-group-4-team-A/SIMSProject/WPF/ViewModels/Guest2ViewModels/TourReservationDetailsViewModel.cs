using SIMSProject.Application.Services;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.ObjectModel;
using SIMSProject.Domain.Models.UserModels;
using System.Windows.Navigation;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class TourReservationDetailsViewModel : ViewModelBase
    {
        #region Polja
        private readonly User _user;
        public ObservableCollection<TourReservation> _tourReservations = new();
        public ObservableCollection<TourReservation> TourReservations
        {
            get => _tourReservations;
            set
            {
                if (value == _tourReservations) return;
                _tourReservations = value;
                OnPropertyChanged();
            }
        }
        private TourReservation _selectedTourReservation = new();
        public TourReservation SelectedTourReservation
        {
            get => _selectedTourReservation;
            set
            {
                if (value == _selectedTourReservation) return;
                _selectedTourReservation = value;
                OnPropertyChanged(nameof(SelectedTourReservation));
            }
        }
        private TourReservation _tourReservation = new();
        public TourReservation TourReservation
        {
            get => _tourReservation;
            set
            {
                if (value == _tourReservation) return;
                _tourReservation = value;
                OnPropertyChanged(nameof(TourReservation));
            }
        }
        public DateTime Date
        {
            get => _selectedTourReservation.TourAppointment.Date;
            set
            {
                if (value != _selectedTourReservation.TourAppointment.Date)
                {
                    _selectedTourReservation.TourAppointment.Date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        public NavigationService NavService { get; set; }
        public RelayCommand GeneratePDFCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        #endregion

        #region Konstruktori
        public TourReservationDetailsViewModel(User user, TourReservation tourReservation, NavigationService navigationService)
        {
            _user = user;
            NavService = navigationService;
            TourReservation = tourReservation;
            GeneratePDFCommand = new RelayCommand(GeneratePDF);

            GoBackCommand = new RelayCommand(GoBackExecute, CanExecute_Command);
        }
        #endregion

        #region Akcije
        public void GeneratePDF()
        {
            PDFService.GenerateTourReservationDetailsPDF(TourReservation);
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
