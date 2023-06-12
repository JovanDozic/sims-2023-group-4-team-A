using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SIMSProject.WPF.CustomControls
{
    public partial class StarRatingInput : UserControl
    {
        public StarRatingInput()
        {
            InitializeComponent();
            Stars = new ObservableCollection<string>();
            UpdateStars();
        }

        public static readonly DependencyProperty ControlHeightProperty =
            DependencyProperty.Register("ControlHeight", typeof(double), typeof(StarRatingInput),
                new FrameworkPropertyMetadata(50.0, FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((StarRatingInput)d).UpdateStars()));

        public double ControlHeight
        {
            get => (double)GetValue(ControlHeightProperty);
            set => SetValue(ControlHeightProperty, value);
        }

        public static readonly DependencyProperty SelectedRatingProperty =
        DependencyProperty.Register("SelectedRating", typeof(int), typeof(StarRatingInput),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((StarRatingInput)d).UpdateStars()));

        public int SelectedRating
        {
            get => (int)GetValue(SelectedRatingProperty);
            set => SetValue(SelectedRatingProperty, value);
        }

        public ObservableCollection<string> Stars { get; set; }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;
            var contentPresenter = VisualTreeHelper.GetParent(image) as ContentPresenter;
            var uniformGrid = VisualTreeHelper.GetParent(contentPresenter) as UniformGrid;
            var index = uniformGrid.Children.IndexOf(contentPresenter);
            SelectedRating = index + 1;
        }

        private void UpdateStars()
        {
            Stars.Clear();
            int fullStars = SelectedRating;

            string starFull = "/Resources/Icons/star-fill.png";
            string starEmpty = "/Resources/Icons/star.png";

            var app = (App)System.Windows.Application.Current;
            if (app.CurrentTheme == "Dark")
            {
                starFull = "/Resources/Icons/dark/star-fill.png";
                starEmpty = "/Resources/Icons/dark/star.png";
            }

            for (int i = 0; i < fullStars; i++) Stars.Add(starFull);

            for (int i = Stars.Count; i < 5; i++) Stars.Add(starEmpty);
        }

    }
}
