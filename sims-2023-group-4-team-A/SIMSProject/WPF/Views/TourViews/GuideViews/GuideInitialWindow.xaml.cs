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
        public static readonly TourController tourController = new();
        public static readonly KeyPointController keyPointController = new();
        public static readonly LocationController locationController = new();
        public static readonly TourAppointmentController tourAppointmentController = new();
        public static readonly TourGuestController tourGuestController = new();
        public static readonly TourKeyPointController tourKeyPointController = new();
        public Tour SelectedTour { get; set; } = new();
        public Guide Guide { get; set; } = new();
        public ObservableCollection<Tour> TodaysTours { get; set; } = new ObservableCollection<Tour>();


        public GuideInitialWindow(Guide guide)
        {
            InitializeComponent();
            this.Guide = guide;

            frame.Content = new AllToursPage();

        }

        private void ShowTour_Click(object sender, RoutedEventArgs e)
        {
            TodaysTourPreviewWindow window = new(SelectedTour);
            window.Show();  
        }

        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            TourCreation window = new(Guide);
            window.Show();
        }

        private void StackPanel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
