using SIMSProject.Domain.Models.UserModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

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

        private void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            // Start the fade animation
            Storyboard storyboard = (Storyboard)FindResource("FadeAnimation");
            storyboard.Begin(MainFrame);

            // Prevent the default navigation behavior
            //e.Cancel = true;
            
        }
    }
}
