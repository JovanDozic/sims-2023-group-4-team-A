﻿using System;
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

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for MovingReservations.xaml
    /// </summary>
    public partial class MovingReservations : Window
    {
        public AccommodationReservation AccommodationReservation { get; set; }
        public MovingReservations(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            AccommodationReservation = accommodationReservation;
            ShowNameAndDate();
        }

        public void ShowNameAndDate()
        {
            NameBlock.Text = AccommodationReservation.Accommodation.Name;
            DateBlock.Text = AccommodationReservation.StartDate.ToString("dd/MM/yyyy.") + " - " 
                + AccommodationReservation.EndDate.ToString("dd/MM/yyyy.");
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            DateTime date = DateFrom.SelectedDate.Value.AddDays(AccommodationReservation.NumberOfDays);
            DateTo.SelectedDate = date;
        }
        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            DateTime date = DateTo.SelectedDate.Value.AddDays(-AccommodationReservation.NumberOfDays);
            DateFrom.SelectedDate = date;
        }
        private void DatePicker1_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.DisplayDateStart = DateTime.Today;
            }
        }
        private void DatePicker2_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.DisplayDateStart = DateTime.Today.AddDays(AccommodationReservation.NumberOfDays);
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
