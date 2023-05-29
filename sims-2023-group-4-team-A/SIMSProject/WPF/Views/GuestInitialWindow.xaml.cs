using System.Windows;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.Guest1;
using SIMSProject.WPF.Views.Guest2Views;
using SIMSProject.WPF.Views;

namespace SIMSProject.WPF.Views
{
    public partial class GuestInitialWindow : Window
    {
        public Guest2 User = new();

        public GuestInitialWindow(Guest2 user)
        {
            InitializeComponent();
            User = user;
        }
        private void OpenTours_Click(object sender, RoutedEventArgs e)
        {
            Guest2HomeView window = new(User);
            window.Show();
            Close();
        }

        private void Button_Click_LogOut(object sender, RoutedEventArgs e)
        {
            var window = new SignInView();
            window.Show();
            Close();
        }
    }
}