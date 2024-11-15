﻿using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.Guest1.Pages
{
    /// <summary>
    /// Interaction logic for SearchedAccommodations.xaml
    /// </summary>
    public partial class SearchedAccommodations : Page
    {
        private AccommodationViewModel _accommodationViewModel;
        private readonly User _user = new();
        public SearchedAccommodations(AccommodationViewModel accommodationViewModel, User user)
        {
            InitializeComponent();
            _user = user;
            _accommodationViewModel = accommodationViewModel;
            DataContext = _accommodationViewModel;
            LabelVisibility();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public void LabelVisibility()
        {
            if (_accommodationViewModel.IsAccommodationFound())
            {
                FoundLabel.Visibility = Visibility.Visible;
            }
            else
                NotFoundLabel.Visibility = Visibility.Visible;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SearchedAccLW.SelectedItem != null)
            {
                NavigationService.Navigate(new AccommodationReview(_accommodationViewModel, _user));
            }
        }
    }
}
