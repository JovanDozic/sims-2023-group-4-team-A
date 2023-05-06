using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
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
    /// Interaction logic for CustomTourRequestCreation.xaml
    /// </summary>
    public partial class CustomTourRequestCreation : Page
    {
        public Guest _user = new();
        private CustomTourRequestViewModel _viewModel;
        public CustomTourRequestCreation(Guest user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new CustomTourRequestViewModel(_user);
            //_viewModel.LoadTourRequestsByGuestId(_user.Id);
            this.DataContext = _viewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            //_viewModel.LoadDatePicker(sender);
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.SelectedDate = null;
                datePicker.DisplayDateStart = DateTime.Today;
            }
        }

        private void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CreateRequest();
        }

    }
}
