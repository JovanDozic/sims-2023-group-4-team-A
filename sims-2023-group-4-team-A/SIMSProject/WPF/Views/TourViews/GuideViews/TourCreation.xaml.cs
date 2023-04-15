using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels;
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
        private TourCreationViewModel _viewModel { get; set; }

        public TourCreation(Guide guide)
        {
            InitializeComponent();
            _viewModel = new();
            this.DataContext = _viewModel;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsNotValid())
            {
                MessageBox.Show("Ne valja");
                return;
            }
            _viewModel.CreateTour();
            Close();
        }
        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddKeyPoint();
            LbKeyPoints.Items.Refresh();
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddAppointment();
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
            _viewModel.Images.Add(ImageURLs.Text);
            LbImages.Items.Refresh();
            _imageAdded = true;
        }
    }
}
