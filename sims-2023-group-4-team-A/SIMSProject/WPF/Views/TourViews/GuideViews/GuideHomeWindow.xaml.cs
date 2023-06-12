using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels;
using SIMSProject.WPF.Views;
using SIMSProject.WPF.Views.TourViews.GuideViews;
using System.Windows;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for GuideHomeWindow.xaml
    /// </summary>
    public partial class GuideHomeWindow : Window
    {
        private GuideHomeViewModel ViewModel { get; set; }
        public GuideHomeWindow(Guide guide)
        {
            InitializeComponent();
            ViewModel = new(guide, this.mainFrame.NavigationService);
            mainFrame.Content = new AllToursPage();
            this.DataContext = ViewModel;
        }
        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            TourCreation window = new();
            window.Show();
        }
        private void Sign_outBTN_Click(object sender, RoutedEventArgs e)
        {
            var window = new SignInView();
            window.Show();
            this.Close();
        }
    }
}
