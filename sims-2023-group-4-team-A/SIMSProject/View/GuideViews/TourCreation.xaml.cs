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
using SIMSProject.Domain.Models.UserModels;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    ///   
    public partial class TourCreation : Window, INotifyPropertyChanged
    {

        private int keyPointCounter = 0;

        private Location? _selectedLocation;
        private bool _imageAdded;

        public Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                if (value != _selectedLocation)
                {
                    _selectedLocation = value;

                    KeyPoints.Clear();
                    foreach (var point in GuideInitialWindow.keyPointController.GetAll().FindAll(x => x.LocationId == value.Id))
                    {
                        KeyPoints.Add(point);
                    }
                    OnPropertyChanged(nameof(SelectedLocation));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Tour New { get; set; } = new();
        public KeyPoint? SelectedKeyPoint { get; set; }
        public DateTime SelectedAppointment { get; set; } = DateTime.Now;
        public Guide Guide { get; set; } = new();
        public List<KeyPoint> NewKeyPoints { get; set; } = new();
        public List<TourAppointment> NewAppointments { get; set; } = new();
        public List<KeyPoint> KeyPoints { get; set; } = new();
        public List<string> TourLanguages { get; set; } = new();
        public List<Location> Locations { get; set; } = new();

        public TourCreation(Guide guide)
        {
            InitializeComponent();
            this.DataContext = this;

            Guide = guide;
            TourLanguages = new()
            {
                "Srpski",
                "Engleski",
                "Francuski",
                "Španski"
            };

            KeyPoints = new(GuideInitialWindow.keyPointController.GetAll());
            Locations = new(GuideInitialWindow.locationController.GetAll());

        }


        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (keyPointCounter < 2)
            {
                MessageBox.Show("Morate uneti najmanje 2 ključne tačke!");
                return;
            }
            else if (New.Images.Count == 0)
            {
                MessageBox.Show("Morate dodati bar 1 sliku.");
                return;
            }

            New.KeyPoints = NewKeyPoints;
            New.TourLanguage = (string)LanguageCombo.SelectedItem;

            New.Guide = Guide;
            New.GuideId = Guide.Id;
            New.Location = SelectedLocation;
            New.LocationId = SelectedLocation.Id;

            Tour createdTour = GuideInitialWindow.tourController.Create(New);

            CreateAndAssignTourAppointment(createdTour);
            CreateNewPairs(createdTour);

            GuideInitialWindow.tourController.Refresh();
            MessageBox.Show("Tura uspešno kreirana.");
            Close();
        }

        private void CreateNewPairs(Tour createdTour)
        {
            foreach (var keyPoint in NewKeyPoints)
            {
                TourKeyPoint newPair = new(createdTour.Id, keyPoint.Id);
                GuideInitialWindow.tourKeyPointController.Create(newPair);
            }
        }

        private void CreateAndAssignTourAppointment(Tour createdTour)
        {
            foreach (var appointment in NewAppointments)
            {
                TourAppointment createdAppointment = GuideInitialWindow.tourAppointmentController.Create(appointment);
                TourAppointment updatedAppointment = GuideInitialWindow.tourAppointmentController.InitializeTour(createdAppointment, createdTour);
                GuideInitialWindow.tourController.AddAppointment(createdTour.Id, updatedAppointment);
            }
        }

        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedKeyPoint == null)
            {
                MessageBox.Show("Nije moguće izabrati ključnu tačku koja ne postoji!");
                return;
            }
            NewKeyPoints.Add(SelectedKeyPoint);
            keyPointCounter++;
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            DateTime newAppointment = CreateAppointment();
            NewAppointments.Add(new(-1, newAppointment, -1, 0, -1));
        }

        private DateTime CreateAppointment()
        {
            string[] timeParts = TBTime.Text.Split(":");
            int hours = int.Parse(timeParts[0]);
            int minutes = int.Parse(timeParts[1]);
            int seconds = 0;

            TBTime.Text = "hh:mm";
            DateTime newDate = new DateTime(SelectedAppointment.Year, SelectedAppointment.Month, SelectedAppointment.Day, hours, minutes, seconds);
            return newDate;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ImageURLs_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_imageAdded)
            {
                _imageAdded = false;
                ImageURLs.Text = string.Empty;
            }
        }

        private void BTNUploadFiles_Click(object sender, RoutedEventArgs e)
        {
            New.Images.Add(ImageURLs.Text);
            _imageAdded = true;
        }
    }
}
