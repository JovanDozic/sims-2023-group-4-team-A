using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIMSProject.Observer;
using SIMSProject.Model.UserModel;
using SIMSProject.Controller;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for FreeAccommodationsSuggestions.xaml
    /// </summary>
    public partial class FreeAccommodationsSuggestions : Window, INotifyPropertyChanged
    {
        public DateRange ConflictedRange { get; set; }
        public DateRange ReservedRange { get; set; } 
        public int DaysNumber { get; set; }
        public ObservableCollection<DateRange> DateRanges { get; set; } = null;
        public DateRange SelectedRange { get; set; } = null;
        public List<DateRange> ReservedDates { get; set; } = new();
        public AccommodationReservation AccommodationReservation { get; set; } = new();
        public Accommodation Accommodation { get; set; }
        public Guest User { get; set; } = new();
        public AccommodationReservationController Controller { get; set; } = new();

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

        public FreeAccommodationsSuggestions(DateRange dateRange, List<DateRange> reservedDates, int numberOfDays, Guest user, AccommodationReservation accommodationReservation, Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = this;
            DateRanges = new ObservableCollection<DateRange>();
            ConflictedRange = dateRange;
            DaysNumber = numberOfDays;
            ReservedDates = reservedDates;
            User = user;
            Accommodation = accommodation;
            AccommodationReservation = accommodationReservation;
            GetAvailableDateRange(ConflictedRange.StartDate, ConflictedRange.EndDate,ReservedDates, DaysNumber);
            ShowDateRangeInDataGrid();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //function that gets all available date ranges
        public void GetAvailableDateRange(DateTime conflictingStartDate, DateTime conflictingEndDate, List<DateRange> dateranges, int numDays)
        {
            DateTime today = DateTime.Today;
            DateTime startDate = conflictingStartDate.AddDays(-1);
            DateTime endDate = conflictingEndDate.AddDays(1);

            int maxExtends = 7; //range extends max to 7 days
            int extendCount = 0;

            while (extendCount < maxExtends)
            {
                bool isAvailableBeforeConflict = CheckAvailability(startDate, startDate.AddDays(numDays), dateranges);
                bool isAvailableAfterConflict = CheckAvailability(endDate.AddDays(-numDays), endDate, dateranges);

               if (isAvailableBeforeConflict && isAvailableAfterConflict)
                {
                    DateRanges.Add(new DateRange(startDate, startDate.AddDays(numDays)));
                    DateRanges.Add(new DateRange(endDate.AddDays(-numDays), endDate));
                    break;
                }

               else if(isAvailableBeforeConflict)
                {
                    DateRanges.Add(new DateRange(startDate, startDate.AddDays(numDays)));
                    break;
                }
                
               else if(isAvailableAfterConflict)
                {
                    DateRanges.Add(new DateRange(endDate.AddDays(-numDays), endDate));
                    break;
                }

                if (startDate > today)
                {
                    startDate = startDate.AddDays(-1);
                }

                endDate = endDate.AddDays(1);
                extendCount++;
            }
                
        }

        //function that checks if input date range is available
        public static bool CheckAvailability(DateTime inputStartDate, DateTime inputEndDate, List<DateRange> reservedDates)
        {
            foreach (var reservedDate in reservedDates)
            {
                if (inputStartDate <= reservedDate.EndDate && reservedDate.StartDate <= inputEndDate)
                {
                    return false;
                }
            }
            return true;
        }

        //function for binding with data grid
        private void ShowDateRangeInDataGrid()
        {
  
            DataGridDates.ItemsSource = DateRanges;
            DataGridDates.Items.Refresh();
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            GuestsNumber = GuestsBox.Value;

            if (SelectedRange != null)
            {
                if (GuestsNumber <= Accommodation.MaxGuestNumber)
                {
                    AccommodationReservation = new AccommodationReservation(AccommodationReservation.Accommodation.Id, User.Id, SelectedRange.StartDate, SelectedRange.EndDate, DaysNumber, GuestsNumber, false);

                    Controller.Create(AccommodationReservation);
                    MessageBox.Show("Smeštaj rezervisan!");
                    Close();
                }
                else
                {
                    MessageBox.Show("Broj gostiju nije prihvatljiv!");
                }

            }
            else
                MessageBox.Show("Niste odabrali datum!");
 
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
