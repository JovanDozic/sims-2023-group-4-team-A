using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestDetails.xaml
    /// </summary>
    public partial class ComplexTourRequestDetails : Page
    {
        public ComplexTourRequestDetails(Guest2 user, ComplexTourRequest complexTourRequest, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new ComplexTourRequestViewModel(user, navigationService, complexTourRequest);
        }

    }
}
