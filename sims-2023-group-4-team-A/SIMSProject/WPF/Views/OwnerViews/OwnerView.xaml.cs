using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccountViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerForumViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerNotificationViews;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerView : Page, INotifyPropertyChanged
    {
        private User _user = new();


        public OwnerView(User user)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;

            NavBtn_Click(NavBtnHome, null);
        }

        private void EnableNavButton(object sender)
        {
            if (sender is not Button button || FindVisualChild<Image>(button) is not Image img) return;
            var filledSource = img.Source?.ToString();
            var index = filledSource?.IndexOf("Resources") ?? -1;
            if (index >= 0 && filledSource is not null && !filledSource.Contains("-fill.png"))
                img.Source = new BitmapImage(new Uri($"../../../{filledSource[index..].Replace(".png", "-fill.png")}", UriKind.Relative));
        }

        private void DisableNavButton(object sender)
        {
            if (sender is not Button button || FindVisualChild<Image>(button) is not Image img) return;
            var filledSource = img.Source?.ToString();
            var index = filledSource?.IndexOf("Resources") ?? -1;
            if (index >= 0 && filledSource is not null)
                img.Source = new BitmapImage(new Uri($"../../../{filledSource[index..].Replace("-fill", "")}", UriKind.Relative));
        }

        private void UpdateNotificationButtons(object sender)
        {
            if (sender is not Button button) return;

            if (button.Name != "NavBtnNotifications") DisableNavButton(NavBtnNotifications);
            if (button.Name != "NavBtnAccommodations") DisableNavButton(NavBtnAccommodations);
            if (button.Name != "NavBtnHome") DisableNavButton(NavBtnHome);
            if (button.Name != "NavBtnForums") DisableNavButton(NavBtnForums);
            if (button.Name != "NavBtnAccount") DisableNavButton(NavBtnAccount);

            EnableNavButton(sender);
        }

        private void NavBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;

            MainFrame.Navigate(
                button.Name switch
                {
                    "NavBtnNotifications" => new OwnerNotificationsView(_user),
                    "NavBtnAccommodations" => new OwnerMyAccommodationsView(_user),
                    "NavBtnHome" => new OwnerHomeView(),
                    "NavBtnForums" => new OwnerAllForumsView(),
                    _ => new OwnerAccountView(_user),
                }
            );
            
            UpdateNotificationButtons(sender);
        }

        public static T FindVisualChild<T>(DependencyObject? parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is not null and T) return (T)child;
                else
                {
                    T found = FindVisualChild<T>(child);
                    if (found != null)
                        return found;
                }
            }
            return null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MainFrame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            Storyboard storyboard = (Storyboard)FindResource("FadeAnimation");
            storyboard.Begin(MainFrame);
        }
    }
}
