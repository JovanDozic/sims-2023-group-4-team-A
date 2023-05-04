﻿using SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for AllToursPage.xaml
    /// </summary>
    public partial class AllToursPage : Page
    {
        private TourManagerViewModel ViewModel { get; set; } = new();
        public AllToursPage()
        {
            InitializeComponent();
            ViewModel.GetTours();
            this.DataContext = ViewModel;
        }

        private void DetailsBTN_Click(object sender, RoutedEventArgs e)
        {
            var window = new DetailedTourWindow(ViewModel);
            window.Show();
        }
    }
}