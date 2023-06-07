using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerForumViews
{
    public partial class OwnerLocationForumsView : Page
    {
        private User _user;
        private OwnerLocationForumsViewModel _viewModel;

        public OwnerLocationForumsView(User user, Location location)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, location);
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void LstForums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstForums.SelectedItem is null) return;
            OwnerForumView forumView = new(_user, _viewModel.Forum);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(forumView);
        }

        private void TextBlock_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MessageBox.Show(_viewModel.Forums.First().ToString());
        }

        private void Page_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LstForums.Items.Refresh();
        }
    }
}
