﻿using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
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

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestCreation.xaml
    /// </summary>
    public partial class ComplexTourRequestCreation : Page
    {
        public Guest2 _user = new();
        private ComplexTourRequestViewModel _viewModel;
        public ComplexTourRequestCreation(Guest2 user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new ComplexTourRequestViewModel(_user);
            this.DataContext = _viewModel;
            CbLanguage.SelectedItem = null;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyTourRequests(_user));

        }
        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.SelectedDate = null;
                datePicker.DisplayDateStart = DateTime.Today.AddDays(2);
            }
        }
        private void DateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker dateFromPicker = sender as DatePicker;
            if (dateFromPicker != null && DateTo.SelectedDate < dateFromPicker.SelectedDate)
            {
                DateTo.SelectedDate = dateFromPicker.SelectedDate;
            }
        }

        private void DateTo_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker dateToPicker = sender as DatePicker;
            if (dateToPicker != null && DateFrom.SelectedDate.HasValue)
            {
                dateToPicker.DisplayDateStart = DateFrom.SelectedDate.Value;
            }
        }

        private void DateTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker dateToPicker = sender as DatePicker;
            if (dateToPicker != null && dateToPicker.SelectedDate < DateFrom.SelectedDate)
            {
                dateToPicker.SelectedDate = DateFrom.SelectedDate;
            }
        }

        private void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            BtnCreation.IsEnabled = false;
            BtnAddPart.IsEnabled = false;
            LblRequestCreated.Visibility = Visibility.Visible;
            LblGuestNumber.Visibility = Visibility.Hidden;
            LblCheck.Visibility = Visibility.Hidden;
            LblPartCreated.Visibility = Visibility.Hidden;
            _viewModel.CreateRequest();
        }

        private void BtnAddPart_Click(object sender, RoutedEventArgs e)
        {
            if ((CbLocation.Text == string.Empty || Description.Text == string.Empty || CbLanguage.Text == string.Empty || DateFrom.Text == string.Empty || DateTo.Text == string.Empty))
            {
                LblCheck.Visibility = Visibility.Visible;
                LblPartCreated.Visibility = Visibility.Hidden;
                return;
            }
            else if (GuestCount.Value == 0)
            {
                LblGuestNumber.Visibility = Visibility.Visible;
                LblPartCreated.Visibility = Visibility.Hidden;
                LblCheck.Visibility = Visibility.Hidden;
                return;
            }
            LblPartCreated.Visibility = Visibility.Visible;
            LblGuestNumber.Visibility = Visibility.Hidden;
            LblCheck.Visibility = Visibility.Hidden;
            _viewModel.CreatePart();
            if (_viewModel.CustomTourRequests.Count() >= 2) BtnCreation.IsEnabled = true;
        }
    }
}