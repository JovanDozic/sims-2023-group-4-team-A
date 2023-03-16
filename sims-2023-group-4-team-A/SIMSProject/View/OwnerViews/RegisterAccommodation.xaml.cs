using SIMSProject.Controller;
using SIMSProject.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for RegisterAccommodation.xaml
    /// </summary>
    public partial class RegisterAccommodation : Window, INotifyPropertyChanged
    {
        public Accommodation Accommodation { get; set; }
        private AccommodationController _accommodationController { get; set; } = new();
        private LocationController _locationController { get; set; } = new();
        public ObservableCollection<string> AccommodationTypeSource { get; set; }
        private string _selectedImageFile = string.Empty;
        public string SelectedImageFile
        {
            get => _selectedImageFile;
            set
            {
                if (_selectedImageFile != value)
                {
                    _selectedImageFile = value;
                    OnPropertyChanged(nameof(SelectedImageFile));
                }
            }
        }
        
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
            if (!Accommodation.IsValid || !Accommodation.Location.IsValid || Accommodation.ImageURLsCSV == string.Empty)
            {
                MessageBox.Show("Nisu uneti svi podaci!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Accommodation.Location = _locationController.Create(Accommodation.Location);
            _accommodationController.Create(Accommodation);

            MessageBox.Show("Registracija smeštaja uspešna!", "Registracija uspešna", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTNUploadFiles_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new()
            {
                Multiselect = true,
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp"
            };

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                Accommodation.ImageURLs = dlg.FileNames.ToList();
                Accommodation.ImageURLsToCSV();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
