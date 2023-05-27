using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest1ViewModels;
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

namespace SIMSProject.WPF.Views.Guest1.Pages
{
    /// <summary>
    /// Interaction logic for ReservationsStatistics.xaml
    /// </summary>
    public partial class ReservationsStatistics : Page
    {
        private readonly User _user = new();
        private readonly ReservationsStatisticsViewModel _reservationViewModel;
        public ReservationsStatistics(User user)
        {
            InitializeComponent();
            _user = user;
            _reservationViewModel = new(_user);
            DataContext = _reservationViewModel;
        }
    }
}
