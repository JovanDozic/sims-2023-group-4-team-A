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
using System.Drawing;
using System.Windows.Media;

namespace SIMSProject.View.OwnerViews
{
    public partial class RegisterAccommodation : Window, INotifyPropertyChanged
    {
        public Accommodation Accommodation { get; set; }
        private AccommodationController _accommodationController { get; set; } = new();
        private LocationController _locationController { get; set; } = new();
        public ObservableCollection<string> AccommodationTypeSource { get; set; }
        private bool _imageAdded { get; set; } = false;
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
            Accommodation.ImageURLsToCSV();
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

        private void BTNAddFiles_Click(object sender, RoutedEventArgs e)
        {
            Accommodation.ImageURLs.Add(TBImageURL.Text);
            //Accommodation.ImageURLsToCSV();
            DGRImageURLs.Items.Refresh();
            _imageAdded = true;
        }

        private void TBImageURL_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_imageAdded)
            {
                _imageAdded = false;
                TBImageURL.Text = string.Empty;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DGRImageURLs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TXTImagePlaceholder.Text = "Učitavanje...";
        }
    }
}
