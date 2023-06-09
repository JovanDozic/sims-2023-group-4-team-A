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
        private Guest2NotificationViewModel _viewModel { get; set; }

        public Guest2NotificationView(User user)
        {
            InitializeComponent();
            _viewModel = new(user);
            this.DataContext = _viewModel;
        }

        private void DgrNotifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SetButtonsState();
        }
    }
}
