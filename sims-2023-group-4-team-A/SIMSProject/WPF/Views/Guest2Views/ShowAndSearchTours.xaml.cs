using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
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
        
        public ShowAndSearchTours(Guest user)
        {
            InitializeComponent();
            _user = user;
            _tourViewModel = new ToursViewModel(_user);
            this.DataContext = _tourViewModel;
            CheckPending();
        }

        private void CheckPending()
        {
            foreach (var tourGuest in _tourViewModel.GetPendingTourGuests(_user))
            {
                if(tourGuest.TourAppointment.TourStatus == Domain.Models.TourModels.Status.ACTIVE)
                {
                    if (MessageBox.Show("Potvrdite prisustvo na " + tourGuest.TourAppointment.Tour.Name
                    + " turi, u terminu " + tourGuest.TourAppointment.Date.ToString() + ".",
                    "Potvrdite prisustvo",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        _tourViewModel.ConfirmTourGuestAttendance(tourGuest);
                    }
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            String locationAndLanguage = LocationAndLanguageSearch.Text;
            String language = ConvertLanguage(CbLanguage.Text);
            _tourViewModel.Search(locationAndLanguage, language);
            LblSelectingTour.Visibility = Visibility.Hidden;
        }
        private string ConvertLanguage(string selectedLanguage)
        {
            switch (selectedLanguage)
            {
                case "Srpski":
                    return "SERBIAN";
                case "Engleski":
                    return "ENGLISH";
                case "Španski":
                    return "SPANISH";
                case "Francuski":
                    return "FRENCH";
                default:
                    return "";
            }
        }
        private void TextSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Foreground = new SolidColorBrush(Colors.Black);
            if (textbox.Text == "Gde putujete?") textbox.Text = string.Empty;
        }

        private void TextSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            if (textbox.Text == string.Empty)
            {
                textbox.Foreground = new SolidColorBrush(Colors.Gray);
                textbox.Text = "Gde putujete?";

            }
        }
        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if (_tourViewModel.IsSelected())
            {
                new TourReservationCreation(_user, _tourViewModel.SelectedTour).Show();
                LblSelectingTour.Visibility = Visibility.Hidden;
                return;
            }
            LblSelectingTour.Visibility = Visibility.Visible;
        }
        
    }
}
