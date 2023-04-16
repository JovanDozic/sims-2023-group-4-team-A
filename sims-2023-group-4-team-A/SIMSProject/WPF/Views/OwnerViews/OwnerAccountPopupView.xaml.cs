using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.OwnerViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerAccountPopupView : Page
    {
        private readonly User _user;
        private readonly OwnerAccountViewModel _viewModel;

        public OwnerAccountPopupView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            DataContext = _viewModel;

            SetIcon();
        }

        public void SetIcon()
        {
            IconFlag.Visibility = !_viewModel.IsSuperOwner() ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            IconGreenFlag.Visibility = _viewModel.IsSuperOwner() ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
    }
}
