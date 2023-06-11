using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using SIMSProject.WPF.Views.Guest1.Pages;
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

namespace SIMSProject.WPF.Views.Guest1.MainView
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly User _user = new();
        private readonly AccommodationViewModel _accommodationViewModel;
        public MainPage(User user)
        {
            InitializeComponent();
            _user = user;
            _accommodationViewModel = new AccommodationViewModel(_user);
            DataContext = _accommodationViewModel;
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            _accommodationViewModel.Search(Search1.Text, _accommodationViewModel.MinReservationDays, _accommodationViewModel.MaxGuestNumber);  
            var searchPage = new SearchedAccommodations(_accommodationViewModel, _user);
            searchPage.SearchedAccLW.Items.Clear();
            NavigationService.Navigate(searchPage);
            
        }
        private void TextSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Foreground = new SolidColorBrush(Colors.Black);
            if (textbox.Text == "Naziv/Tip/Lokacija") textbox.Text = string.Empty;
        }

        private void TextSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            if (textbox.Text == string.Empty)
            {
                textbox.Foreground = new SolidColorBrush(Colors.Gray);
                textbox.Text = "Naziv/Tip/Lokacija";

            }
        }
    }
}
