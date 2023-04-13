using SIMSProject.Controller;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model.UserModel;
using SIMSProject.Observer;
using SIMSProject.WPF.Views.TourViews.GuideViews;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for GuideInitialWindow.xaml
    /// </summary>
    public partial class GuideInitialWindow : Window
    {

        public static LocationController LocationController { get; set; } = new();
        public static KeyPointController KeyPointController { get; set; } = new();
        public static TourGuestController TourGuestController { get; set; } = new();    


        public Tour SelectedTour { get; set; } = new();
        public Guide Guide { get; set; } = new();
        public ObservableCollection<Tour> TodaysTours { get; set; } = new ObservableCollection<Tour>();


        public GuideInitialWindow(Guide guide)
        {
            InitializeComponent();
            this.Guide = guide;

            frame.Content = new AllToursPage();

        }

        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            TourCreation window = new(Guide);
            window.Show();
        }

        private void TodaysBTN_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new TodaysToursPage();
        }

        private void AllBTN_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new AllToursPage();
        }
    }
}
