using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

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
            OwnerRegisterAccommodationView addAccommodationView = new(_user, this);
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
            LstAccommodations.SelectedItem = null;
            _viewModel.SearchAccommodations(TbSearchInput.Text);
        }

        private void LstAccommodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstAccommodations.SelectedItem is null) return;
            Accommodation duplicateAccommodation = new Accommodation(_viewModel.Accommodation);

            OwnerAccommodationDetails accommodationDetails = new OwnerAccommodationDetails(_user, duplicateAccommodation);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new OwnerWindow(_user);
            ownerWindow?.SwitchToPage(accommodationDetails);

            LstAccommodations.SelectedItem = null;
        }

        internal void ReloadAccommodations()
        {
            _viewModel.LoadAccommodationsByOwner();
            LstAccommodations.Items.Refresh();
        }

    }
}
