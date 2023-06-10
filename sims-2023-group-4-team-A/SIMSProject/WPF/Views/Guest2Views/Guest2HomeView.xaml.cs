using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.Guest2;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2HomeView.xaml
    /// </summary>
    public partial class Guest2HomeView : Window
    {
        public Guest2HomeView(Guest2 user)
        {
            InitializeComponent();
            this.DataContext = new HomepageViewModel(user, this, this.SelectedTab.NavigationService);
        }
    }
}
