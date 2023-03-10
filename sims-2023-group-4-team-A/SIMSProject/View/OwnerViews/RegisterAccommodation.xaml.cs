using SIMSProject.Controller;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIMSProject.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for RegisterAccommodation.xaml
    /// </summary>
    public partial class RegisterAccommodation : Window
    {

        private AccommodationController _accommodationController { get; set; } = new();
        private AddressController _addressController { get; set; } = new();


        public Accommodation Accommodation { get; set; }


        public ObservableCollection<string> AccommodationTypeSource { get; set; }
        
        
        
        public RegisterAccommodation()
        {
            InitializeComponent();
            DataContext = this;

            Accommodation = new();

            AccommodationTypeSource = new()
            {
                "Apartman",
                "Kuća",
                "Koliba"
            };

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? focusedTextBox = sender as TextBox;
            if (focusedTextBox == null) return;
            focusedTextBox.Foreground = new SolidColorBrush(Colors.Black);
            if (focusedTextBox.Text == string.Empty 
                || focusedTextBox.Text == "Ulica" 
                || focusedTextBox.Text == "Broj" 
                || focusedTextBox.Text == "Grad" 
                || focusedTextBox.Text == "Država")
            {
                focusedTextBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? focusedTextBox = sender as TextBox;
            if (focusedTextBox == null) return;
            if (focusedTextBox.Text == string.Empty) 
            {
                focusedTextBox.Foreground = new SolidColorBrush(Color.FromArgb(125, 0, 0, 0));
                if (focusedTextBox.Name == "TBStreet") focusedTextBox.Text = "Ulica";
                else if (focusedTextBox.Name == "TBStreetNumber") focusedTextBox.Text = "Broj";
                else if (focusedTextBox.Name == "TBCity") focusedTextBox.Text = "Grad";
                else if (focusedTextBox.Name == "TBCountry") focusedTextBox.Text = "Država";
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Accommodation = new();
            Accommodation.Name = TBName.Text;
            
            Address address = new Address();
            address.Street = TBStreet.Text;
            address.StreetNumber = TBStreetNumber.Text;
            address.City = TBCity.Text;
            address.Country = TBCountry.Text;
            address = _addressController.Create(address);
            Accommodation.Location = address;

            Accommodation.Type = _addressController.GetType(CBType.Text);

            //Accommodation.MaxGuestNumber = int.Parse(TBMaxGuestNumber.Text);
            Accommodation.MinReservationDays = int.Parse(TBMinReservationDays.Text);
            Accommodation.CancellationThreshold = int.Parse(TBCancellationThreshold.Text);
            // TODO: Accommodation.ImageURLs = new List<string>() { "" };

            _accommodationController.Create(Accommodation);
            MessageBox.Show("Registracija smeštaja uspešna!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
