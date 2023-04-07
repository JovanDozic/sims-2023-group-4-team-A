using SIMSProject.Controller;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
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
using SIMSProject.Model.UserModel;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModel.TourViewModels;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    ///   
    public partial class TourCreation : Window
    {


        private Location? _selectedLocation;
        private bool _imageAdded;

        public TourViewModel View { get; set; } = new();

        public TourCreation(Guide guide)
        {
            InitializeComponent();
            this.DataContext = View;

            View.Guide = guide;
            View.GuideId = guide.Id;

            TBTime.Text = "hh:mm";

        }
        
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (View.Keys.Count < 2)
            {
                MessageBox.Show("Morate uneti najmanje 2 ključne tačke!");
                return;
            }
            else if (View.Images.Count == 0)
            {
                MessageBox.Show("Morate dodati bar 1 sliku.");
                return;
            }

            View.CreateTour();

            //CreateAndAssignTourAppointment(createdTour);
            //CreateNewPairs(createdTour);

            MessageBox.Show("Tura uspešno kreirana.");
            Close();
        }

        private void CreateNewPairs(Tour createdTour)
        {
            foreach (var keyPoint in View.Keys)
            {
                GuideInitialWindow.tourKeyPointController.Create(new(createdTour.Id, keyPoint.Id));//servis
            }
        }

        private void CreateAndAssignTourAppointment(Tour createdTour) //isto mora servis
        {
            foreach (var appointment in View.Appointments)
            {
                TourAppointment createdAppointment = GuideInitialWindow.tourAppointmentController.Create(appointment);
                TourAppointment updatedAppointment = GuideInitialWindow.tourAppointmentController.InitializeTour(createdAppointment, createdTour);
            }
        }

        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            if (View.SelectedKeyPoint == null)
            {
                MessageBox.Show("Nije moguće izabrati ključnu tačku koja ne postoji!");
                return;
            }
            View.Keys.Add(View.SelectedKeyPoint);
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            View.Appointments.Add(new(CreateAppointment(), -1, View.MaxGuestNumber, -1));
        }

        private DateTime CreateAppointment()
        {
            string[] timeParts = TBTime.Text.Split(":");
            int hours = int.Parse(timeParts[0]);
            int minutes = int.Parse(timeParts[1]);
            int seconds = 0;

            TBTime.Text = "hh:mm";
            DateTime newDate = new(View.SelectedAppointment.Year, View.SelectedAppointment.Month, View.SelectedAppointment.Day, hours, minutes, seconds);
            return newDate;
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
