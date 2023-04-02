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
using System.Windows.Shapes;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using SIMSProject.Controller;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmation.xaml
    /// </summary>
    public partial class AccommodationReservationConfirmation : Window, INotifyPropertyChanged
    {
        public Guest User { get; set; } = new();
        public Accommodation Accommodation { get; set; } = new();
        List<AccommodationReservation> AccommodationReservations { get; set; } = new List<AccommodationReservation>();
        public AccommodationReservationController Controller { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; } = new();
        public DateTime[] DateRange { get; set; }
        public string FormattedDateRange => $"{DateRange[0]:dd/MM/yyyy} - {DateRange[1]:dd/MM/yyyy}";
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        private int _guestsNumber = 1;
        public int GuestsNumber
        {
            get => _guestsNumber;

            set
            {
                if (value != _guestsNumber && value >= 1)
                {
                    _guestsNumber = value;
                    OnPropertyChanged(nameof(GuestsNumber));
                }
            }
        }
        private int _numberOfDays = 1;
        public int NumberOfDays
        {
            get => _numberOfDays;

            set
            {
                if (value != _numberOfDays && value >= 1)
                {
                    _numberOfDays = value;
                    OnPropertyChanged(nameof(NumberOfDays));
                }
            }
        }

        public AccommodationReservationConfirmation(Guest user, Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = this;
            Accommodation = accommodation;
            Controller = new AccommodationReservationController();
            NameTextBlock.Text = Accommodation.Name;
            User = user;
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Button(object sender, RoutedEventArgs e)
        {
            DateTime dateBegin = DateBeginBox.SelectedDate ?? DateTime.MinValue;
            DateTime dateEnd = DateEndBox.SelectedDate ?? DateTime.MinValue;
            NumberOfDays = DaysBox.Value;
            DateRange selectedDateRange = DatesCombo.SelectedItem as DateRange; //selected date range from combo box
            GuestsNumber = GuestsBox.Value;

            if (dateEnd > dateBegin)
            {
                if (GuestsNumber <= Accommodation.MaxGuestNumber)
                {
                    if (selectedDateRange != null)
                    {
                        AccommodationReservation = new AccommodationReservation(Accommodation.Id, User.Id, selectedDateRange.StartDate, selectedDateRange.EndDate, NumberOfDays, GuestsNumber, false);

                        Controller.Create(AccommodationReservation);
                        MessageBox.Show("Smeštaj rezervisan!");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Niste odabrali datum");
                    }
                }
                else
                {
                    MessageBox.Show("Broj gostiju nije prihvatljiv");
                }

            }
            else
                MessageBox.Show("Datum odlaska mora biti veći od datuma dolaska");
             
        }
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateBegin = DateBeginBox.SelectedDate ?? DateTime.MinValue;
            DateTime dateEnd = DateEndBox.SelectedDate ?? DateTime.MinValue;
            NumberOfDays = DaysBox.Value;
            TimeSpan duration = dateEnd - dateBegin; //number of days between 2 dates
            AccommodationReservation reserved = CheckReservations(Controller.GetAll(), dateBegin, dateEnd, Accommodation.Id);

            if (dateEnd > dateBegin)
            {
                if (NumberOfDays >= Accommodation.MinReservationDays && NumberOfDays <= duration.Days)
                {
                    if (reserved != null)
                    {
                        List<AccommodationReservation> reservations = GetReservations(Controller.GetAll(), Accommodation.Id);
                        List<DateRange> reservedDates = GetReservationDates(reservations);
                        DateRange conflictingRange = new DateRange(dateBegin, dateEnd);
                       
                        if (reserved.Canceled)
                         AddPossibleDates(dateBegin, dateEnd, NumberOfDays);

                        else
                        {
                            var show = new FreeAccommodationsSuggestions(conflictingRange, reservedDates, NumberOfDays, User, AccommodationReservation, Accommodation);
                            show.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Broj dana nije prihvatljiv!");
                }
            }
            else
                MessageBox.Show("Datum odlaska mora biti veći od datuma dolaska");
        }

        //function that checks for free accommodations for a given date range
        public AccommodationReservation CheckReservations(List<AccommodationReservation> reservations, DateTime startDate, DateTime endDate, int accommodationId)
        {
            var conflictingReservation =  reservations.FirstOrDefault(r => r.Accommodation.Id == accommodationId && (startDate < r.EndDate && r.StartDate < endDate));

            return conflictingReservation; //if return value is null it means that accommodation is free in specific date range
        }

        //function that finds date ranges and adds them to combo box
        public void AddPossibleDates(DateTime startDate, DateTime endDate, int numberOfDays)
        {
            List<DateRange> availableRanges = new List<DateRange>();

            for (DateTime date = startDate; date <= endDate.AddDays(-numberOfDays); date = date.AddDays(1))
            {
                DateTime endDateRange = date.AddDays(numberOfDays);
                DateRange dateRange = new DateRange(date, endDateRange);
                availableRanges.Add(dateRange);
            }
            DatesCombo.ItemsSource = availableRanges;
        }

        //function that goes through reservations and return their dates
        public List<DateRange> GetReservationDates(List<AccommodationReservation> reservations)
        {
            List<DateRange> dateRanges = new List<DateRange>();

            foreach(AccommodationReservation reservation in reservations)
            {
                dateRanges.Add(new DateRange(reservation.StartDate, reservation.EndDate));
            }
            return dateRanges;
        }

        //function that returns all reservations for specific accommodation
        public List<AccommodationReservation> GetReservations(List<AccommodationReservation> reservations, int accommodationId)
        {
            List<AccommodationReservation> matchingReservations = new List<AccommodationReservation>();

            foreach(AccommodationReservation reservation in reservations)
            {
                if(reservation.Accommodation.Id == accommodationId)
                {
                    matchingReservations.Add(reservation);
                }
            }

            return matchingReservations;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.DisplayDateStart = DateTime.Today;
            }
        }
    }
}
