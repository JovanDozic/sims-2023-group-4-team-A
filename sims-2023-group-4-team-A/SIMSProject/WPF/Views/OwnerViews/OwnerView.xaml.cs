using Dynamitey.DynamicObjects;
using Microsoft.TeamFoundation.Test.WebApi;
using Microsoft.VisualStudio.Services.Commerce;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.Guest1.MainView;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccountViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerForumViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerNotificationViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private App App => (App)System.Windows.Application.Current;

        public OwnerView(User user)
        {
            InitializeComponent();
            DataContext = this;
            _user = user;
            
            NavBtn_Click(NavBtnHome, null);
        }

        private void ChangeNavButtonIcon(object sender, string resourceName)
        {
            if (sender is not Button button || FindVisualChild<Image>(button) is not Image img) return;
            img.Source = (ImageSource)App.Resources[resourceName];
        }

        private void UpdateNotificationButtons(object sender)
        {
            if (sender is not Button button) return;

            if (button.Name != "NavBtnNotifications") ChangeNavButtonIcon(NavBtnNotifications, "NotificationsMenuIcon");
            else ChangeNavButtonIcon(sender, "NotificationsMenuIconFill");
            if (button.Name != "NavBtnAccommodations") ChangeNavButtonIcon(NavBtnAccommodations, "AccommodationsMenuIcon");
            else ChangeNavButtonIcon(sender, "AccommodationsMenuIconFill");
            if (button.Name != "NavBtnHome") ChangeNavButtonIcon(NavBtnHome, "HomeMenuIcon");
            else ChangeNavButtonIcon(sender, "HomeMenuIconFill");
            if (button.Name != "NavBtnForums") ChangeNavButtonIcon(NavBtnForums, "ForumsMenuIcon");
            else ChangeNavButtonIcon(sender, "ForumsMenuIconFill");
            if (button.Name != "NavBtnAccount") ChangeNavButtonIcon(NavBtnAccount, "AccountMenuIcon");
            else ChangeNavButtonIcon(sender, "AccountMenuIconFill");
        }

        private void NavBtn_Click(object sender, RoutedEventArgs? e)
        {
            if (sender is not Button button) return;

            MainFrame.Navigate(
                button.Name switch
                {
                    "NavBtnNotifications" => new OwnerNotificationsView(_user),
                    "NavBtnAccommodations" => new OwnerMyAccommodationsView(_user),
                    "NavBtnHome" => new OwnerHomeView(_user),
                    "NavBtnForums" => new OwnerForumLocationsView(_user),
                    _ => new OwnerAccountView(_user),
                }
            );

            UpdateNotificationButtons(sender);
        }

        public object NavigateToPage(string navBtnName)
        {
            if (navBtnName == "NavBtnAccommodations") NavBtn_Click(NavBtnAccommodations, null);
            if (navBtnName == "NavBtnNotifications") NavBtn_Click(NavBtnNotifications, null);
            return this;
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

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // TODO: Move this logic to corresponding button in My Account view
            if (App.CurrentTheme == "Light")
            {
                App.ChangeTheme("Dark");
                App.CurrentTheme = "Dark";
            }
            else
            {
                App.ChangeTheme("Light");
                App.CurrentTheme = "Light";
            }
            NavBtn_Click(NavBtnAccount, null);
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // TODO: Move this logic to corresponding button in My Account view
            if (App.CurrentLanguage.Equals("en-US"))
            {
                App.ChangeLanguage("sr-LATN");
            }
            else
            {
                App.ChangeLanguage("en-US");
            }
        }
    }
}
