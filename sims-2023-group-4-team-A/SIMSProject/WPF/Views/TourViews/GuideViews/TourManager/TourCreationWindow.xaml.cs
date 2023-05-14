using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels;
using System;
using System.Windows;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    ///   
    public partial class TourCreation : Window
    {
        private bool _imageAdded;
        private TourCreationViewModel ViewModel { get; set; } = new();

        public TourCreation()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsNotValid())
            {
                MessageBox.Show("Ne valja");
                return;
            }
            ViewModel.CreateTour();
            Close();
        }
        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddKeyPoint();
            LbKeyPoints.Items.Refresh();
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddAppointment();
            LbAppointments.Items.Refresh();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ImageURLs_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_imageAdded)
            {
                _imageAdded = false;
                ImageURLs.Text = string.Empty;
            }
        }
        private void BTNUploadFiles_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Images.Add(ImageURLs.Text);
            LbImages.Items.Refresh();
            _imageAdded = true;
        }
    }
}
