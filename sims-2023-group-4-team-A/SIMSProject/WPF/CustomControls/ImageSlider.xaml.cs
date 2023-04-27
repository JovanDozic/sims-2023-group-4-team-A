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
    /// Interaction logic for ImageSlider.xaml
    /// </summary>
    public partial class ImageSlider : UserControl
    {
        private List<string> Images = new List<string>();
        private int currentIndex;
        public ImageSlider()
        {
            InitializeComponent();
            currentIndex = 0;
            LeftButton.IsEnabled = false;
            RightButton.IsEnabled = true;

            LeftButton.Click += Button_Click_Previous;
            RightButton.Click += Button_Click_Next;

        }

        public void AddImages(List<string> imageUrls)
        {
            Images.AddRange(imageUrls);
            if (Images.Count == 1)
            {
                UpdateImage();
            }
            else
            {
                UpdateImage();
                RightButton.IsEnabled = true;
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
                ImageControl.Source = bitmapImage;
            }
        }

        private void Button_Click_Previous(object sender, RoutedEventArgs e)
        {
            currentIndex--;
            if (currentIndex == 0)
            {
                LeftButton.IsEnabled = false;
            }
            RightButton.IsEnabled = true;
            UpdateImage();
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            if (currentIndex == Images.Count - 1)
            {
                RightButton.IsEnabled = false;
            }
            LeftButton.IsEnabled = true;
            UpdateImage();
        }
    }


}
