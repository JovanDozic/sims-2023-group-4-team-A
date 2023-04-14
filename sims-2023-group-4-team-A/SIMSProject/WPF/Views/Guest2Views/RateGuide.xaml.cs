using System.ComponentModel;
using System.Windows;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;

namespace SIMSProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for RateGuide.xaml
    /// </summary>
    public partial class RateGuide : Window
    {
        private User _user;
        private TourAppointment _tourAppointment;
        private readonly GuideRatingViewModel _viewModel;
        public GuideRating GuideRating { get; set; } = new();
        public RateGuide(User user, TourAppointment tourAppointment)
        {
            InitializeComponent();
            _user = user;
            _tourAppointment = tourAppointment;
            _viewModel = new(_user, _tourAppointment);
            DataContext = _viewModel;
        }


    }
}
