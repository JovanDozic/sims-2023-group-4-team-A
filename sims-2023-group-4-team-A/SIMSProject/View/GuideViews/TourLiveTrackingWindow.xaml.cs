using SIMSProject.Controller;
using SIMSProject.Model;
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
    /// Interaction logic for TourLiveTrackingWindow.xaml
    /// </summary>
    public partial class TourLiveTrackingWindow : Window
    {
        private readonly TourController tourController = new();

        public ObservableCollection<KeyPoint> KeyPoints { get; set; }

        public TourLiveTrackingWindow(TourDate Date)
        {
            InitializeComponent();
            this.DataContext = this;

            KeyPoints = new();
            Tour? tour = tourController.GetAll().Find(x => x.Id == Date.TourId);

            foreach(var key in tour.KeyPoints)
            {
                KeyPoints.Add(key);
            }
        }
    }
}
