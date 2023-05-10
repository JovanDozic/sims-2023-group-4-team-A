using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.CustomControls
{
    public partial class StarRating : UserControl
    {
        public StarRating()
        {
            InitializeComponent();
            Stars = new ObservableCollection<string>();
            UpdateStars();
        }

        public static readonly DependencyProperty ControlHeightProperty =
            DependencyProperty.Register("ControlHeight", typeof(double), typeof(StarRating),
                new FrameworkPropertyMetadata(50.0, FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((StarRating)d).UpdateStars()));

        public double ControlHeight
        {
            get => (double)GetValue(ControlHeightProperty);
            set => SetValue(ControlHeightProperty, value);
        }

        public static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register("Rating", typeof(double), typeof(StarRating),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((StarRating)d).UpdateStars()));

        public double Rating
        {
            get => (double)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        public ObservableCollection<string> Stars { get; set; }


        private void UpdateStars()
        {
            Stars.Clear();
            int fullStars = (int)Rating;
            double fractionalStar = Rating - fullStars;
            string starFull = "/Resources/Icons/star-fill.png";
            string starHalf = "/Resources/Icons/star-half.png";
            string starEmpty = "/Resources/Icons/star.png";

            for (int i = 0; i < fullStars; i++) Stars.Add(starFull);

            if (fractionalStar >= 0.75 && fullStars < 5) Stars.Add(starFull);
            else if (fractionalStar >= 0.25 && fullStars < 5) Stars.Add(starHalf);
            else if (fullStars < 5) Stars.Add(starEmpty);

            for (int i = Stars.Count; i < 5; i++) Stars.Add(starEmpty);
        }

    }
}
