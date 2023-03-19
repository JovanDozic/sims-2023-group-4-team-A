using SIMSProject.Model.UserModel;
using SIMSProject.View.Guest1;
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

namespace SIMSProject.View
{
    /// <summary>
    /// Interaction logic for GuestInitialWindow.xaml
    /// </summary>
    public partial class GuestInitialWindow : Window
    {
        public Guest User = new();
        public GuestInitialWindow(Guest user)
        {
            InitializeComponent();
            User = user;
        }

        private void OpenAccommodations_Click(object sender, RoutedEventArgs e)
        {
            AccommodationSearchAndShowForm window = new(User);
            window.Show();
        }

        private void OpenTours_Click(object sender, RoutedEventArgs e)
        {
            ShowAndSearchTours window = new();
            window.Show();
        }
    }
}
