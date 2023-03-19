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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }

        public AccommodationReservationConfirmation(Guest user, Accommodation accommodation)
        {
            InitializeComponent();
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
            int Days = DaysBox.Value;
            DateRange selectedDateRange = DatesCombo.SelectedItem as DateRange;

            int guestsNumber = GuestsBox.Value;

            if(guestsNumber <= Accommodation.MaxGuestNumber)
            {
                if (selectedDateRange != null)
                {
                    AccommodationReservation = new AccommodationReservation(Accommodation.Id, User.Id, selectedDateRange.StartDate, selectedDateRange.EndDate, Days, guestsNumber);

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
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateBegin = DateBeginBox.SelectedDate ?? DateTime.MinValue;
            DateTime dateEnd = DateEndBox.SelectedDate ?? DateTime.MinValue;
            int Days = DaysBox.Value;
            TimeSpan duration = dateEnd - dateBegin;
            AccommodationReservation reserved = CheckReservations(Controller.GetAll(), dateBegin, dateEnd, Accommodation.Id);

            if (Days >= Accommodation.MinReservationDays && Days <= duration.Days)
            {
                if (reserved != null)
                {
                    DateRange conflictingRange = new DateRange(dateBegin, dateEnd);
                    DateRange reservedRange = new DateRange(reserved.StartDate, reserved.EndDate);
                    var confirm = new FreeAccommodationsSuggestions(conflictingRange, reservedRange, Days, User, reserved, Accommodation);
                    confirm.Show();
                }
                else
                {
                    AddPossibleDates(dateBegin, dateEnd, Days);

                }
            }
            else
            {
                MessageBox.Show("Broj dana nije prihvatljiv!");
            }

        }

        public AccommodationReservation CheckReservations(List<AccommodationReservation> reservations, DateTime startDate, DateTime endDate, int accommodationId)
        {
            var conflictingReservation =  reservations.FirstOrDefault(r => r.AccommodationId == accommodationId && (startDate < r.EndDate && r.StartDate < endDate));

            return conflictingReservation;
        }

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
