using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.Guest2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2HomeView.xaml
    /// </summary>
    public partial class Guest2HomeView : Window
    {
        public Guest User { get; set; } = new();
        public Guest2HomeView(Guest user)
        {
            InitializeComponent();
            User = user;
            SelectedTab.Content = new ShowAndSearchTours(User, string.Empty);
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            User = new Guest(0, "<null>", "<null>", DateTime.Now);
            SignInView window = new();
            window.Show();
            Close();
        }

        private void Homepage_Click(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new ShowAndSearchTours(User, string.Empty);
        }

        private void Vouchers_Click(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new VouchersDisplay(User);
        }

        private void MyReservations_Click(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new TourReservations(User);
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new Guest2NotificationView(User);
        }

        private void MyRequests_Click(object sender, RoutedEventArgs e)
        {
            SelectedTab.Content = new MyTourRequests(User);
        }
    }
}
