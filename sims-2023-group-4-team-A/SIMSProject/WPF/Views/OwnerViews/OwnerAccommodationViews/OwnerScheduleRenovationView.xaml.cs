using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerScheduleRenovationView : Page
    {
        private User _user;
        private OwnerScheduleRenovationViewModel _viewModel;
        
        public Accommodation Accommodation { get; set; }


        public OwnerScheduleRenovationView(User user, Accommodation accommodation, OwnerAccommodationDetails detailsView)
        {
            _user = user;
            Accommodation = accommodation;
            InitializeComponent();

            _viewModel = new(_user, Accommodation, this, detailsView);
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
