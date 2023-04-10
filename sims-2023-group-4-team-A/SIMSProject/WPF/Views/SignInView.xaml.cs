using SIMSProject.WPF.ViewModels.ApplicationViewModels;
using System.Windows;
using System.Windows.Input;

namespace SIMSProject.WPF.Views
{
    public partial class SignInView : Window
    {
        private SignInViewModel _signInViewModel;

        public SignInView()
        {
            InitializeComponent();
            _signInViewModel = new SignInViewModel();
            DataContext = _signInViewModel;
            _signInViewModel.DirectSignIn("jovan", "jovan");
            Close();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            _signInViewModel.SignIn(TxtPassword.Password);
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                BtnSignIn_Click(sender, e);
            }
        }

        private void BtnGoToOwner_Click(object sender, RoutedEventArgs e)
        {
            _signInViewModel.DirectSignIn("jovan", "jovan");
            Close();
        }

        private void BtnGoToGuest_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement
        }

        private void BtnGoToGuide_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement
        }
    }
}