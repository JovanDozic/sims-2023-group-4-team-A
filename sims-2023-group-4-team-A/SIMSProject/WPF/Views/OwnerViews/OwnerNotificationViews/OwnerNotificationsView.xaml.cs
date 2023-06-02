using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerNotificationViews
{
    public partial class OwnerNotificationsView : Page
    {
        private User _user;
        private NotificationViewModel _viewModel;

        public OwnerNotificationsView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            DataContext = _viewModel;
        }

        private void LstNotifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstNotifications.SelectedItem is null) return;
            OwnerNotificationView notificationView = new(_viewModel);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(notificationView);
        }
    }
}
