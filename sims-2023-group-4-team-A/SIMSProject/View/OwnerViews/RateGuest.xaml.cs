using SIMSProject.Model;
using SIMSProject.Model.UserModel;
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

namespace SIMSProject.View.OwnerViews
{
    public partial class RateGuest : Window
    {
        public Owner User { get; set; } = new();
        public GuestRating GuestRating { get; set; } = new();

        public RateGuest(Owner user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;

        }

        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
