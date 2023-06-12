using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerForumViews;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerAllReschedulingRequestsView : Page
    {
        private User _user;
        private Accommodation _accommodation;
        private OwnerAllReschedulingRequestsViewModel _viewModel;

        public OwnerAllReschedulingRequestsView(User user, Accommodation accommodation)
        {
            InitializeComponent();

            _user = user;
            _accommodation = accommodation;
            _viewModel = new(_user, _accommodation);

            DataContext = _viewModel;
        }

        internal void ReloadRequests()
        {
            _viewModel.ReloadRequests();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void LstRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstRequests.SelectedItem is null) return;
            OwnerReschedulingRequestView requestView = new(_user, _viewModel.Request, this);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(requestView);
        }
    }
}
