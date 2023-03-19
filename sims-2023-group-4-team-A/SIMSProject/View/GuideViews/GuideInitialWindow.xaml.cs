using SIMSProject.Controller;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
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
    public partial class GuideInitialWindow : Window
    {
        private readonly TourController tourController = new();
        public ObservableCollection<Tour> TodaysTours { get; set; }
        public Tour SelectedTour { get; set; } = new();
        public Guide guide { get; set; } = new();

        public GuideInitialWindow(Guide guide)
        {
            InitializeComponent();
            this.DataContext = this;

            TodaysTours = new();
            this.guide = guide;

            DisplayTodaysTours();
        }

        private void DisplayTodaysTours()
        {
            foreach (var tour in tourController.FindTodays())
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
    }
}
