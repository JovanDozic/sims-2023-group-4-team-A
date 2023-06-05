using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerForumViews
{
    public partial class OwnerAllForumsView : Page
    {
        private User _user;
        private OwnerForumLocationViewModel _viewModel;

        public OwnerAllForumsView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            DataContext = _viewModel;
        }
    }
}
