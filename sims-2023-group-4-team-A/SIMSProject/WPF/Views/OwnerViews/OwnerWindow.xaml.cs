using SIMSProject.Application.Services.AccommodationServices;
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

        public OwnerWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            _suggestionNotificationService = new(_user);
           
            SwitchToPage(new OwnerView(user));

            StartParallelTasks();
            AdaptViewToUser();
        }

        private void AdaptViewToUser()
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
