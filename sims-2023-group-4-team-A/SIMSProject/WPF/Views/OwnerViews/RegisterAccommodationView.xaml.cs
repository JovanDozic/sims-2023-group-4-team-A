using System.Windows;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;

namespace SIMSProject.View.OwnerViews
{
    public partial class RegisterAccommodationView : Window
    {
        private User _user;
        private App _app = (App)System.Windows.Application.Current;
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
            if (_app.CurrentLanguage == "en-US")
            {
                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            }
            else
            {
                if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            }

            _accommodationViewModel.RegisterAccommodation();

            if (_app.CurrentLanguage == "en-US")
            {
                MessageBox.Show("Accommodation successfully registrated!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Registracija smeštaja uspešna!", "Uspeh!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            Close();
        }

        private void BtnUploadFiles_Click(object sender, RoutedEventArgs e)
        {
            _accommodationViewModel.UploadImageURLToAccommodation(TbImageUrl.Text);
            TbImageUrl.Text = string.Empty;
            DgrImageUrLs.Items.Refresh();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}