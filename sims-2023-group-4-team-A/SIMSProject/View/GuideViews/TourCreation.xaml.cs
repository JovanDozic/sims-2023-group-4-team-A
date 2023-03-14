using SIMSProject.Controller;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    ///   
    public partial class TourCreation : Window, INotifyPropertyChanged
    {

        private static int keyPointCounter = 0;
        public TourController tourController { get; set; } = new();
        public KeyPointController keyPointController { get; set; } = new();
        public TourDateController tourDateController { get; set; } = new();
        public LocationController tourLocationController { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Tour New { get; set; }
        public KeyPoint? SelectedKeyPoint { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public string SelectedTime { get; set; } = "hh:mm";


        private Location? _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                if(value != _selectedLocation)
                {
                    _selectedLocation = value;
                    
                    KeyPoints.Clear();
                    foreach(var point in keyPointController.GetAll().FindAll(x => x.LocationId == value.Id))
                    {
                        KeyPoints.Add(point);
                    }
                    OnPropertyChanged(nameof(SelectedLocation));
                }
            }
        }
        public string Images { get; set; } = "slika1.png,slika2.jpg...";
        public User Guide { get; set; }

        public List<KeyPoint> NewKeyPoints { get; set; } = new();
        public List<TourDate> NewDates { get; set; } = new();
        public ObservableCollection<KeyPoint> KeyPoints { get; set; } = new();
        public ObservableCollection<string> TourLanguages { get; set; } = new();
        public ObservableCollection<Location> Locations { get; set; } = new();

        public TourCreation()
        {
            InitializeComponent();
            this.DataContext = this;

            New = new Tour();

            Guide = new User("Admin", "Admin", USER_ROLE.GUIDE);
            Guide.Id = 256;

            TourLanguages = new()
            {
                "Srpski",
                "Engleski",
                "Francuski",
                "Španski"
            };

            KeyPoints = new(keyPointController.GetAll());
            Locations = new(tourLocationController.GetAll());

        }

        private List<string> SeperateURLs()
        {
            List<string> seperatedURLS = new List<string>();
            string[] urls = Images.Split(",");
            seperatedURLS.AddRange(urls);
            return seperatedURLS;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLocation != null && keyPointCounter >= 2)
            {
                
                New.KeyPoints = NewKeyPoints;
                New.Dates = NewDates;
                New.AvailableSpots = New.MaxGuestNumber;
                
                New.TourLanguage = (string)LanguageCombo.SelectedItem;
                
                New.Guide = Guide;
                New.GuideId = Guide.Id;
                New.Location = SelectedLocation;
                New.LocationId = SelectedLocation.Id;

                List<string> images = SeperateURLs();
                New.Images = images;

                keyPointCounter = 0;

                tourController.Create(New);
                MessageBox.Show("Tura uspešno kreirana.");
                Close();
                
            }
            else
            {
                MessageBox.Show("Morate uneti najmanje 2 ključne tačke!");
            }            

        }

        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedKeyPoint == null)
            {
                MessageBox.Show("Nije moguće izabrati ključnu tačku koja ne postoji!");
                return;
            }
            NewKeyPoints.Add(SelectedKeyPoint);
            keyPointCounter++;
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            string[] timeParts = SelectedTime.Split(":");
            int hours = int.Parse(timeParts[0]);
            int minutes = int.Parse(timeParts[1]);
            int seconds = 0;

            DateTime newDate = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, hours, minutes, seconds);
            NewDates.Add(new(-1, SelectedDate, -1));
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
