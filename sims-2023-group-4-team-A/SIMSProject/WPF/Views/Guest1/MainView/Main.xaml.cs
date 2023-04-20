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
using SIMSProject.View;
using System.ComponentModel;
using SIMSProject.WPF.Views;
using SIMSProject.WPF.Views.Guest1.Pages;
using SIMSProject.Domain.Models.UserModels;

namespace SIMSProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private readonly User _user;
        public Main(User user)
        {
            InitializeComponent();
            _user = user;
            OpenMainPage();
        }
     
        public void OpenMainPage()
        {
            MainWind.Content = new MainPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWind.Content = new Probna();
            if (ButtonMenu.IsChecked == true)
            {
                ButtonMenu.IsChecked = false;
            }
        }
        private void Button_Click_Home(object sender, RoutedEventArgs e)
        {
            MainWind.Content = new MainPage();
            if(ButtonMenu.IsChecked == true)
            {
                ButtonMenu.IsChecked = false;
            }
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            var open = new LogIn();
            Close();
            open.Show();
            
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ButtonMenu.IsChecked = false;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Zatvori meni nakon izbora stavke u ListView-u
            if (ButtonMenu.IsChecked == true)
            {
                ButtonMenu.IsChecked = false;
            }

            // Izvrši željenu akciju za izabranu stavku
            if (ListViewMenu.SelectedItem != null)
            {
                ListViewItem selectedItem = e.AddedItems[0] as ListViewItem;
                switch (selectedItem.Name)
                {
                   // case "ItemAllAccommodations":
                     //   MainWind.Navigate(new AllAccommodationsPage());
                       // break;
                    case "ItemReservations":
                        MainWind.Navigate(new ReservationList(_user));
                        ListViewMenu.SelectedItem = null;
                        break;
                    case "ItemRating":
                        MainWind.Navigate(new OwnerRating(_user));
                        ListViewMenu.SelectedItem = null;
                        break;
                    //case "ItemSearch":
                      //  MainWind.Navigate(new SearchPage());
                        //break;
                    //case "ItemForums":
                      //  MainWind.Navigate(new ForumsPage());
                       // break;
                    //case "ItemStatistics":
                      //  MainWind.Navigate(new StatisticsPage());
                        //break;
                    //case "ItemReport":
                      //  MainWind.Navigate(new ReportPage());
                        //break;
                }


            }
        }
    }
}
