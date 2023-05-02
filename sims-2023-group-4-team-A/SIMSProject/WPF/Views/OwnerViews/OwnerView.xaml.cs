using SIMSProject.View.Guest2;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerView : Window, INotifyPropertyChanged
    {

        private string _frameViewSource = "OwnerAccommodationViews/OwnerMyAccommodationsView.xaml";
        public string FrameViewSource
        {
            get => _frameViewSource;
            set
            {
                if (value == _frameViewSource) return;
                _frameViewSource = value;
                OnPropertyChanged(nameof(FrameViewSource));
            }
        }


        public OwnerView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void EnableNavBtn(object sender)
        {
            if (sender is not Button button || FindVisualChild<Image>(button) is not Image img) return;
            var filledSource = img.Source?.ToString();
            var index = filledSource?.IndexOf("Resources") ?? -1;
            if (index >= 0 && filledSource is not null) 
                img.Source = new BitmapImage(new Uri($"../../../{filledSource[index..].Replace(".png", "-fill.png")}", UriKind.Relative));
        }

        private void DisableNavBtn(object sender)
        {
            if (sender is not Button button || FindVisualChild<Image>(button) is not Image img) return;
            var filledSource = img.Source?.ToString();
            var index = filledSource?.IndexOf("Resources") ?? -1;
            if (index >= 0 && filledSource is not null)
                img.Source = new BitmapImage(new Uri($"../../../{filledSource[index..].Replace("-fill", "")}", UriKind.Relative));
        }

        private void DisableOtherNavBtns(object sender)
        {
            if (sender is not Button button) return;

            if (button.Name != "NavBtnNotifications") DisableNavBtn(NavBtnNotifications);
            if (button.Name != "NavBtnAccommodations") DisableNavBtn(NavBtnAccommodations);
            if (button.Name != "NavBtnHome") DisableNavBtn(NavBtnHome);
            if (button.Name != "NavBtnForums") DisableNavBtn(NavBtnForums);
            if (button.Name != "NavBtnAccount") DisableNavBtn(NavBtnAccount);

        }

        private void NavBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            switch (button.Name)
            {
                case "NavBtnNotifications":
                    FrameViewSource = "OwnerNotificationViews/OwnerNotificationsView.xaml";
                    EnableNavBtn(sender);
                    DisableOtherNavBtns(sender);
                    break;

                case "NavBtnAccommodations":
                    FrameViewSource = "OwnerAccommodationViews/OwnerMyAccommodationsView.xaml";
                    EnableNavBtn(sender);
                    DisableOtherNavBtns(sender);

                    break;

                case "NavBtnHome":
                    FrameViewSource = "";
                    EnableNavBtn(sender);
                    DisableOtherNavBtns(sender);

                    break;

                case "NavBtnForums":
                    FrameViewSource = "OwnerForumViews/OwnerAllForumsView.xaml";
                    EnableNavBtn(sender);
                    DisableOtherNavBtns(sender);

                    break;

                case "NavBtnAccount":
                    FrameViewSource = "OwnerAccountViews/OwnerAccountView.xaml";
                    EnableNavBtn(sender);
                    DisableOtherNavBtns(sender);

                    break;

            }



        }

        public static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
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


    }
}
