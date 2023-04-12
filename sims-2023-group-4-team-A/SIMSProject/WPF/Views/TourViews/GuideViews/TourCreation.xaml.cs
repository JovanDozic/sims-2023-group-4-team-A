using SIMSProject.Model.UserModel;
using SIMSProject.WPF.ViewModel.TourViewModels;
using System;
using System.Windows;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    ///   
    public partial class TourCreation : Window
    {
        private bool _imageAdded;
        private TourCreationViewModel View { get; set; }

        public TourCreation(Guide guide)
        {
            InitializeComponent();
            View = new(guide);
            this.DataContext = View;
            TBTime.Text = "hh:mm";
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!View.IsValid())
            {
                MessageBox.Show("Ne valja");
                return;
            }
            View.CreateTour();
            Close();
        }
        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            View.AddKeyPoint();
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            View.Appointments.Add(new(CreateDate(), -1, View.MaxGuestNumber, -1));
        }

        private DateTime CreateDate()
        {
            int year = View.SelectedAppointment.Year;
            int month = View.SelectedAppointment.Month;
            int day = View.SelectedAppointment.Day;
            int hours, minutes, seconds;
            CalculateTime(out hours, out minutes, out seconds);
            DateTime newDate = new(year, month, day, hours, minutes, seconds);
            TBTime.Text = "hh:mm";
            return newDate;
        }
        private void CalculateTime(out int hours, out int minutes, out int seconds)
        {
            string[] timeParts = TBTime.Text.Split(":");
            hours = int.Parse(timeParts[0]);
            minutes = int.Parse(timeParts[1]);
            seconds = 0;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ImageURLs_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_imageAdded)
            {
                _imageAdded = false;
                ImageURLs.Text = string.Empty;
            }
        }
        private void BTNUploadFiles_Click(object sender, RoutedEventArgs e)
        {
            View.Images.Add(ImageURLs.Text);
            _imageAdded = true;
        }
    }
}
