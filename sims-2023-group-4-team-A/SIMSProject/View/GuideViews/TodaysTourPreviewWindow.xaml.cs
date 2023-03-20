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
        public Tour SelectedTour { get; set; } = new();
        public TourDate SelectedDate { get; set; } = new();
        public List<TourDate> TodaysDates { get; set; } = new();
        private readonly TourDateController TourDateController = new();


        public TodaysTourPreviewWindow(Tour Tour)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedTour = Tour;

            GetTodaysAppointments();
        }

        private void GetTodaysAppointments()
        {
            foreach (TourDate date in TourDateController.FindTodaysByTour(SelectedTour.Id))
            {
                TodaysDates.Add(date);
            }
        }

        private void LiveTrackBTN_Click(object sender, RoutedEventArgs e)
        {

            TourDate? activeDate = TodaysDates.Find(x => x.TourStatus.Equals("Aktivna"));
            if (activeDate != null)
            {
                if (activeDate.Id != SelectedDate.Id)
                {
                    MessageBox.Show("Već postoji aktivna tura!");
                    return;
                }
                //Samo nastavi turu

            }
            //Napravi novu turu
            LiveTrackTourDate();
        }

        private void LiveTrackTourDate()
        {
            TourDate LiveTrackingDate = SelectedDate;
            LiveTrackingDate.CurrentKeyPointId = SelectedTour.KeyPoints[0].Id;
            LiveTrackingDate.CurrentKeyPoint = SelectedTour.KeyPoints[0];
            TourDateController.StartTourLiveTracking(LiveTrackingDate);

            TourLiveTrackingWindow window = new TourLiveTrackingWindow(LiveTrackingDate);
            window.Show();
        }
    }
}
