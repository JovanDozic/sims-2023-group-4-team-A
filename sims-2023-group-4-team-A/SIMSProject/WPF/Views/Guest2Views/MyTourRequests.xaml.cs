using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
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

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for MyTourRequests.xaml
    /// </summary>
    public partial class MyTourRequests : Page
    {
        public Guest _user = new();
        private CustomTourRequestViewModel _viewModel;
        public MyTourRequests(Guest user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new CustomTourRequestViewModel(_user);
            this.DataContext = _viewModel;
        }

        private void NewRequest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomTourRequestCreation(_user));
        }
    }
}
