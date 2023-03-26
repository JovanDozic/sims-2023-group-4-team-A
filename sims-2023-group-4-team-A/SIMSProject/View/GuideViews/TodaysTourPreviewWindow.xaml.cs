using SIMSProject.Controller;
using SIMSProject.Model;
using SIMSProject.Observer;
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

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TodaysTourPreviewWindow.xaml
    /// </summary>
    public partial class TodaysTourPreviewWindow : Window, IObserver
    {

        public Tour TodaysTour { get; set; } = new();
        public TourAppointment SelectedAppointment { get; set; } = new();
        public ObservableCollection<TourAppointment> TodaysAppointments { get; set; } = new();


        public TodaysTourPreviewWindow(Tour TodaysTour)
        {
            InitializeComponent();
            this.DataContext = this;
            this.TodaysTour = TodaysTour;

            GuideInitialWindow.tourController.Subscribe(this);
            GuideInitialWindow.tourAppointmentController.Subscribe(this);

            GetTodaysAppointments();
        }

        private void GetTodaysAppointments()
        {
            TodaysAppointments.Clear();
            foreach (TourAppointment appointment in GuideInitialWindow.tourAppointmentController.FindTodaysByTour(TodaysTour.Id))
            {
                TodaysAppointments.Add(appointment);
            }
        }

        private void LiveTrackBTN_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment.TourStatus.Equals("Završena"))
            {
                MessageBox.Show("Ne možete otpočeti turu koja se završila!");
                return;
            }

            TourAppointment? activeAppointment = TodaysAppointments.ToList().Find(x => x.TourStatus.Equals("Aktivna"));
            if (activeAppointment != null)
            {
                if (activeAppointment.Id != SelectedAppointment.Id)
                {
                    MessageBox.Show("Već postoji aktivna tura!");
                    return;
                }
                TourLiveTrackingWindow window = new(SelectedAppointment);
                window.Show();

            }
            else
            {
                StartLiveTracking();
            }
        }

        private void StartLiveTracking()
        {
            SetAppointmentStartPoint();
            GuideInitialWindow.tourAppointmentController.StartTourLiveTracking(SelectedAppointment);

            TourLiveTrackingWindow window = new(SelectedAppointment);
            window.Show();
        }

        private void SetAppointmentStartPoint()
        {
            SelectedAppointment.CurrentKeyPointId = TodaysTour.KeyPoints[0].Id;
            SelectedAppointment.CurrentKeyPoint = TodaysTour.KeyPoints[0];
        }

        public void Update()
        {
            GetTodaysAppointments();
        }
    }
}
