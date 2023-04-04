using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIMSProject.Controller.UserController;
using SIMSProject.Model.UserModel;
using SIMSProject.View;
using SIMSProject.View.GuideViews;
using SIMSProject.View.OwnerViews;

namespace SIMSProject
{
    public partial class SignInForm : Window
    {
        private readonly UserController _userController;
        private readonly GuideController _guideController;
        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;

            _userController = new UserController();
            _guideController = new GuideController();
        }

        private void SignIn(object? sender, RoutedEventArgs? e)
        {
            var user = _userController.GetByUsername(Username) as User;

            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    switch (user.Role)
                    {
                        case "Vlasnik":
                        {
                            OwnerInitialWindow window = new(user as Owner ?? new Owner(0, "<null>", "<null>"));
                            window.Show();
                            break;
                        }
                        case "Vodič":
                        {
                            var guide = _guideController.GetByID(user.Id);
                            GuideInitialWindow window = new(guide);
                            window.Show();
                            break;
                        }
                        case "Gost":
                        {
                            GuestInitialWindow window = new(user as Guest ?? new Guest(0, "<null>", "<null>"));
                            window.Show();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pogrešna šifra!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(Username + " korisnik ne postoji!", "Greška!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                SignIn(sender, e);
            }
        }

        private void TBUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = "";
        }

        private void TBPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = "";
        }

        private void BTNGoTo_Click(object sender, RoutedEventArgs e)
        {
            var senderButton = sender as Button;
            if (senderButton != null)
            {
                var senderName = senderButton.Name ?? "";
                if (senderName == "BTNGoToOwner")
                {
                    Username = "jovan";
                    txtPassword.Password = "jovan";
                }
                else if (senderName == "BTNGoToGuest")
                {
                    Username = "marko";
                    txtPassword.Password = "marko";
                }
                else if (senderName == "BTNGoToGuide")
                {
                    Username = "nata";
                    txtPassword.Password = "nata";
                }

                SignIn(sender, e);
            }
        }
    }
}