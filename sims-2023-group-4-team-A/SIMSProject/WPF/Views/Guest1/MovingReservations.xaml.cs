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
using SIMSProject.Domain.Models.AccommodationModels;
using System.Windows.Shapes;
using SIMSProject.Model;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;

namespace SIMSProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for MovingReservations.xaml
    /// </summary>
    public partial class MovingReservations : Window
    {
        private readonly User User = new();
        private readonly ReschedulingRequestViewModel _reschedulingRequestViewModel;
        private AccommodationReservation _accommodationReservation { get; set; }
        public MovingReservations(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            _accommodationReservation = accommodationReservation;
            User = new();
            _reschedulingRequestViewModel = new(User, _accommodationReservation);
            DataContext = _reschedulingRequestViewModel;            
            ShowNameAndDate();
        }

        public void ShowNameAndDate()
        {
            NameBlock.Text = _reschedulingRequestViewModel.DisplayName();
            DateBlock.Text = _reschedulingRequestViewModel.DisplayDate();
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateFrom.SelectedDate.HasValue && DateFrom.SelectedDate.Value != DateTime.MinValue)
            {
                DateTime date = DateFrom.SelectedDate.Value.AddDays(_reschedulingRequestViewModel.AddDays());
                DateTo.SelectedDate = date;
            }

        }
        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateTo.SelectedDate.HasValue && DateTo.SelectedDate.Value != DateTime.MinValue)
            {
                DateTime date = DateTo.SelectedDate.Value.AddDays(_reschedulingRequestViewModel.SubDays());
                DateFrom.SelectedDate = date;
            }
        }
        private void DatePicker1_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.DisplayDateStart = DateTime.Today.AddDays(1);
            }
        }
        private void DatePicker2_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.DisplayDateStart = DateTime.Today.AddDays(_reschedulingRequestViewModel.AddDays() + 1);
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_SendRequest(object sender, RoutedEventArgs e)
        {
            _reschedulingRequestViewModel.SendRequest();
            MessageBox.Show("Zahtev uspešno poslat!");
            Close();
        }
    }
}
