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

namespace SIMSProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window, INotifyPropertyChanged
    {
        public Main()
        {
            InitializeComponent();
            OpenMainPage();
        }

        
        public void OpenMainPage()
        {
            MainWind.Content = new MainPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWind.Content = new Probna();
        }
        private void Button_Click_Home(object sender, RoutedEventArgs e)
        {
            MainWind.Content = new MainPage();
        }


        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            var open = new SignInForm();
            Close();
            open.Show();
            
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ButtonMenu.IsChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
