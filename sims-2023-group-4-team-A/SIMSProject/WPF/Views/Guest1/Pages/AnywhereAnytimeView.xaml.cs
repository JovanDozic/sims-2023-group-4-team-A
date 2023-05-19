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
    /// Interaction logic for AnywhereAnytimeView.xaml
    /// </summary>
    public partial class AnywhereAnytimeView : Page
    {
        private readonly User _user = new();
        private AnywhereAnytimeViewModel _anywhereAnytimeViewModel;

        public AnywhereAnytimeView(User user)
        {
            InitializeComponent();
            _user = user;
            _anywhereAnytimeViewModel = new(_user);
            DataContext = _anywhereAnytimeViewModel;
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            _anywhereAnytimeViewModel.Search();
        }
    }
}
