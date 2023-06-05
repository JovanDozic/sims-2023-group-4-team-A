using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for MyTourRequests.xaml
    /// </summary>
    public partial class MyTourRequests : Page
    {
        public Guest2 _user = new();
        private CustomTourRequestViewModel _viewModel;
        public MyTourRequests(Guest2 user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new CustomTourRequestViewModel(_user);
            this.DataContext = _viewModel;
        }

        private void NewRequest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomTourRequestCreation(_user));
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TourRequestStatistics(_user));
        }

        private void NewComplexRequest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComplexTourRequestCreation(_user));
        }

        private void ComplexTourRequestDetails_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComplexTourRequestDetails(_user, _viewModel.SelectedComplexTourRequest));
        }
    }
}
