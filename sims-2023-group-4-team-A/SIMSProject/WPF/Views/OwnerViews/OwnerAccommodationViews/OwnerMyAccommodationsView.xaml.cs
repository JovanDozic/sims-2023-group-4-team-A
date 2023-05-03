﻿using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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





        private void TbSearchInput_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            TbSearchInput.Text = string.Empty;
            TbSearchInput.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TbSearchInput_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TbSearchInput.Text != string.Empty) return;

            TbSearchInput.Text = "Pretraga";
            TbSearchInput.Foreground = new SolidColorBrush(Colors.Gray);


        }

        private void TbSearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO: Execute search
        }






        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
