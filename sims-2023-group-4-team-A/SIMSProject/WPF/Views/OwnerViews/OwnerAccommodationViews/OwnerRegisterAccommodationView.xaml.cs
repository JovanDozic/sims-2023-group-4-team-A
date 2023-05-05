using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerRegisterAccommodationView : Page
    {
        private User _user;
        private readonly AccommodationViewModel _viewModel;

        public OwnerRegisterAccommodationView(User user)
        {
            InitializeComponent();
            _user = user;

            _viewModel = new(_user);
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_viewModel.Accommodation.Name);
        }

        private void BtnUploadImages_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
