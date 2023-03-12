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
using System.Diagnostics;

namespace SIMSProject.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for RegisterAccommodation.xaml
    /// </summary>
    public partial class RegisterAccommodation : Window
    {
        public Accommodation Accommodation { get; set; } = new();
        public string ImageURLs { get; set; } = "primer1.jpg, primer2.png...";
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
            CBType.SelectedIndex = 0;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Accommodation.IsValid && !Accommodation.Location.IsValid)
            {
                MessageBox.Show("Nisu unešeni svi podaci!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Accommodation.Location = _addressController.Create(Accommodation.Location);
            Trace.WriteLine("\nACC: " + Accommodation.ImageURLsCSV);
            _accommodationController.Create(Accommodation);

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

        private void TextBox_FocusChanged(object sender, RoutedEventArgs e)
        {
            if (Accommodation.IsValid && Accommodation.Location.IsValid) BTNRegister.IsEnabled = true;
            else BTNRegister.IsEnabled = false;
        }
    }
}
