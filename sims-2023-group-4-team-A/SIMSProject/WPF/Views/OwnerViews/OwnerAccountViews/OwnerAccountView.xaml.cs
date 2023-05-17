using SIMSProject.Domain.Models.UserModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccountViews
{
    public partial class OwnerAccountView : Page
    {
        private User _user;

        public User User
        {
            get => _user;
            set
            {
                if (_user == value) return;
                _user = value;
            }
        }

        public OwnerAccountView(User user)
        {
            InitializeComponent();
            _user = user;
            DataContext = this;
        }

        private void BtnOpenOldMenu_Click(object sender, RoutedEventArgs e)
        {
            OwnerHomeViewOld ownerHomeViewOld = new(_user);
            ownerHomeViewOld.Show();

            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(new User());
            ownerWindow?.Close();
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new();
            signInView.Show();

            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(new User());
            ownerWindow?.Close();
        }
    }
}
