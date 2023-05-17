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
    /// Interaction logic for Guest2NumericBoxInput.xaml
    /// </summary>
    public partial class Guest2NumericBoxInput : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(Guest2NumericBoxInput), new FrameworkPropertyMetadata(0));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        public Guest2NumericBoxInput()
        {
            InitializeComponent();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Handle arrow keys to increment/decrement value
            if (e.Key == Key.Up)
            {
                Value++;
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                Value--;
                e.Handled = true;
            }
        }

        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out var _);
        }

        private void IncrementButton_Click(object sender, RoutedEventArgs e)
        {
            Value++;
        }

        private void DecrementButton_Click(object sender, RoutedEventArgs e)
        {
            Value--;
        }
    }
}
