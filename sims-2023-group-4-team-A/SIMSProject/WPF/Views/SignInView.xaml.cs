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

            // TODO: Remove this before commit to Develop
            // BtnGoToOwner_Click(null, null);
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (_signInViewModel.SignIn(TxtPassword.Password)) Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                BtnSignIn_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                Close();
            }
        }

        private void BtnGoToOwner_Click(object? sender, RoutedEventArgs? e)
        {
            _signInViewModel.DirectSignIn("jovan", "jovan");
            Close();
        }

        private void BtnGoToGuest1_Click(object sender, RoutedEventArgs e)
        {
            _signInViewModel.DirectSignIn("marko", "marko");
            Close();
        }

        private void BtnGoToGuest2_Click(object sender, RoutedEventArgs e)
        {
            _signInViewModel.DirectSignIn("anja", "anja");
            Close();
        }

        private void BtnGoToGuide_Click(object sender, RoutedEventArgs e)
        {
            _signInViewModel.DirectSignIn("nata", "nata");
            Close();
        }
    }
}
