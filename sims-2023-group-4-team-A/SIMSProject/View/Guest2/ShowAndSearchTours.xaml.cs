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
            foreach (var tour in new ObservableCollection<Tour>(TourController.GetAll()))
            {
                Tours.Add(tour);
            }

            String locationAndLanguage = LocationAndLanguageSearch.Text;
            if (locationAndLanguage == "Lokacija jezik") locationAndLanguage = string.Empty;
            List<Tour> searchResults = new();
            List<string> searchList = new List<string>();

            string[] searchValues = locationAndLanguage.Split(" ");

            int searchDuration = DurationSearch.Value <= 0 ? -1 : DurationSearch.Value;
            int searchMaxGuests = GuestSearch.Value <= 0 ? -1 : GuestSearch.Value;

            foreach(string  value in searchValues)
            {
                foreach(var tour in Tours)
                {
                    if (tour.ToString().ToLower().Contains(value.ToLower()))
                    {


                        if (searchDuration == -1)
                        {
                            if (searchMaxGuests == -1)
                                searchResults.Add(tour);
                            else if (tour.MaxGuestNumber >= searchMaxGuests)
                                searchResults.Add(tour);
                        }
                        else if (tour.Duration == searchDuration)
                        {
                            if (searchMaxGuests == -1)
                                searchResults.Add(tour);
                            else if (tour.MaxGuestNumber >= searchMaxGuests)
                                searchResults.Add(tour);
                        }





                    }
                }
            }

            Tours.Clear();

            foreach (var searchResult in searchResults)
            {
                Tours.Add(searchResult);
            }

            /*
            //string searchtext = LocationSearch.Text + LanguageSearch.Text + DurationSearch.Text + GuestSearch.Text;
            //var results = TourController.search(searchtext);


            //if (LocationSearch.Text != string.Empty)
            //{
            //    Tours.Clear();
            //    foreach (var tour in TourController.SearchLocations(LocationSearch.Text))
            //    {
            //        Tours.Add(tour);
            //    }

            //    toursFilteredByLocation = Tours;

            //}

            //if (DurationSearch.Text != string.Empty)
            //{
            //    Tours.Clear();
            //    foreach (var tour in TourController.SearchDurations(DurationSearch.Text))
            //    {
            //        Tours.Add(tour);
            //    }

            //    toursFilteredByDuration = Tours;

            //}

            //if (LanguageSearch.Text != string.Empty)
            //{
            //    Tours.Clear();
            //    foreach (var tour in TourController.SearchLanguages(LanguageSearch.Text))
            //    {
            //        Tours.Add(tour);
            //    }

            //    toursFilteredByLanguage = Tours;

            //}

            //if (GuestSearch.Text != string.Empty)
            //{
            //    Tours.Clear();
            //    foreach (var tour in TourController.SearchMaxGuest(GuestSearch.Text))
            //    {
            //        Tours.Add(tour);
            //    }

            //    toursFilteredByMaxGuests = Tours;
            //}


            //Tours.Clear();
            //var commonElements = toursFilteredByLocation.Intersect(toursFilteredByDuration).Intersect(toursFilteredByLanguage).Intersect(toursFilteredByMaxGuests);
            //List<Tour> toursFlitered = commonElements.ToList();
            //foreach (var element in toursFlitered)
            //{
            //    Tours.Add(element);
            //}




            //int numguests = string.isnullorempty(guestsearch.text) ? 0 : int.parse(guestsearch.text);
            //language language = (language)enum.parse(typeof(language), languagesearch.text.tostring());
            //string location = locationsearch.text;
            //int duration = string.isnullorempty(durationsearch.text) ? 0 : int.parse(durationsearch.text);

            //searchtours(numguests, language, location, duration);
            */

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
