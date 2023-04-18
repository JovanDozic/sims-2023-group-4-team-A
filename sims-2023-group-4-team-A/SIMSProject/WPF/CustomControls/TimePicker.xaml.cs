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
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl
    {
        public static readonly DependencyProperty HoursProperty = 
            DependencyProperty.Register(nameof(Hours), typeof(int), typeof(TimePicker), new FrameworkPropertyMetadata(0));

        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register(nameof(Minutes), typeof(int), typeof(TimePicker), new FrameworkPropertyMetadata(1));

        public int Hours
        {
            get => (int)GetValue(HoursProperty);
            set => SetValue(HoursProperty, value);
        }

        public int Minutes
        {
            get =>(int)GetValue(MinutesProperty);
            set => SetValue(MinutesProperty, value);
        }
        

        public TimePicker()
        {
            InitializeComponent();
        }

        private void BtnHM_Click(object sender, RoutedEventArgs e)
        {
            BtnHM.Content = BtnHM.Content.Equals("H") ? "M" : "H";
        }

        private void Increment_Click(object sender, RoutedEventArgs e)
        {
            switch (BtnHM.Content)
            {
                case "H": Hours++; break;
                case "M": Minutes++; break;
            }
        }

        private void Decrement_Click(object sender, RoutedEventArgs e)
        {
            switch (BtnHM.Content)
            {
                case "H": Hours--; break;
                case "M": Minutes--; break;
            }
        }

        private void TbHours_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void TbHours_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (BtnHM.Content.Equals("H"))
            {
                if (e.Key == Key.Up)
                {
                    Hours++;
                    e.Handled = true;
                }
                else if (e.Key == Key.Down)
                {
                    Hours--;
                    e.Handled = true;
                }
            }
        }

        private void TbMinutes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void TbMinutes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(BtnHM.Content.Equals("M"))
            {
                if (e.Key == Key.Up)
                {
                    Minutes++;
                    e.Handled = true;
                }
                else if (e.Key == Key.Down)
                {
                    Minutes--;
                    e.Handled = true;
                }
            }
        }

        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out var _);
        }
    }
}
