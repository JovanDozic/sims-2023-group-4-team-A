using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SIMSProject.WPF.CustomControls
{

    public partial class MobileImageSlider : UserControl
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(List<string>), typeof(MobileImageSlider), new PropertyMetadata(null));

        public static new readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(MobileImageSlider), new PropertyMetadata(double.NaN));

        public static new readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(MobileImageSlider), new PropertyMetadata(double.NaN));

        public List<string> ImageSource
        {
            get { return (List<string>)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public new double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public new double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        private int currentImageIndex = 0;

        public MobileImageSlider()
        {
            InitializeComponent();
        }

        public void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            currentImageIndex++;
            if (currentImageIndex >= ImageSource.Count)
            {
                currentImageIndex = 0;
            }

            UpdateCurrentImage();
        }

        private void OnPreviousButtonClick(object sender, RoutedEventArgs e)
        {
            currentImageIndex--;
            if (currentImageIndex < 0)
            {
                currentImageIndex = ImageSource.Count - 1;
            }

            UpdateCurrentImage();
        }

        public void UpdateCurrentImage()
        {
            if (ImageSource != null && ImageSource.Count > 0)
            {
                string imagePath = ImageSource[currentImageIndex];
                BitmapImage bitmap = new BitmapImage(new System.Uri(imagePath));
                image.Source = bitmap;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == ImageSourceProperty)
            {
                currentImageIndex = 0;
                UpdateCurrentImage();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            UpdateCurrentImage();
        }
    }







}

