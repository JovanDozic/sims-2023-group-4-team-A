﻿using System;
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

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for FreeAccommodationsSuggestions.xaml
    /// </summary>
    public partial class FreeAccommodationsSuggestions : Window
    {
        public FreeAccommodationsSuggestions()
        {
            InitializeComponent();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
