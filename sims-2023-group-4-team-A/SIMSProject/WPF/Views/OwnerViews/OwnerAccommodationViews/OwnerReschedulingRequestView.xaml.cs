using Microsoft.TeamFoundation.Test.WebApi;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerReschedulingRequestView : Page
    {
        private User _user;
        private OwnerAllReschedulingRequestsView _allView;
        private OwnerReschedulingRequestViewModel _viewModel;

        public OwnerReschedulingRequestView(User user, ReschedulingRequest request, OwnerAllReschedulingRequestsView allView)
        {
            InitializeComponent();

            _user = user;
            _allView = allView;
            _viewModel = new(_user, request, this);

            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        public void GoBackAndReload()
        {
            NavigationService?.GoBack();
            _allView.ReloadRequests();
        }
    }
}
