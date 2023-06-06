using SIMSProject.WPF.ViewModels.Guest2ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.TourViews.GuideViews.ComplexTourRequests
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestsPage.xaml
    /// </summary>
    public partial class ComplexTourRequestsPage : Page
    {
        private ComplexRequestViewModel ViewModel = new();
        public ComplexTourRequestsPage()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new ComplexTourRequestsAcceptanceWindow();
            window.Show();
        }
    }
}
