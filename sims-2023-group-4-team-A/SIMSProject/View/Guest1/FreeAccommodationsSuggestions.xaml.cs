﻿using System;
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
        public ObservableCollection<DateRange> DateRanges { get; set; }
        public DateRange SelectedRange { get; set; } = new();
        public AccommodationReservation AccommodationReservation { get; set; } = new();
        public Accommodation Accommodation { get; set; }
        public Guest User { get; set; } = new();
        public AccommodationReservationController Controller { get; set; } = new();
        private int _guestsNumber;
        public int GuestsNumber
        {
            get => _guestsNumber;

            set
            {
                if (value != _guestsNumber && value >= 0)
                {
                    _guestsNumber = value;
                    OnPropertyChanged(nameof(GuestsNumber));
                }
            }
        }

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
            GetAvailableDateRange(ConflictedRange.StartDate, ConflictedRange.EndDate, ReservedRange.StartDate, ReservedRange.EndDate, DaysNumber);
            ShowDateRangeInDataGrid();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //function that collects available dates from extended date range
        public void GetAvailableDateRange(DateTime conflictingStartDate, DateTime conflictingEndDate, DateTime reservedStartDate, DateTime reservedEndDate, int numDays)
        {
            DateTime startDate = conflictingStartDate;
            DateTime endDate = conflictingEndDate;

            int maxExtends = 7; //range extends max to 7 days
            int extendCount = 0;

            while (extendCount < maxExtends)
            {
                bool free = (startDate.AddDays(numDays) < reservedStartDate || endDate.AddDays(-numDays) > reservedEndDate);

                if (free)
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

            if(GuestsNumber <= Accommodation.MaxGuestNumber)
            {
                AccommodationReservation = new AccommodationReservation(AccommodationReservation.Accommodation.Id, User.Id, SelectedRange.StartDate, SelectedRange.EndDate, DaysNumber, GuestsNumber);

                Controller.Create(AccommodationReservation);
                MessageBox.Show("Smeštaj rezervisan!");
                Close();
            }
            else
            {
                MessageBox.Show("Broj gostiju nije prihvatljiv!");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
