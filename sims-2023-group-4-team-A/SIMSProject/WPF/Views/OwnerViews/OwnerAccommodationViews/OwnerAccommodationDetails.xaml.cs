using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerAccommodationDetails : Page, INavigationService
    {
        private User _user;
        private readonly AccommodationDetailsViewModel _viewModel;

        public OwnerAccommodationDetails(User user, Accommodation accommodation)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, accommodation);
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        public void GoBack()
        {
            NavigationService?.GoBack();
        }
    }
}
