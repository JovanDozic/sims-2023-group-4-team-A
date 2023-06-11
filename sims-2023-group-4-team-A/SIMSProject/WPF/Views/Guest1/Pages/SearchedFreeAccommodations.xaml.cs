using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest1ViewModels;
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
    /// Interaction logic for SearchedFreeAccommodations.xaml
    /// </summary>
    public partial class SearchedFreeAccommodations : Page
    {
        private AnywhereAnytimeViewModel _accommodationViewModel;
        private readonly User _user = new();
        public SearchedFreeAccommodations(AnywhereAnytimeViewModel vm, User user)
        {
            InitializeComponent();
            _user = user;
            _accommodationViewModel = vm;
            DataContext = _accommodationViewModel;
            LabelVisibility();

        }
        public void LabelVisibility()
        {
            if (_accommodationViewModel.IsAccommodationFound())
            {
                FoundLabel.Visibility = Visibility.Visible;
            }
            else
                NotFoundLabel.Visibility = Visibility.Visible;
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AnywhereAnytimeView(_user));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchedAccLW.SelectedItem != null)
            {
                NavigationService.Navigate(new FreeAccommodationReview(_accommodationViewModel, _user));
            }
        }
    }
}
