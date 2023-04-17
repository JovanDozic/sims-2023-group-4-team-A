using SIMSProject.Controller;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels;

namespace SIMSProject.View.Guest2
{
    /// <summary>
    /// Interaction logic for ShowAndSearchTours.xaml
    /// </summary>
    /// 

    public partial class ShowAndSearchTours : Page
    {
        private Guest _user { get; set; }
        private ToursViewModel _tourViewModel { get; set; }
        public int DurationSearchBox;
        public int MaxGuestSearchBox;
        
        private readonly VoucherController VoucherController = new();

        public ShowAndSearchTours(Guest user)
        {
            InitializeComponent();
            _user = user;
            _tourViewModel = new ToursViewModel();
            this.DataContext = _tourViewModel;

            VoucherController.DeleteExpired();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            String locationAndLanguage = LocationAndLanguageSearch.Text;
            int searchDuration = DurationSearch.Value <= 0 ? -1 : DurationSearch.Value;
            int searchMaxGuests = GuestSearch.Value <= 0 ? -1 : GuestSearch.Value;
            _tourViewModel.Search(locationAndLanguage, searchDuration, searchMaxGuests);
        }

        private void TextSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Foreground = new SolidColorBrush(Colors.Black);
            if (textbox.Text == "Lokacija jezik") textbox.Text = string.Empty;
        }

        private void TextSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            if (textbox.Text == string.Empty)
            {
                textbox.Foreground = new SolidColorBrush(Colors.Gray);
                textbox.Text = "Lokacija jezik";

            }
        }
        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if (!_tourViewModel.IsSelected()) { MessageBox.Show("Odaberite turu koju zelite da rezervisete!"); return; }
            new TourReservationCreation(_user, _tourViewModel.SelectedTour).Show();

            //MessageBox.Show("Odaberite turu koju zelite da rezervisete!");
        }
    }
}
