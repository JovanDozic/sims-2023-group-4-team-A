using SIMSProject.Model;
using SIMSProject.FileHandler;
using SIMSProject.View;
using SIMSProject.View.OwnerViews;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SIMSProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserFileHandler _fileHandler;

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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _fileHandler = new UserFileHandler();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            
            InitialWindow initialWindow = new InitialWindow();
            initialWindow.Show();
            
            /*
            Guest user = _fileHandler.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    CommentsOverview commentsOverview = new CommentsOverview(user);
                    commentsOverview.Show();
                    Close();
                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            */
            
        }
    }
}
