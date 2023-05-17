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
            _guideId = guideId;
            _viewModel = new(_user, tourReservation);
            DataContext = _viewModel;
        }

        private void BTNRate_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(TbComment.Text))
            {
                _viewModel.LeaveRating(_guideId);
                LblCommentIsRequired.Visibility = Visibility.Hidden;
                LblURLFormat.Visibility = Visibility.Hidden;
                LblURLAdded.Visibility = Visibility.Hidden;
                LblSuccessfullyRated.Visibility = Visibility.Visible;
                BtnRate.IsEnabled = false;
                return;
            }
            LblCommentIsRequired.Visibility = Visibility.Visible;
            LblURLFormat.Visibility = Visibility.Hidden;
            LblURLAdded.Visibility = Visibility.Hidden;

        }

        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            if (!TbImageURL.Text.StartsWith("https://"))
            {
                LblURLFormat.Visibility = Visibility.Visible;
                LblURLAdded.Visibility = Visibility.Hidden;
                LblCommentIsRequired.Visibility = Visibility.Hidden;
                TbImageURL.Text = string.Empty;
                return;
            }
            LblURLAdded.Visibility = Visibility.Visible;
            LblURLFormat.Visibility = Visibility.Hidden;
            LblCommentIsRequired.Visibility = Visibility.Hidden;
            _viewModel.AddImageToGuideRating(TbImageURL.Text);
            TbImageURL.Text = string.Empty;
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
