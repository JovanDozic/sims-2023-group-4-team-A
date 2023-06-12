using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerWindow : Window
    {
        private User _user;
        private SuggestionNotificationService _suggestionNotificationService;
        private NotificationService _notificationService;
        private UserService _userService;

        public OwnerWindow(User user)
        {
            _user = user;
            AdaptAppThemeAndLanguageToUser();

            InitializeComponent();
            DataContext = this;
            _suggestionNotificationService = new(_user);
            _notificationService = Injector.GetService<NotificationService>();
            _userService = Injector.GetService<UserService>();
           
            SwitchToPage(new OwnerView(user));

            StartParallelTasks();
        }

        private void AdaptAppThemeAndLanguageToUser()
        {
            if (_user is not Owner owner) return;
            ((App)System.Windows.Application.Current).ChangeTheme(owner.SelectedTheme);
            ((App)System.Windows.Application.Current).ChangeLanguage(owner.SelectedLanguage);
        }

        public void SwitchToPage(Page page)
        {
            MainFrame.Navigate(page);
        }

        private void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Storyboard storyboard = (Storyboard)FindResource("FadeAnimation");
            storyboard.Begin(MainFrame);
        }

        private void StartParallelTasks()
        {
            Task.Run(() =>
            {
                try
                {
                    _suggestionNotificationService.GenerateSuggestionNotifications();
                    if (_user is Owner owner)
                    {
                        owner.HasNotifications = _notificationService.GetAllUnreadByUser(_user).Count > 0;
                        _userService.UpdateOwner(owner);
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(ex.StackTrace);
                }
            });
        }
    }
}
