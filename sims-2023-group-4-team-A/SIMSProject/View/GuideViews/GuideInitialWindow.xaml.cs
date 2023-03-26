using SIMSProject.Controller;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using SIMSProject.Observer;
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

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for GuideInitialWindow.xaml
    /// </summary>
    public partial class GuideInitialWindow : Window, IObserver
    {
        public static readonly TourController tourController = new();
        public static readonly KeyPointController keyPointController = new();
        public static readonly LocationController locationController = new();
        public static readonly TourAppointmentController tourAppointmentController = new();
        public static readonly TourGuestController tourGuestController = new();
        public static readonly TourKeyPointController tourKeyPointController = new();
        public Tour SelectedTour { get; set; } = new();
        public Guide guide { get; set; } = new();
        public ObservableCollection<Tour> TodaysTours { get; set; } = new ObservableCollection<Tour>();


        public GuideInitialWindow(Guide guide)
        {
            InitializeComponent();
            this.DataContext = this;

            this.guide = guide;

            tourController.Subscribe(this);
            DisplayTodaysTours();
        }

        private void DisplayTodaysTours()
        {
            foreach (var tour in tourController.FindTodaysTours())
            {
                TodaysTours.Add(tour);
            }
        }

        private void ShowTour_Click(object sender, RoutedEventArgs e)
        {
            TodaysTourPreviewWindow window = new TodaysTourPreviewWindow(SelectedTour);
            window.Show();  
        }

        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            TourCreation window = new TourCreation(guide);
            window.Show();
        }

        public void Update()
        {
            UpdateTodaysTours();
        }
        private void UpdateTodaysTours()
        {
            TodaysTours.Clear();
            foreach (var tour in tourController.FindTodaysTours())
            {
                TodaysTours.Add(tour);
            }
        }

    }
}
