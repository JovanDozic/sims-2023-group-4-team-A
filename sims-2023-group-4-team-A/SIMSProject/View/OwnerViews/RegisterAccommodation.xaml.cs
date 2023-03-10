using SIMSProject.Controller;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMSProject.CustomControls;

namespace SIMSProject.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for RegisterAccommodation.xaml
    /// </summary>
    public partial class RegisterAccommodation : Window
    {
        public Accommodation Accommodation { get; set; } = new();
        private AccommodationController _accommodationController { get; set; } = new();
        private AddressController _addressController { get; set; } = new();

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
            if (sender is not TextBox focusedTextBox) return;
            focusedTextBox.Foreground = new SolidColorBrush(Colors.Black);
            if (focusedTextBox.Text == string.Empty
                || focusedTextBox.Text == "Ulica"
                || focusedTextBox.Text == "Broj"
                || focusedTextBox.Text == "Grad"
                || focusedTextBox.Text == "Država"
                || focusedTextBox.Text == "primer1.jpg, primer2.png...")
                focusedTextBox.Text = string.Empty;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is not TextBox focusedTextBox) return;
            if (focusedTextBox.Text == string.Empty)
            {
                focusedTextBox.Foreground = new SolidColorBrush(Color.FromArgb(125, 0, 0, 0));
                if (focusedTextBox.Name == "TBStreet") focusedTextBox.Text = "Ulica";
                else if (focusedTextBox.Name == "TBStreetNumber") focusedTextBox.Text = "Broj";
                else if (focusedTextBox.Name == "TBCity") focusedTextBox.Text = "Grad";
                else if (focusedTextBox.Name == "TBCountry") focusedTextBox.Text = "Država";
                else if (focusedTextBox.Name == "TBImageURLs") focusedTextBox.Text = "primer1.jpg, primer2.png...";

            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            //Address address = new()
            //{
            //    Street = TBStreet.Text,
            //    StreetNumber = TBStreetNumber.Text,
            //    City = TBCity.Text,
            //    Country = TBCountry.Text
            //};
            //address = _addressController.Create(address);

            //Accommodation = new()
            //{
            //    Name = TBName.Text,
            //    Location = address,
            //    Type = _accommodationController.GetType(CBType.Text),
            //    MaxGuestNumber = int.Parse(TBMaxGuestNumber.Text),
            //    MinReservationDays = int.Parse(TBMinReservationDays.Text),
            //    CancellationThreshold = int.Parse(TBCancellationThreshold.Text),
            //    ImageURLs = new()
            //};

            //if (!TBImageURLs.Text.Contains("primer1.jpg, primer2.png..."))
            //{
            //    var imageURLs = TBImageURLs.Text.Replace(" ", "").Replace("\n", "").Replace("\t", "").Split(',');
            //    foreach (var imageURL in imageURLs) Accommodation.ImageURLs.Add(imageURL);
            //}

            //_accommodationController.Create(Accommodation);
            MessageBox.Show("Registracija smeštaja uspešna!", "Registracija uspešna", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

      



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
