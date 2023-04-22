using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TestSearch.xaml
    /// </summary>
    public partial class TestSearch : Page
    {
        private readonly User _user = new();
        private AccommodationViewModel _accommodationViewModel;
        private ObservableCollection<Accommodation> _accommodations;
        public TestSearch(ObservableCollection<Accommodation> results)
        {
            InitializeComponent();
            _accommodations = results;
            _accommodationViewModel = new(_user);
            DataContext = _accommodationViewModel;

        }
    }
}
