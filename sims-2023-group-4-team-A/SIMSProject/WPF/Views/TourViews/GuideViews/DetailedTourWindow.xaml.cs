using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModels.TourViewModels;
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

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for DetailedTourView.xaml
    /// </summary>
    public partial class DetailedTourWindow : Window
    {
        private  AppointmentsViewModel _viewModel { get; set; }
        public DetailedTourWindow(Tour tour)
        {
            InitializeComponent();
            _viewModel = new(tour);
            _viewModel.GetAllAppointments();
            DataContext = _viewModel;
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelAppointment();
            LbAppointments.Items.Refresh();
        }

        private void LbAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CancelBtn.IsEnabled = _viewModel.SelectedAppointment.TourStatus == Status.INACTIVE ? true : false;
        }
    }
}
