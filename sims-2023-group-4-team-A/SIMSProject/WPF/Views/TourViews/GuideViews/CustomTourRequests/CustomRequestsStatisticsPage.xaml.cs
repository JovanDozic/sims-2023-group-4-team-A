﻿using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.TourViews.GuideViews.CustomTourRequests
{
    /// <summary>
    /// Interaction logic for CustomRequestsStatisticsPage.xaml
    /// </summary>
    public partial class CustomRequestsStatisticsPage : Page
    {
        private CustomTourRequestsStatisticsViewModel ViewModel { get; set; } = new();
        public CustomRequestsStatisticsPage()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void btnCreateTour1_Click(object sender, RoutedEventArgs e)
        {
            var window = new TourCreation();
            window.Show();
        }
    }
}
