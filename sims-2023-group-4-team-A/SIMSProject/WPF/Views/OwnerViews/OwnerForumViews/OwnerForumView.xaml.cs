using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerForumViews
{
    public partial class OwnerForumView : Page
    {
        private User _user;
        private OwnerForumViewModel _viewModel;

        public OwnerForumView(User user, Forum forum)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, forum);
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void BtnDownvoteComment_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BtnAddComment_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
