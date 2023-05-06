using SIMSProject.Domain.Models.UserModels;
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
        public Guest User = new();
        public MyTourRequests(Guest user)
        {
            InitializeComponent();
            User = user;
        }

        private void NewRequest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomTourRequestCreation(User));
        }
    }
}
