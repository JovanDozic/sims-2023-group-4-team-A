using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for MyTourRequests.xaml
    /// </summary>
    public partial class MyTourRequests : Page
    {
        public MyTourRequests(Guest2 user, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new CustomTourRequestViewModel(user, navigationService);
        }
        
    }
}
