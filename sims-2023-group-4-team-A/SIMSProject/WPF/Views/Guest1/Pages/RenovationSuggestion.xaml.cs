using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
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
    /// Interaction logic for RenovationSuggestion.xaml
    /// </summary>
    public partial class RenovationSuggestion : Page
    {
        private readonly User _user = new();
        private OwnerRatingViewModel _rating;
        private RenovationSuggestionViewModel _suggestion;
        public RenovationSuggestion(OwnerRatingViewModel rating, User user)
        {
            InitializeComponent();
            _rating = rating;
            _user = user;
            _suggestion = new();
            DataContext = _suggestion;
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void TextComment_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Foreground = new SolidColorBrush(Colors.Black);
            if (textbox.Text == "Napišite šta Vam se nije svidelo u vezi smeštaja") textbox.Text = string.Empty;
        }

        private void TextComment_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            if (textbox.Text == string.Empty)
            {
                textbox.Foreground = new SolidColorBrush(Colors.Gray);
                textbox.Text = "Napišite šta Vam se nije svidelo u vezi smeštaja";
            }
        }
        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyComboBox.SelectedItem != null)
            {
                SelectionValidation.Content = " ";
                RequestButton.IsEnabled = true;
            }
            
        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (Comment.Text == "Napišite šta Vam se nije svidelo u vezi smeštaja")
                Comment.Text = string.Empty;
            _suggestion.SendRequest();
            _rating.RateWithRenovation(_suggestion.Renovation);
            MessageBox.Show("Ocena i preporuka uspešno poslati!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new MainPage(_user));
        }

        private void LoadText(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Text = "Napišite šta Vam se nije svidelo u vezi smeštaja";
        }
    }
}
