using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        public ToursViewModel ToursViewModel { get; set; }
        public Domain.Models.UserModels.Guest2 _user { get; set; }
        public ShowAndSearchTours(Domain.Models.UserModels.Guest2 user)
        {
            InitializeComponent();
            _user = user;
            ToursViewModel = new ToursViewModel(user);
            this.DataContext = ToursViewModel;
            //this.DataContext = new ToursViewModel(user);
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

        private void Reserce_Click(object sender, RoutedEventArgs e)
        {
            if (ToursViewModel.IsSelected())
            {
                //new TourReservationCreation(_user, ToursViewModel.SelectedTour).Show();
                //new ReservationCreation(_user, SelectedTour);
                NavigationService.Navigate(new ReservationCreation(_user, ToursViewModel.SelectedTour));
                ToursViewModel.LabelVisibility = Visibility.Hidden;
                return;
            }
            ToursViewModel.LabelVisibility = Visibility.Visible;
        }
    }
}
