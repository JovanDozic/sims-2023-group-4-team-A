using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerRatingView : Window
    {
        private User _user;
        private readonly OwnerRatingViewModel _viewModel;

        public OwnerRatingView(User user, AccommodationReservation reservation)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(user, reservation);
            DataContext = _viewModel;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
