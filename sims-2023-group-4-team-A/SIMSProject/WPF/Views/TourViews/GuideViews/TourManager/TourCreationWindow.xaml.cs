using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels;
using System;
using System.Windows;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    ///   
    public partial class TourCreation : Window
    {
        private TourCreationViewModel ViewModel { get; set; } = new();
        public TourCreation()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
