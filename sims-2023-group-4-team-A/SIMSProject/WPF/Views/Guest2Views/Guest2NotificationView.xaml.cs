using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2NotificationView.xaml
    /// </summary>
    public partial class Guest2NotificationView : Page
    {
        private User _user { get; set; }
        private Guest2NotificationViewModel _viewModel { get; set; }

        public Guest2NotificationView(User user)
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

        private void DgrNotifications_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BtnOpenNotification_Click(sender, e);
        }
    }
}
