using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
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

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for ShowKeyPoint.xaml
    /// </summary>
    public partial class ShowKeyPoint : Page
    {
        private User _user;
        private readonly TourReservationsViewModel _viewmodel;
        private TourReservation TourReservation;
        public ShowKeyPoint(User user, TourReservation tourReservation)
        {
            InitializeComponent();
            _user = user;
            TourReservation = tourReservation;
            _viewmodel = new(user);
            _viewmodel.GetDetails(tourReservation);
            DataContext = _viewmodel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnGeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            _viewmodel.GeneratePDF(TourReservation);
        }
    }
}
