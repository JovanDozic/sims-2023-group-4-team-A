using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerAllRenovationsView : Page
    {
        private User _user;
        private OwnerAllRenovationsViewModel _viewModel;



        public OwnerAllRenovationsView(User user, Accommodation accommodation, OwnerAccommodationDetails detailsView)
        {
            InitializeComponent();
            _user = user;


            _viewModel = new OwnerAllRenovationsViewModel(_user, accommodation, detailsView);
        
            DataContext = _viewModel;

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void BtnCancelRenovation_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da otkažete renoviranje?",
                                "Otkazivanje renoviranja",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            _viewModel.CancelRenovation();
            LstRenovations.Items.Refresh();
        }

        private void LstRenovationsItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem ?? throw new System.Exception("List item not found.");
            AccommodationRenovation? renovation = listViewItem.DataContext as AccommodationRenovation;
            _viewModel.HoveredRenovation = renovation;
        }

    }
}
