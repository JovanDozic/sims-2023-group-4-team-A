using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest1ViewModels;
using SIMSProject.WPF.Views.Guest1.MainView;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.Guest1.Pages
{
    /// <summary>
    /// Interaction logic for FreeAccommodationReview.xaml
    /// </summary>
    public partial class FreeAccommodationReview : Page
    {
        private AnywhereAnytimeViewModel _anywhereAnytimeViewModel;
        private readonly User _user = new();
        public FreeAccommodationReview(AnywhereAnytimeViewModel vm, User user)
        {
            InitializeComponent();
            _user = user;
            _anywhereAnytimeViewModel = vm;
            DataContext = _anywhereAnytimeViewModel;
            _anywhereAnytimeViewModel.GenerateAvailableDateRanges();
            AddImages();
            ItemsVisibility();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void AddImages()
        {
            imageSlider.AddImages(_anywhereAnytimeViewModel.SelectedAccommodation.ImageURLs);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _anywhereAnytimeViewModel.SaveReservation();
            NavigationService.Navigate(new MainPage(_user));
        }

        public void ItemsVisibility()
        {
            if (_anywhereAnytimeViewModel.AreDatesSelected())
            {
                ReservationButton.Visibility = Visibility.Visible;
                FreeDates.Visibility = Visibility.Visible;
                DatesCombo.Visibility = Visibility.Visible;
            }
            else
            {
                ReservationButtonNew.Visibility = Visibility.Visible;
                NewDatesCombo.Visibility = Visibility.Visible;

            }
        }

        private void FreeDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatesCombo.SelectedItem != null)
                ReservationButton.IsEnabled = true;
        }

        private void NewFreeDates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewDatesCombo.SelectedItem != null)
                ReservationButtonNew.IsEnabled = true;
        }
    }
}
