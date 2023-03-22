using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SIMSProject.Controller;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;

namespace SIMSProject.View.OwnerViews
{
    public partial class RegisterAccommodation : Window, INotifyPropertyChanged
    {
        public Owner User { get; set; }
        public Accommodation Accommodation { get; set; } = new();
        private AccommodationController _accommodationController { get; set; } = new();
        private LocationController _locationController { get; set; } = new();
        public ObservableCollection<string> AccommodationTypeSource { get; set; }
        private bool ImageAdded { get; set; }
        public string _selectedImageFile = string.Empty;
        public string SelectedImageFile
        {
            get => _selectedImageFile;
            set
            {
                if (_selectedImageFile == value) return;
                _selectedImageFile = value;
                OnPropertyChanged();
            }
        }

        public RegisterAccommodation(Owner user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;

            AccommodationTypeSource = new ObservableCollection<string>
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

            Accommodation.Owner = User;
            Accommodation.Location = _locationController.Create(Accommodation.Location);
            _accommodationController.Create(Accommodation);

            MessageBox.Show("Registracija smeštaja uspešna!", "Registracija uspešna", MessageBoxButton.OK,
                MessageBoxImage.Information);

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTNAddFiles_Click(object sender, RoutedEventArgs e)
        {
            Accommodation.ImageURLs.Add(TBImageURL.Text);
            Accommodation.ImageURLsToCSV();
            DGRImageURLs.Items.Refresh();
            ImageAdded = true;
        }

        private void TBImageURL_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ImageAdded)
            {
                ImageAdded = false;
                TBImageURL.Text = string.Empty;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DGRImageURLs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TXTImagePlaceholder.Text = "Učitavanje...";
        }
    }
}