using SIMSProject.Controller;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIMSProject.Controller;
using System.Collections;
using System.Diagnostics;



namespace SIMSProject.View.Guest2
{
    /// <summary>
    /// Interaction logic for ShowAndSearchTours.xaml
    /// </summary>
    /// 

    public enum TourLanguage { ENGLISH = 0, SERBIAN, SPANISH, FRENCH };

    public partial class ShowAndSearchTours : Window
    {
        public Tour Tour { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        //public ObservableCollection<TourLocation> TourLocations { get; set; }      
        //public ObservableCollection<Tour> toursFilteredByLocation { get; set; }
        //public ObservableCollection<Tour> toursFilteredByDuration { get; set; }
        //public ObservableCollection<Tour> toursFilteredByLanguage { get; set; }
        //public ObservableCollection<Tour> toursFilteredByMaxGuests { get; set; }


        private readonly TourController TourController;
        private readonly TourLocationController TourLocationController = new();



        public ShowAndSearchTours()
        {
            InitializeComponent();
            DataContext = this;
            Tour = new();
            TourController = new TourController();
            //tourController.Subscribe(this);

            Tours = new ObservableCollection<Tour>(TourController.GetAll());

            var tourLocations = TourLocationController.GetAll();

            foreach (var tour in Tours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
            }

            //Trace.WriteLine("lok " + Tours.First().LocationId + TourLocationController.GetAll().Find(x => x.Id == Tours.First().LocationId));
        }

        private void PretraziClick(object sender, RoutedEventArgs e)
        {
            Tours.Clear();
            foreach (var tour in new ObservableCollection<Tour>
                (TourController.GetAll()))
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
                searchResults.RemoveAll(x=>!x.ToString().ToLower().Contains(value.ToLower()));

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

        private void SearchTours(int numGuests, Language language, string location, int duration)
        {
            // TODO: Implement code to search for tours using the provided parameters
            List<Tour> matchingTours = new List<Tour>();

            foreach (Tour tour in Tours)
            {
                // Check if the tour matches the search criteria
                if ((numGuests == 0 || tour.MaxGuestNumber >= numGuests) &&
                    /*(language == Language.Any || tour.TourLanguage == language) &&*/
                    (string.IsNullOrEmpty(location) || tour.Location.Equals(location)) &&
                    (duration == 0 || tour.Duration <= duration))
                {
                    // Add the tour to the list of matching tours
                    matchingTours.Add(tour);
                }
            }

            Tours.Clear();
            foreach (Tour tour in matchingTours)
            {
                Tours.Add(tour);
            }

            //tours = matchingTours;

            // Display the matching tours to the user
            //ShowMatchingTours(matchingTours);
        }


    }
}
