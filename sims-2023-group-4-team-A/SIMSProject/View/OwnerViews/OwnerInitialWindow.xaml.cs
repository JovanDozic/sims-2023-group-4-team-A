using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SIMSProject.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for OwnerInitialWindow.xaml
    /// </summary>
    public partial class OwnerInitialWindow : Window
    {
        public OwnerInitialWindow()
        {
            InitializeComponent();
        }

        private void OpenRegisterAccommodationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodation window = new();
            window.Show();
        }
    }
}
