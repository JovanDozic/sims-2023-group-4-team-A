using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerAllReschedulingRequests : Page
    {
        private User _user;
        private Accommodation _accommodation;
        private OwnerAllReschedulingRequestsViewModel _viewModel;

        public OwnerAllReschedulingRequests(User user, Accommodation accommodation)
        {
            InitializeComponent();

            _user = user;
            _accommodation = accommodation;
            _viewModel = new(_user, _accommodation);

            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void LstRequestsItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
    }
}
