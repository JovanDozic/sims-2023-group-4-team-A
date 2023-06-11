using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for TourReservations.xaml
    /// </summary>
    public partial class TourReservations : Page
    {
        public TourReservations(Guest2 user, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new TourReservationsViewModel(user, navigationService);
        }
        
    }
}