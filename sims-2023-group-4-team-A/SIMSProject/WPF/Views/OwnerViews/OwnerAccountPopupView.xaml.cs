using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.OwnerViewModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccountViews;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerAccountPopupView : Page
    {
        private readonly User _user;

        public OwnerAccountPopupView(User user)
        {
            InitializeComponent();
            _user = user;

            SetIcon();
        }

        public void SetIcon()
        {
            //IconFlag.Visibility = !_viewModel.IsSuperOwner() ? Visibility.Visible : Visibility.Collapsed;
            //IconGreenFlag.Visibility = _viewModel.IsSuperOwner() ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
