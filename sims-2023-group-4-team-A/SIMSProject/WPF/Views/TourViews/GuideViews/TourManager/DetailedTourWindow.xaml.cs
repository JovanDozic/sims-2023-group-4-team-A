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
        private  AppointmentsViewModel ViewModel { get; set; }
        public DetailedTourWindow(Tour tour)
        {
            InitializeComponent();
            ViewModel = new(tour);
            ViewModel.GetAllAppointments();
            DataContext = ViewModel;
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CancelAppointment();
            LbAppointments.Items.Refresh();
            IsCancelEnabled();
        }


        private void IsCancelEnabled()
        {
            CancelBtn.IsEnabled = ViewModel.SelectedAppointment.TourStatus == Status.INACTIVE;
        }

        private void LbAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsCancelEnabled();
        }
    }
}
