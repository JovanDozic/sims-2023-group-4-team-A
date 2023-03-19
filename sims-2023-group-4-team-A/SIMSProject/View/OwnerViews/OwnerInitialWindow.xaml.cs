using SIMSProject.Model;
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
        public Accommodation Accommodation { get; set; } = new();

        public OwnerInitialWindow()
        {
            InitializeComponent();
            DataContext = this;
            Accommodation.MaxGuestNumber = 1;
        }

        private void OpenRegisterAccommodationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodation window = new();
            window.Show();
        }

        private void OpenRateGuestWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RateGuest window = new();
            window.Show();
        }
    }
}
