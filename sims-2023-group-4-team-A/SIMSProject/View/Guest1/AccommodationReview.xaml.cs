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
using System.Windows.Shapes;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReview.xaml
    /// </summary>
    public partial class AccommodationReview : Window, INotifyPropertyChanged
    {
        public Guest User = new();
        public Accommodation Accommodation { get; set; }
        public Accommodation selectedAccommodation { set; get; } = new();

      
        
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
        public AccommodationReview(Guest user, Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            Accommodation = accommodation;
            TextBoxNaziv.Text = accommodation.Name;
            TextBoxLokacija.Text = accommodation.Location.ToString();
            TextBoxTip.Text = accommodation.Type;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_Reserve(object sender, RoutedEventArgs e)
        {
            var reserve = new AccommodationReservationConfirmation(User, Accommodation);
            reserve.Show();
        }

        private void mainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TXTImagePlaceholder.Text = "Učitavanje...";
        }
    }
}
