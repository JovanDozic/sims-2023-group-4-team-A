using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModels.TourViewModels.ComplexTourRequestsViewModels;
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
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.TourViews.GuideViews.ComplexTourRequests
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestsAcceptanceWindow.xaml
    /// </summary>
    public partial class ComplexTourRequestsAcceptanceWindow : Window
    {
        private AcceptComplexPartViewModel ViewModel;
        public ComplexTourRequestsAcceptanceWindow(AcceptComplexPartViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
            ViewModel.RequestOpen += (sender, args) => OpenCreationView();
            this.DataContext = ViewModel;
        }
        private void OpenCreationView()
        {
            var window = new TourCreation(ViewModel.NextViewModel);
            window.Show();
            this.Close();
        }
    }
}
