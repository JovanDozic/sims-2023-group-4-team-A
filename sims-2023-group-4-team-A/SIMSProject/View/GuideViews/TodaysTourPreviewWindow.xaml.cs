using SIMSProject.Controller;
using SIMSProject.Model;
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

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TodaysTourPreviewWindow.xaml
    /// </summary>
    public partial class TodaysTourPreviewWindow : Window
    {
        private readonly TourAppointmentController tourAppointmentController = new();

        public Tour TodaysTour { get; set; } = new();
        public TourAppointment SelectedAppointment { get; set; } = new();
        public List<TourAppointment> TodaysAppointments { get; set; } = new();


        public TodaysTourPreviewWindow(Tour TodaysTour)
        {
            InitializeComponent();
            this.DataContext = this;
            this.TodaysTour = TodaysTour;

            GetTodaysAppointments();
        }

        private void GetTodaysAppointments()
        {
            foreach (TourAppointment appointment in tourAppointmentController.FindTodaysByTour(TodaysTour.Id))
            {
                TodaysAppointments.Add(appointment);
            }
        }

        private void LiveTrackBTN_Click(object sender, RoutedEventArgs e)
        {

            TourAppointment? activeAppointment = TodaysAppointments.Find(x => x.TourStatus.Equals("Aktivna"));
            if (activeAppointment != null)
            {
                if (activeAppointment.Id != SelectedAppointment.Id)
                {
                    MessageBox.Show("Već postoji aktivna tura!");
                    return;
                }
                //Samo nastavi turu

            }
            //Napravi novu turu
            StartLiveTracking();
        }

        private void StartLiveTracking()
        {
            TourAppointment LiveTrackingDate = SelectedAppointment;
            LiveTrackingDate.CurrentKeyPointId = TodaysTour.KeyPoints[0].Id;
            LiveTrackingDate.CurrentKeyPoint = TodaysTour.KeyPoints[0];
            tourAppointmentController.StartTourLiveTracking(LiveTrackingDate);

            TourLiveTrackingWindow window = new TourLiveTrackingWindow(LiveTrackingDate);
            window.Show();
        }
    }
}
