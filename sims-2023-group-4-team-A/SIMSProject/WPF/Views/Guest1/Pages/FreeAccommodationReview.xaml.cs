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
    /// Interaction logic for FreeAccommodationReview.xaml
    /// </summary>
    public partial class FreeAccommodationReview : Page
    {
        private AnywhereAnytimeViewModel _anywhereAnytimeViewModel;
        private readonly User _user = new();
        public FreeAccommodationReview(AnywhereAnytimeViewModel vm, User user)
        {
            InitializeComponent();
            _user = user;
            _anywhereAnytimeViewModel = vm;
            DataContext = _anywhereAnytimeViewModel;
            AddImages();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void AddImages()
        {
            imageSlider.AddImages(_anywhereAnytimeViewModel.SelectedAccommodation.ImageURLs);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _anywhereAnytimeViewModel.SaveReservation();
            MessageBox.Show("Uspesno rezervisano!");
        }
    }
}
