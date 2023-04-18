using System.Windows;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;

namespace SIMSProject.View.OwnerViews
{
    public partial class RegisterAccommodationView : Window
    {
        private User _user;
        private AccommodationViewModel _accommodationViewModel { get; set; }

        public RegisterAccommodationView(User user)
        {
            InitializeComponent();
            _user = user;

            _accommodationViewModel = new(_user);
            DataContext = _accommodationViewModel;
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            _accommodationViewModel.RegisterAccommodation();
            MessageBox.Show("Registracija smeštaja uspešna!", "Registracija uspešna", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void BtnUploadFiles_Click(object sender, RoutedEventArgs e)
        {
            _accommodationViewModel.UploadImageToAccommodation(TbImageUrl.Text);
            TbImageUrl.Text = string.Empty;
            DgrImageUrLs.Items.Refresh();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}