using SIMSProject.WPF.ViewModels.ApplicationViewModels;
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

namespace SIMSProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private SignInViewModel _signInViewModel;
        public LogIn()
        {
            InitializeComponent();
            _signInViewModel = new SignInViewModel();
            DataContext = _signInViewModel;
        }

        private void Button_Click_SignIn(object sender, RoutedEventArgs e)
        {
            if (_signInViewModel.GuestSignIn(PasswordTxt.Password)) Close();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
