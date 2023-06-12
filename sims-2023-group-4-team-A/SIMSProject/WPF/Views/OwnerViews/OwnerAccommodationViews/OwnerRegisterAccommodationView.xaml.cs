using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerRegisterAccommodationView : Page, INavigationService
    {
        private User _user;
        private OwnerMyAccommodationsView _myAccommodationsView;
        private readonly AccommodationViewModel _viewModel;

        public OwnerRegisterAccommodationView(User user, OwnerMyAccommodationsView ownerMyAccommodationsView)
        {
            InitializeComponent();
            _user = user;

            _myAccommodationsView = ownerMyAccommodationsView;

            _viewModel = new(_user, this);
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

        private void BtnUploadImages_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UploadImageToAccommodation();
            SliderImageURLs.OnNextButtonClick(sender, e);
        }

        private void BtnCancellationHelp_Click(object sender, RoutedEventArgs e)
        {
            PopupCancellationHelp.IsOpen = true;
        }

        internal void GoBackAndReload()
        {
            _myAccommodationsView.ReloadAccommodations();
            GoBack();
        }
    }

}
