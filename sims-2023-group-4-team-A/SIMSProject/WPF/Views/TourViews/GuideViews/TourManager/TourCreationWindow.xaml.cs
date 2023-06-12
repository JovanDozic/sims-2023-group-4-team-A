using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    ///   
    public partial class TourCreation : Window
    {
        private TourCreationViewModel ViewModel;
        public TourCreation(TourCreationViewModel viewModel)
        {
            InitializeComponent();
            PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) this.Close(); };
            this.ViewModel = viewModel;
            this.DataContext = ViewModel;
        }
    }
}
