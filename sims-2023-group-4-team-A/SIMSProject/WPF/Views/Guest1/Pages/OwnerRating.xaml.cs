using SIMSProject.Domain.Models.AccommodationModels;
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
    /// Interaction logic for OwnerRating.xaml
    /// </summary>
    public partial class OwnerRating : Page
    {
        private readonly User User = new();
        private readonly OwnerRatingViewModel _ownerRatingViewModel;
        private AccommodationReservation _accommodationReservation;

        public OwnerRating(User user)
        {
            InitializeComponent();
            User = user;
            _ownerRatingViewModel = new(User, _accommodationReservation);
            DataContext = _ownerRatingViewModel;
            _ownerRatingViewModel.AddReservationsToCombo();
        }
        private void Button_Click_Upload(object sender, RoutedEventArgs e)
        {
            _ownerRatingViewModel.UploadImage(ImageUrlTB.Text);
            ImageUrlTB.Text = string.Empty;
            ImagesList.Items.Refresh();
        }

        private void Button_Click_Rate(object sender, RoutedEventArgs e)
        {
            _ownerRatingViewModel.RateOwnerAndAccommodation();
            MessageBox.Show("Ocena uspešno ostavljena!", "Vlasnik i smeštaj ocenjeni", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();

        }

        private void ReservationsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonRate.IsEnabled = _ownerRatingViewModel.IsSelected();
        }
    }
}
