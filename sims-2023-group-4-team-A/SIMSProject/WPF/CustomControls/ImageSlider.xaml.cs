using SIMSProject.WPF.ViewModels.AccommodationViewModels;
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
    /// Interaction logic for ImageSlider.xaml
    /// </summary>
    public partial class ImageSlider : UserControl
    {
        public static readonly DependencyProperty AccommodationViewModelProperty =
            DependencyProperty.Register(nameof(AccommodationViewModel), typeof(AccommodationViewModel), typeof(ImageSlider));

        public AccommodationViewModel AccommodationViewModel
        {
            get { return (AccommodationViewModel)GetValue(AccommodationViewModelProperty); }
            set { SetValue(AccommodationViewModelProperty, value); }
        }

        private int _currentIndex = 0;

        public ImageSlider()
        {
            InitializeComponent();
            DataContext = this;
        }

        

        private void UpdateImage()
        {
            if (AccommodationViewModel.SelectedAccommodationImages.Count > 0)
            {
                var uri = new Uri(AccommodationViewModel.SelectedAccommodationImages[_currentIndex]);
                var bitmap = new BitmapImage(uri);
                ImageControl.Source = bitmap;
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccommodationViewModel.SelectedAccommodationImages.Count > 0)
            {
                _currentIndex = (_currentIndex + AccommodationViewModel.SelectedAccommodationImages.Count - 1) % AccommodationViewModel.SelectedAccommodationImages.Count;
                UpdateImage();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
                if (AccommodationViewModel.SelectedAccommodationImages.Count > 0)
                {
                    _currentIndex = (_currentIndex + 1) % AccommodationViewModel.SelectedAccommodationImages.Count;
                    UpdateImage();
                }           
        }
    }


}
