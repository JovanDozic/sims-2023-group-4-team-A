using SIMSProject.Domain.Models.UserModels;
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

namespace SIMSProject.WPF.Views.Guest1.Pages
{
    /// <summary>
    /// Interaction logic for Forums.xaml
    /// </summary>
    public partial class Forums : Page
    {
        private User _user = new();
        private ForumViewModel _viewModel;
        public Forums(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ForumCreation(_user));
        }

        private void MyForums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyForums.SelectedItem != null)
            {
                NavigationService.Navigate(new ForumView());
            }
        }
    }
}
