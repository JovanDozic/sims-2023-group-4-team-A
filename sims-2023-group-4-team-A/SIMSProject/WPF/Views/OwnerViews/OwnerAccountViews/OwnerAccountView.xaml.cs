using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.OwnerViewModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccountViews
{
    public partial class OwnerAccountView : Page
    {
        private User _user;
        private OwnerAccountViewModel _viewModel;

        public OwnerAccountView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, this);
            DataContext = _viewModel;
        }
    }
}
