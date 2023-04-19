using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.OwnerViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerNotificationView : Page
    {
        private User _user { get; set; }
        private OwnerNotificationViewModel _viewModel { get; set; }

        public OwnerNotificationView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            DataContext = _viewModel;
        }

        private void BtnOpenNotification_Click(object sender, RoutedEventArgs e)
        {
            var notification = _viewModel.SelectedNotification;
            if (notification == null) return;

            if (MessageBox.Show(notification.Description, notification.Title, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                return;

            _viewModel.MarkAsRead();
            DgrNotifications.Items.Refresh();
        }

        private void DgrNotifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnOpenNotification.IsEnabled = _viewModel.SelectedNotification != null;
        }
    }
}
