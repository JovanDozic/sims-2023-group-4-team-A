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
        private AcceptComplexPartViewModel ViewModel = new();
        public ComplexTourRequestsAcceptanceWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel;

        }
    }
}
