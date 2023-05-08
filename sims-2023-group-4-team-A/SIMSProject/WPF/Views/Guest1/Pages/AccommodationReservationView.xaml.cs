using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using SIMSProject.WPF.ViewModels.Guest1ViewModels;
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
    /// Interaction logic for AccommodationReservation.xaml
    /// </summary>
    public partial class AccommodationReservationView : Page
    {
        private AccommodationViewModel _accommodationViewModel;
        private AccommodationReservationViewModel _accommodationReservationViewModel;
        private ReservationViewModel _reservationViewModel;
        private readonly User _user = new();

        public AccommodationReservationView(AccommodationViewModel accommodationViewModel, User user)
        {
            InitializeComponent();
            _user = user;
            _accommodationReservationViewModel = new(_user);
            _accommodationViewModel = accommodationViewModel;
            _reservationViewModel = new(_accommodationReservationViewModel, _accommodationViewModel, _user);
            DataContext = _reservationViewModel;
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Reservation(object sender, RoutedEventArgs e)
        {
            DateValidation.Text = " ";
            DurationValidation.Text = " ";
            DaysValidation.Text = " ";
            GuestsValidation.Text = " ";
            DateTime dateBegin = DateBeginBox.SelectedDate ?? DateTime.MinValue;
            DateTime dateEnd = DateEndBox.SelectedDate ?? DateTime.MinValue;
            int NumberOfDays = DaysNum.Value;
            int GuestsNumber = GuestsNum.Value;
            TimeSpan duration = dateEnd - dateBegin;

            if (!_accommodationViewModel.IsDateInPast(dateBegin, dateEnd))
            {
                if (_accommodationViewModel.IsNumberOfDaysValid(NumberOfDays))
                {
                    if (_accommodationViewModel.IsNumberOfDaysGreaterThanDuration(NumberOfDays, duration))
                    {
                        if (_accommodationViewModel.IsGuestsNumberValid(GuestsNumber))
                        {
                            if (_accommodationViewModel.IsAccommodationOccupied(dateBegin, dateEnd))
                            {
                                if (_accommodationViewModel.IsCanceled(dateBegin, dateEnd))
                                {
                                    NavigationService.Navigate(new FreeReservationDates(_reservationViewModel));
                                }
                                else
                                {
                                    NavigationService.Navigate(new AlternativeReservationDates(_reservationViewModel));
                                }
                            }
                            else
                            {
                                NavigationService.Navigate(new FreeReservationDates(_reservationViewModel));
                            }
                        }
                        else
                            GuestsValidation.Text = _accommodationViewModel.GetGuestsMessage();
                    }
                    else
                        DurationValidation.Text = _accommodationViewModel.GetDaysDurationMessage();
                }
                else
                    DaysValidation.Text = _accommodationViewModel.GetDaysMessage();
            }
            else
                DateValidation.Text = _accommodationViewModel.GetDateMessage();
        }

        private void DatePickerStart_Loaded(object sender, RoutedEventArgs e)
        {
                DatePicker datePicker = sender as DatePicker;
                if (datePicker != null)
                {
                    datePicker.SelectedDate = null;
                    datePicker.DisplayDateStart = DateTime.Today;
                }     
        }

        private void DatePickerEnd_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.SelectedDate = null;
                datePicker.DisplayDateStart = DateTime.Today.AddDays(1);
            }
        }

    }
}
