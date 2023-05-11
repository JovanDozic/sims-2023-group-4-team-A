using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerMyAccommodationsView : Page, INotifyPropertyChanged
    {
        private User _user = new();
        private AccommodationViewModel _viewModel;

        public OwnerMyAccommodationsView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);

            DataContext = _viewModel;

            _viewModel.LoadAccommodationsByOwner();
        }

        private void BtnAddAccommodation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OwnerRegisterAccommodationView addAccommodationView = new(_user);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(addAccommodationView);

            LstAccommodations.Items.Refresh();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TbSearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SearchAccommodations(TbSearchInput.Text);
        }

        private void LstAccommodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OwnerAccommodationDetails accommodationDetails = new(_user, new Accommodation(_viewModel.Accommodation));
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(accommodationDetails);
        }
    }
}
