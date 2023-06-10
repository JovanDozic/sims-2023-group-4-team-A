using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerHomeView : Page
    {
        private User _user;
        public NotificationViewModel NotificationViewModel { get; set; }
        public AccommodationViewModel AccommodationViewModel { get; set; }


        public OwnerHomeView(User user)
        {
            InitializeComponent();
            _user = user;

            NotificationViewModel = new(_user, true);
            AccommodationViewModel = new(_user);
            DataContext = this;

            AccommodationViewModel.LoadFirstTwoAccommodationsByOwner();
        }

        private void BtnShowAllAccommodations_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (Window.GetWindow(this) as OwnerWindow).SwitchToPage(new OwnerView(_user).NavigateToPage("NavBtnAccommodations") as Page);
        }

        private void BtnShowAllNotifications_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (Window.GetWindow(this) as OwnerWindow).SwitchToPage(new OwnerView(_user).NavigateToPage("NavBtnNotifications") as Page);
        }
    }
}
