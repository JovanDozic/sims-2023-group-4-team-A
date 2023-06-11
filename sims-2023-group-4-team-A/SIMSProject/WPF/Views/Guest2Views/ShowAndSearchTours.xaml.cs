using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels;
using SIMSProject.WPF.Views.Guest2Views;

namespace SIMSProject.View.Guest2
{
    /// <summary>
    /// Interaction logic for ShowAndSearchTours.xaml
    /// </summary>
    /// 

    public partial class ShowAndSearchTours : Page
    {
        public ShowAndSearchTours(Domain.Models.UserModels.Guest2 user, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new ToursViewModel(user, navigationService);
        }

        private void TextSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (LocationSearch is null) return;
            LocationSearch.Foreground = new SolidColorBrush(Colors.Black);
            if (LocationSearch.Text == "Gde putujete?") LocationSearch.Text = string.Empty;
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

    }
}
