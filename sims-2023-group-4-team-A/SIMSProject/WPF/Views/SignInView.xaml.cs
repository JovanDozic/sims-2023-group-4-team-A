using SIMSProject.WPF.ViewModels.ApplicationViewModels;
using System.Windows;
using System.Windows.Input;

namespace SIMSProject.WPF.Views
{
    public partial class SignInView : Window
    {

        public SignInView()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            ((SignInViewModel)DataContext).SignIn(TxtPassword.Password);
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                BtnSignIn_Click(sender, e);
            }
        }
    }
}