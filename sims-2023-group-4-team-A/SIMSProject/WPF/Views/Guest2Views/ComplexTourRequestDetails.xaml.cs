using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestDetails.xaml
    /// </summary>
    public partial class ComplexTourRequestDetails : Page
    {
        private Guest2 _user = new();
        private ComplexTourRequestViewModel _viewModel;

        public ComplexTourRequestDetails(Guest2 user, ComplexTourRequest complexTourRequest)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new ComplexTourRequestViewModel(_user, complexTourRequest);
            _viewModel.GetParts();
            this.DataContext = _viewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
