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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMSProject.WPF.CustomControls
{
    /// <summary>
    /// Interaction logic for DateRangePicker.xaml
    /// </summary>
    public partial class DateRangePicker : UserControl
    {
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register(nameof(StartDate), typeof(DateTime), typeof(DateRangePicker), new PropertyMetadata(DateTime.Now));

        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register(nameof(EndDate), typeof(DateTime), typeof(DateRangePicker), new PropertyMetadata(DateTime.Now));

        public DateTime? StartDate
        {
            get => (DateTime)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }
        public DateTime? EndDate
        {
            get => (DateTime)GetValue(EndProperty);
            set => SetValue(EndProperty, value);
        }
        public DateRangePicker()
        {
            InitializeComponent();
        }

        private void btnShowCalendar_Click(object sender, RoutedEventArgs e)
        {
            ppCalendar.IsOpen = true;
        }

        private void clndr_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            StartDate = clndr.SelectedDates.FirstOrDefault();
            EndDate = clndr.SelectedDates.LastOrDefault();

            tbDateRange.Text = $"{StartDate:dd.MM.yy} - {EndDate:dd.MM.yy}";
            ppCalendar.IsOpen = false;
        }
    }
}
