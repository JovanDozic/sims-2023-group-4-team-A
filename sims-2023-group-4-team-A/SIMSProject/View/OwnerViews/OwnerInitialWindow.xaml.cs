using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace SIMSProject.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for OwnerInitialWindow.xaml
    /// </summary>
    public partial class OwnerInitialWindow : Window
    {
        public Owner User { get; set; } = new();
        public Accommodation Accommodation { get; set; } = new();

        public OwnerInitialWindow(Owner user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            Accommodation.MaxGuestNumber = 1;
        }

        private void OpenRegisterAccommodationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodation window = new();
            window.Show();
        }

        private void OpenRateGuestWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RateGuest window = new(User);
            window.Show();
        }
    }
}
