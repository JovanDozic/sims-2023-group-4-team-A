using SIMSProject.Model;
using SIMSProject.FileHandler;
using SIMSProject.View;
using SIMSProject.View.OwnerViews;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Controller.UserController;
using SIMSProject.Model.UserModel;
using System.Windows.Input;
using System.Windows.Controls;
using SIMSProject.View.GuideViews;

namespace SIMSProject
{
    public partial class SignInForm : Window
    {
        private readonly UserController _userController;
        private readonly GuideController guideController;

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

            _userController = new();
            guideController = new();
        }

        private void SignIn(object? sender, RoutedEventArgs? e)
        {
            var user = _userController.GetByUsername(Username) as User;

            if (user != null)
                if (user.Password == txtPassword.Password)
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
                                Guide guide = guideController.GetByID(user.Id);
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
                else MessageBox.Show("Pogrešna šifra!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            else MessageBox.Show(Username + " korisnik ne postoji!", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
                string senderName = senderButton.Name.ToString() ?? "";
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
