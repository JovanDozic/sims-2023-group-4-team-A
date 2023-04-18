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
using System.Windows.Navigation;

namespace SIMSProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private NavigationBar navigationBar;
        private NavigationService _navigationService;
        public Main()
        {
            InitializeComponent();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWind.Content = new Probna();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (navigationBar == null)
            {
                navigationBar = new NavigationBar(); // create a new instance of the NavigationBar page
                MainWind.Content = navigationBar; // set the NavigationBar page as the content of the MainWindow
            }
            else
            {
                MainWind.Content = null; // remove the NavigationBar page from the content of the MainWindow
                navigationBar = null; // set the reference to the NavigationBar page to null to allow for a new instance to be created if the button is clicked again
            }
        }
        /*
        private void ReturnToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            // Vratite se na početnu stranicu (prvi unos) u istoriji navigacije
            while (MainWind.NavigationService.CanGoBack)
            {
                MainWind.NavigationService.RemoveBackEntry();
            }
            MainWind.NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }
        */
    }
}
