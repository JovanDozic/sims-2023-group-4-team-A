using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SIMSProject.Application1.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Model.UserModel;

namespace SIMSProject.WPF.Views
{
    public partial class SignInForm : Window
    {
        private readonly UserService _userService;

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _userService = AppInjector.Configure().GetService<UserService>() ?? throw new Exception("Dependency Injection Fazailed.");
        }

        private void SignIn(object? sender, RoutedEventArgs? e)
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Password;
            try
            {
                User? user = _userService.GetUser(username, password) as User;
                if (user == null) MessageBox.Show("Ulogovao se: null", "nista");
                MessageBox.Show("Ulogovao se: " + user.Username, "nista");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
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

    }
}