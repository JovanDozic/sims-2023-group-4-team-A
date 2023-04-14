using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels;
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
        private TourCreationViewModel _viewModel { get; set; }

        public TourCreation(Guide guide)
        {
            InitializeComponent();
            _viewModel = new(guide);
            this.DataContext = _viewModel;
            TBTime.Text = "hh:mm";
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.IsValid())
            {
                MessageBox.Show("Ne valja");
                return;
            }
            _viewModel.CreateTour();
            Close();
        }
        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddKeyPoint();
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Appointments.Add(new(CreateDate(), -1, _viewModel.MaxGuestNumber, -1));
        }

        private DateTime CreateDate()
        {
            int year = _viewModel.SelectedAppointment.Year;
            int month = _viewModel.SelectedAppointment.Month;
            int day = _viewModel.SelectedAppointment.Day;
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
            _viewModel.Images.Add(ImageURLs.Text);
            _imageAdded = true;
        }
    }
}
