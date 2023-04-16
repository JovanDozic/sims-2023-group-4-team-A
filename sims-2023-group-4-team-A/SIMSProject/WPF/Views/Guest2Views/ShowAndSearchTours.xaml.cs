using SIMSProject.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels;

namespace SIMSProject.View.Guest2
{
    /// <summary>
    /// Interaction logic for ShowAndSearchTours.xaml
    /// </summary>
    /// 

    public partial class ShowAndSearchTours : Page, INotifyPropertyChanged
    {
        public Guest User = new();
        public Tour Tour { get; set; } = new();
        public Tour SelectedTour { get; set; } = new();

        private int _durationSearchBox = 0;
        public int DurationSearchBox
        {
            get => _durationSearchBox;
            set
            {
                if (value != _durationSearchBox && value >= 0)
                {
                    _durationSearchBox = value;
                    OnPropertyChanged(nameof(DurationSearchBox));
                }
            }
        }

        private int _maxGuestSearchBox = 0;
        public int MaxGuestSearchBox
        {
            get => _maxGuestSearchBox;
            set
            {
                if (value != _maxGuestSearchBox && value >= 0)
                {
                    _maxGuestSearchBox = value;
                    OnPropertyChanged(nameof(MaxGuestSearchBox));
                }
            }
        }
        public ObservableCollection<Tour> Tours { get; set; } = new();

        //private readonly TourController TourController;
        //private readonly LocationController TourLocationController = new();

        private readonly VoucherController VoucherController = new();
        private ToursViewModel _tourViewModel { get; set; }

        public ShowAndSearchTours(Guest user)
        {
            InitializeComponent();
            User = user;
            _tourViewModel = new ToursViewModel();
            this.DataContext = _tourViewModel;

            VoucherController.DeleteExpired();

            //TourController = new TourController();
            //Tours = new ObservableCollection<Tour>(TourController.GetAll());

            //Tours = new ObservableCollection<Tour>(_tourViewModel.Tours); RANDOM LINIJA PROBA

            //List<Location> tourLocations = TourLocationController.GetAll();
            //foreach (var tour in Tours)
            //{
            //    tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
            //}
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Tours.Clear();
            foreach (var tour in new ObservableCollection<Tour>
                (_tourViewModel.Tours))
                Tours.Add(tour);

            String locationAndLanguage = LocationAndLanguageSearch.Text;
            if (locationAndLanguage == "Lokacija jezik") locationAndLanguage = string.Empty;
            string[] searchValues = locationAndLanguage.Split(" ");

            int searchDuration = DurationSearch.Value <= 0 ? -1 : DurationSearch.Value;
            int searchMaxGuests = GuestSearch.Value <= 0 ? -1 : GuestSearch.Value;

            List<Tour> searchResults = Tours.ToList();
            List<Tour> fourdResults = new();

            // Removing all by location and language
            foreach(string value in searchValues)
                searchResults.RemoveAll(x=>!x.ToStringSearch().ToLower().Contains(value.ToLower()));

            // Removing by numbers
            if (searchDuration > 0) searchResults.RemoveAll(x => x.Duration != searchDuration);
            if(searchMaxGuests > 0) searchResults.RemoveAll(x=>x.MaxGuestNumber < searchMaxGuests);          

            Tours.Clear();
            foreach (var searchResult in searchResults)
                Tours.Add(searchResult);
            
        }

        private void TextSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Foreground = new SolidColorBrush(Colors.Black);
            if (textbox.Text == "Lokacija jezik") textbox.Text = string.Empty;
        }

        private void TextSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            if (textbox.Text == string.Empty)
            {
                textbox.Foreground = new SolidColorBrush(Colors.Gray);
                textbox.Text = "Lokacija jezik";

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if(_tourViewModel.SelectedTour != null)
            {
                TourReservationCreation tourReservationCreation = new TourReservationCreation(User, _tourViewModel.SelectedTour);
                tourReservationCreation.Show();
            }
            else
            {
                MessageBox.Show("Odaberite turu koju zelite da rezervisete!");
            }
        }
    }
}
