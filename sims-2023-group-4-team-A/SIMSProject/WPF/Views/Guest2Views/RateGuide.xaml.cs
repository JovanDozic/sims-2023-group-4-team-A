using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;

namespace SIMSProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for RateGuide.xaml
    /// </summary>
    public partial class RateGuide : Page
    {
        private User _user;
        private readonly GuideRatingViewModel _viewModel;
        private int _guideId;
        public RateGuide(User user, TourReservation tourReservation, int guideId)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, tourReservation);
            _guideId = guideId;
            DataContext = _viewModel;
        }

        private void BTNRate_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LeaveRating(_guideId);
            MessageBox.Show("Ocena uspesno ostavljena");
            //Close();
        }

        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddImageToGuideRating(TbImageURL.Text);
            TbImageURL.Text = string.Empty;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
