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
    /// Interaction logic for ForumCreation.xaml
    /// </summary>
    public partial class ForumCreation : Page
    {
        private User _user = new();
        private ForumViewModel _viewModel;
        public ForumCreation(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            TodaysDate.Text = DateTime.Today.ToString("dd/MM/yyyy.");
            DataContext = _viewModel;
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            if (CommentLabel.Visibility == Visibility.Collapsed && LocationLabel.Visibility == Visibility.Collapsed)
            {
                _viewModel.CreateForum();
                NavigationService.Navigate(new Forums(_user));
            }
                                
        }

        private void LocationsSelection(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
                LocationLabel.Visibility = Visibility.Collapsed;
        }

        private void CommentTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CommentTB.Text != String.Empty)
            {
                CommentLabel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
