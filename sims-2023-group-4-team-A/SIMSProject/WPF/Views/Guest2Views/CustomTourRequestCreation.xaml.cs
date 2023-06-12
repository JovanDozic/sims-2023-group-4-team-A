using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for CustomTourRequestCreation.xaml
    /// </summary>
    public partial class CustomTourRequestCreation : Page
    {
        public Guest2 _user = new();
        private CustomTourRequestCreationViewModel _viewModel;
        public CustomTourRequestCreation(Guest2 user, NavigationService navigationService)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new CustomTourRequestCreationViewModel(_user, navigationService);
            this.DataContext = _viewModel;
            CbLanguage.SelectedItem = null;
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
            if ((CbLocation.Text == string.Empty || Description.Text == string.Empty || CbLanguage.Text == string.Empty || DateFrom.Text == string.Empty || DateTo.Text == string.Empty)) 
            {
                LblCheck.Visibility = Visibility.Visible;
                return;
            }
            else if (GuestCount.Value == 0)
            {
                LblGuestNumber.Visibility = Visibility.Visible;
                LblCheck.Visibility = Visibility.Hidden;
                return;
            }
            LblRequestCreated.Visibility = Visibility.Visible;
            LblGuestNumber.Visibility = Visibility.Hidden;
            LblCheck.Visibility = Visibility.Hidden;
            BtnCreation.IsEnabled = false;
            _viewModel.CreateRequest();
        }

    }
}
