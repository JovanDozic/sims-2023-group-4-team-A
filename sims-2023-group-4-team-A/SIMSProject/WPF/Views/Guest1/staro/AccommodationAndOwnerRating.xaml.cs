using System.Windows;
using System.Windows.Controls;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;


namespace SIMSProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerRating.xaml
    /// </summary>
    public partial class AccommodationAndOwnerRating : Window
    {
        private readonly User User = new();
        private readonly OwnerRatingViewModel _ownerRatingViewModel;
        private AccommodationReservation _accommodationReservation;

        public AccommodationAndOwnerRating(User user)
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
            if(!_ownerRatingViewModel.IsSelected())
            {
                MessageBox.Show("Morate da izaberete rezervaciju!");
                return;
            }
            _ownerRatingViewModel.RateOwnerAndAccommodation();
            MessageBox.Show("Ocena uspešno ostavljena!", "Vlasnik i smeštaj ocenjeni", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
