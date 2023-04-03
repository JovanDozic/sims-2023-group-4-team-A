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
    /// Interaction logic for GuideAllToursWindow.xaml
    /// </summary>
    public partial class GuideAllToursWindow : Window
    {

        public ObservableCollection<Tour> AllTours { get; set; }
        public Tour SelectedTour { get; set; } = new();

        public GuideAllToursWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            AllTours = new(GuideInitialWindow.tourController.GetAll());

        }

        private void Show_all_appointmentsBTN_Click(object sender, RoutedEventArgs e)
        {
            AllAppointmentsByTourWindow window = new(SelectedTour.Id);
            window.Show();
        }
    }
}
