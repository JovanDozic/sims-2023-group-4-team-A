using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
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
    /// Interaction logic for GuestRatings.xaml
    /// </summary>
    public partial class GuestRatings : Page
    {
        private readonly User _user = new();
        private AccommodationReservationViewModel _accommodationReservationViewModel;
        public GuestRatings(User _user)
        {
            InitializeComponent();
            _accommodationReservationViewModel = new(_user);
            DataContext = _accommodationReservationViewModel;
            _accommodationReservationViewModel.AddReservationsToCombo();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _accommodationReservationViewModel.GetOverallRating();
            if(AccommodationsCombo.SelectedItem != null)
            {
                Details.Visibility = Visibility.Visible;
                OutOfFive.Visibility = Visibility.Visible;
            }
        }
    }
}
