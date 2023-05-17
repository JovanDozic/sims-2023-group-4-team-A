using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerScheduleRenovationOld : Window
    {
        private User _user;
        private AccommodationRenovationViewModel _viewModel;

        public OwnerScheduleRenovationOld(User user, Accommodation accommodation)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            _viewModel.Accommodation = accommodation;
            DataContext = _viewModel;
        }

        private void BtnFindDates_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.FindDates();
        }

        private void BtnScheduleRenovation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ScheduleRenovation();
            Close();
        }
    }
}
