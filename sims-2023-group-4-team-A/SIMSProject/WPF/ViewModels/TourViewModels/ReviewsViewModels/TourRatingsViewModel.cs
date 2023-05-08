using SIMSProject.Application.DTOs;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.View.Guest2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ReviewsViewModels
{
    public class TourRatingsViewModel : ViewModelBase
    {
        private readonly TourService _tourService;

        private ObservableCollection<TourRatingDTO> _ratings = new();
        public ObservableCollection<TourRatingDTO> Ratings
        {
            get => _ratings;
            set
            {
                if (value == _ratings) return;
                _ratings = value;
                OnPropertyChanged(nameof(Ratings));
            }
        }

        private ObservableCollection<DateTime> _ratedDates = new();
        public ObservableCollection<DateTime> RatedDates
        {
            get => _ratedDates;
            set
            {
                if(_ratedDates == value) return;
                _ratedDates = value;
                OnPropertyChanged(nameof(RatedDates));
            }
        }


        private TourRatingDTO _selectedRating = new();
        public TourRatingDTO SelectedRating
        {
            get => _selectedRating;
            set
            {
                if (value == _selectedRating) return;
                _selectedRating = value;
                OnPropertyChanged(nameof(SelectedRating));


                AppointmentRatings.Clear();
                AppointmentRatings = new(SelectedRating.Ratings);

                RatedDates.Clear();
                RatedDates = new(_tourService.GetRatedDatesByTour(SelectedRating));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (value == _selectedDate) return;
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private ObservableCollection<TourAppointmentRatingDTO> _appointmentRatings = new();
        public ObservableCollection<TourAppointmentRatingDTO> AppointmentRatings
        {
            get => _appointmentRatings;
            set
            {
                if(value == _appointmentRatings) return;
                _appointmentRatings = value;
                OnPropertyChanged(nameof(AppointmentRatings));
            }
        }

        private TourAppointmentRatingDTO _appointmentRating = new();
        public TourAppointmentRatingDTO AppointmentRating
        {
            get => _appointmentRating;
            set
            {
                if (value == _appointmentRating) return;
                _appointmentRating = value;
                OnPropertyChanged(nameof(AppointmentRating));
            }
        }

        private string _searchText = "Pretraži po nazivu ture";
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (value == _searchText) return;
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public TourRatingsViewModel()
        {
            _tourService = Injector.GetService<TourService>();
            Ratings = new(_tourService.GetRatings());

            SearchRatingsCommand = new RelayCommand(SearchRatingsExecuted, SearchRatingsCanExecute);
            GetAppointemntsCommand = new RelayCommand(AppointemntsExecuted, AppointemntsCanExecute);
        }

        
        public ICommand SearchRatingsCommand { get; private set; }
        public void SearchRatingsExecuted()
        {
            Ratings.Clear();
            Ratings = new(_tourService.SearchRatings(SearchText));
        }

        public bool SearchRatingsCanExecute()
        {
            return !SearchText.Equals("Pretraži po nazivu ture") && SearchText.Length > 0;
        }

        #region AppointemntsCommand
        public ICommand GetAppointemntsCommand { get; private set; }
        public void AppointemntsExecuted()
        {
            AppointmentRatings.Clear();
            AppointmentRatings = new(SelectedRating.Ratings.FindAll(x => x.User.TourAppointment.Date == SelectedDate));
        }
        public bool AppointemntsCanExecute()
        {
            return !SelectedDate.Equals(default);
        }

        #endregion

    }
}
