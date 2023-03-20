using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.CustomControls
{
    public partial class RatingInput : UserControl
    {
        public static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register("Rating", typeof(int), typeof(RatingInput), new PropertyMetadata(0));

        public int Rating
        {
            get => (int)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        public RatingInput()
        {
            InitializeComponent();
            star1.IsChecked = true;
            Rating = 1;
        }

        private void Star1_Click(object? sender, RoutedEventArgs? e)
        {
            Rating = 1;
            star2.IsChecked = false;
            star3.IsChecked = false;
            star4.IsChecked = false;
            star5.IsChecked = false;
        }

        private void Star2_Click(object sender, RoutedEventArgs e)
        {
            Rating = 2;
            star1.IsChecked = true;
            star3.IsChecked = false;
            star4.IsChecked = false;
            star5.IsChecked = false;
        }

        private void Star3_Click(object sender, RoutedEventArgs e)
        {
            Rating = 3;
            star1.IsChecked = true;
            star2.IsChecked = true;
            star4.IsChecked = false;
            star5.IsChecked = false;
        }

        private void Star4_Click(object sender, RoutedEventArgs e)
        {
            Rating = 4;
            star1.IsChecked = true;
            star2.IsChecked = true;
            star3.IsChecked = true;
            star5.IsChecked = false;
        }

        private void Star5_Click(object sender, RoutedEventArgs e)
        {
            Rating = 5;
            star1.IsChecked = true;
            star2.IsChecked = true;
            star3.IsChecked = true;
            star4.IsChecked = true;
        }
    }
}