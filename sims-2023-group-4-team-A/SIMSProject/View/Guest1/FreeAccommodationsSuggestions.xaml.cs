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

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for FreeAccommodationsSuggestions.xaml
    /// </summary>
    public partial class FreeAccommodationsSuggestions : Window
    {
        public DateRange ConflictedRange { get; set; }
        public DateRange ReservedRange { get; set; }
        public int DaysNumber { get; set; }
        public ObservableCollection<DateRange> DateRanges { get; set; }
        public DateRange SelectedRange { get; set; } = new();
        public AccommodationReservation AccommodationReservation { get; set; } = new();
        public Accommodation Accommodation { get; set; }
        public Guest User { get; set; } = new();
        public AccommodationReservationController Controller { get; set; } = new();

        public FreeAccommodationsSuggestions(DateRange dateRange, DateRange reservedRange, int numberOfDays, Guest user, AccommodationReservation accommodationReservation, Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = this;
            DateRanges = new ObservableCollection<DateRange>();
            ConflictedRange = dateRange;
            DaysNumber = numberOfDays;
            ReservedRange = reservedRange;
            User = user;
            Accommodation = accommodation;
            AccommodationReservation = accommodationReservation;
            List<DateRange> availableDates = GetAvailableDateRange(ConflictedRange.StartDate, ConflictedRange.EndDate, ReservedRange.StartDate, ReservedRange.EndDate, DaysNumber);
            ShowDateRangeInDataGrid(availableDates);
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public List<DateRange> GetAvailableDateRange(DateTime conflictingStartDate, DateTime conflictingEndDate, DateTime reservedStartDate, DateTime reservedEndDate, int numDays)
        {
            List<DateRange> availableDateRange = new List<DateRange>();
            DateTime startDate = conflictingStartDate;
            DateTime endDate = conflictingEndDate;

            int maxExtends = 7;
            int extendCount = 0;

            while (extendCount < maxExtends)
            {
                bool conflict = (startDate.AddDays(numDays) < reservedStartDate || endDate.AddDays(-numDays) > reservedEndDate);

                if (conflict)
                {
                    if(startDate.AddDays(numDays) < reservedStartDate)
                    {
                        DateRanges.Add(new DateRange(startDate, startDate.AddDays(numDays)));
                        break;
                    }
                    else
                    {
                        DateRanges.Add(new DateRange(endDate.AddDays(-numDays), endDate));
                        break;
                    }
                    
                }
                 startDate = startDate.AddDays(-1);
                 endDate = endDate.AddDays(1);
 
                 extendCount++;
            }
            
            return availableDateRange;
        }
 
        private void ShowDateRangeInDataGrid(List<DateRange> availableDates)
        {
  
            DataGridDates.ItemsSource = DateRanges;
            DataGridDates.Items.Refresh();
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            int guestNumber = GuestsBox.Value;

            if(guestNumber <= Accommodation.MaxGuestNumber)
            {
                AccommodationReservation = new AccommodationReservation(AccommodationReservation.Accommodation.Id, User.Id, SelectedRange.StartDate, SelectedRange.EndDate, DaysNumber, guestNumber);

                Controller.Create(AccommodationReservation);
                MessageBox.Show("Smeštaj rezervisan!");
                Close();
            }
            else
            {
                MessageBox.Show("Broj gostiju nije prihvatljiv!");
            }
            
            

        }
    }
}
