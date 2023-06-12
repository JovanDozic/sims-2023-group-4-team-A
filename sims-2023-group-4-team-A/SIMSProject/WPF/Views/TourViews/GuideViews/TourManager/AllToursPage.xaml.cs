using SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for AllToursPage.xaml
    /// </summary>
    public partial class AllToursPage : Page
    {
        private ToursManagerViewModel ViewModel { get; set; } = new("AllTours");
        public AllToursPage()
        {
            InitializeComponent();
            ViewModel.RequestOpen += (sender, args) => OpenDetails();
            this.DataContext = ViewModel;
        }

        private void OpenDetails()
        {
            var window = new DetailedTourWindow(ViewModel.NextViewModel);
            window.Show();
        }

        //private void DetailsBTN_Click(object sender, RoutedEventArgs e)
        //{
        //    var window = new DetailedTourWindow(ViewModel.NextViewModel);
        //    window.Show();
        //}
    }
}
