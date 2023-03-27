using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMSProject.CustomControls
{
    public partial class NumericTextBox : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericTextBox), new FrameworkPropertyMetadata(0));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public NumericTextBox()
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