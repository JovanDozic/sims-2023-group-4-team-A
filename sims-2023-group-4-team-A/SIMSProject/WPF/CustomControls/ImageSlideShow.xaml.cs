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
using System.Windows.Threading;
using System.Windows.Xps.Serialization;

namespace SIMSProject.WPF.CustomControls
{
    /// <summary>
    /// Interaction logic for ImageSlideShow.xaml
    /// </summary>
    public partial class ImageSlideShow : UserControl
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("Images", typeof(List<string>), typeof(ImageSlideShow), new PropertyMetadata(null));

        public List<string> Images
        {
            get { return (List<string>)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        private  int currentIndex;
        private readonly DispatcherTimer timer;
        public ImageSlideShow()
        {
            InitializeComponent();
            currentIndex = 0;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(Images.Count > 0)
            {
                currentIndex = currentIndex == (Images.Count - 1) ? 0 : ++currentIndex;
                UpdateImage();
            }
        }

        public void AddImages(List<string> imageUrls)
        {
            Images.AddRange(imageUrls);
            if (Images.Count > 0)
            {
                UpdateImage();
            }
        }
        public void AddImage(string imageUrl)
        {
            Images.Add(imageUrl);
            if (Images.Count == 1)
            {
                UpdateImage();
            }
        }
        private void UpdateImage()
        {
            if (Images.Count > 0)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(Images[currentIndex], UriKind.Absolute);
                bitmapImage.EndInit();
                IImage.Source = bitmapImage;
            }
        }
    }
}
