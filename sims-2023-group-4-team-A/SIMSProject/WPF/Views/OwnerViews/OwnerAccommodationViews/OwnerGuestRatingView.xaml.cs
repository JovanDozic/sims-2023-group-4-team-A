using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerGuestRatingView : Page
    {
        private User _user;
        private OwnerGuestRatingViewModel _viewModel;

        public OwnerGuestRatingView(User user, AccommodationReservation reservation)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(user, reservation, this);
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
