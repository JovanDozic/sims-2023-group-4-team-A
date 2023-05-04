using SIMSProject.Domain.Models.UserModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerWindow : Window
    {
        private User _user;

        public OwnerWindow(User user)
        {
            InitializeComponent();
            DataContext = this;

            _user = user;
            MainFrame.Navigate(new OwnerView(user));
        }
        public void SwitchToPage(Page page)
        {
            MainFrame.Navigate(page);
        }

    }
}
