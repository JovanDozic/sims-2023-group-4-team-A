using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerForumViews
{
    public partial class OwnerForumLocationsView : Page
    {
        private User _user;
        private OwnerForumLocationsViewModel _viewModel;

        public OwnerForumLocationsView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            DataContext = _viewModel;
        }

        private void LstLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstLocations.SelectedItem is null) return;
            OwnerLocationForumsView forumsView = new(_user, _viewModel.Location);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(forumsView);
        }
    }
}
