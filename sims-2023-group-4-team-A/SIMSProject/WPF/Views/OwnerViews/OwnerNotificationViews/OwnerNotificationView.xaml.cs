using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System;
using Azure;
using Azure.AI.Translation.Document;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerNotificationViews
{
    public partial class OwnerNotificationView : Page
    {
        private NotificationViewModel _viewModel;

        public OwnerNotificationView(NotificationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void BtnMarkAsRead_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MarkNotificationAsRead();
            NavigationService?.GoBack();
        }

    }
}
