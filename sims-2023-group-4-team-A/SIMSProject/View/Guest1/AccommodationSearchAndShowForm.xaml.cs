using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMSProject.Model;
using SIMSProject.Observer;
using SIMSProject.Controller;
using System.Collections.ObjectModel;
using SIMSProject.View.Guest1;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using SIMSProject.Model.UserModel;

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationSearchAndShowForm.xaml
    /// </summary>
    public partial class AccommodationSearchAndShowForm : Window, IObserver, INotifyPropertyChanged
    {
        public Guest User = new();
        public Accommodation Accommodation { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private AccommodationController AccommodationControllers;
        public event PropertyChangedEventHandler? PropertyChanged;
        public Accommodation? SelectedAccommodation { set; get; } = null;

        private int _durationSearch;
        public int DurationSearch
        {
            get => _durationSearch;

            set
            {
                if(value != _durationSearch && value >= 0)
                {
                    _durationSearch = value;
                    OnPropertyChanged(nameof(DurationSearch));
                }
            }
        }
        private int _maxGuestsSearch;
        public int MaxGuestsSearch
        {
            get => _maxGuestsSearch;

            set
            {
                if(value != _maxGuestsSearch && value >= 0)
                {
                    _maxGuestsSearch = value;
                    OnPropertyChanged(nameof(MaxGuestsSearch));
                }
            }
        }
        public AccommodationSearchAndShowForm(Guest user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            Accommodation = new();
            AccommodationControllers = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(AccommodationControllers.GetAll());
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            Accommodations.Clear();
            foreach (var accommodations in new ObservableCollection<Accommodation>(AccommodationControllers.GetAll()))
                Accommodations.Add(accommodations);

            String search1 = TextSearch1.Text;
            if (search1 == "Naziv Tip Lokacija") search1 = string.Empty;
            string[] searchValues = search1.Split(" ");

            List<Accommodation> searchResults = Accommodations.ToList();

            // Removing all by name, location and type
            foreach (string value in searchValues)
                searchResults.RemoveAll(x => !x.ToString().ToLower().Contains(value.ToLower()));

            // Removing by numbers
            if (DurationSearch > 0) searchResults.RemoveAll(x => x.MinReservationDays > DurationSearch);
            if (MaxGuestsSearch > 0) searchResults.RemoveAll(x => x.MaxGuestNumber < MaxGuestsSearch);

            Accommodations.Clear();
            foreach (var searchResult in searchResults)
                Accommodations.Add(searchResult);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        private void TextSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Foreground = new SolidColorBrush(Colors.Black);
            if (textbox.Text == "Naziv Tip Lokacija") textbox.Text = string.Empty;
        }

        private void TextSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            if (textbox.Text == string.Empty)
            {
                textbox.Foreground = new SolidColorBrush(Colors.Gray);
                textbox.Text = "Naziv Tip Lokacija";

            }
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            
            if(SelectedAccommodation != null)
            {
                var openReservation = new AccommodationReview(User, SelectedAccommodation);
                openReservation.Show();
            }
            else
            {
                MessageBox.Show("Izaberite smeštaj");
            }
            
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Reservations(object sender, RoutedEventArgs e)
        {
            var openReservations = new AccommodationReservationList();
            openReservations.Show();
        }
    }
}
