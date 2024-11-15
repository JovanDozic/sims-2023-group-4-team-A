﻿using System.Windows;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourLiveTrackingWindow.xaml
    /// </summary>
    public partial class TourLiveTrackingWindow : Window
    {
        private TourLiveTrackViewModel ViewModel { get; set; } = new();
        public TourLiveTrackingWindow()
        {
            InitializeComponent();
            ViewModel.RequestClose += (sender, args) => Close();
            this.DataContext = ViewModel;
        }
    }
}
