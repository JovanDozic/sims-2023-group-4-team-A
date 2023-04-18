using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
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
using System.Windows.Shapes;

namespace SIMSProject.View.Guest2
{
    /// <summary>
    /// Interaction logic for VouchersDisplay.xaml
    /// </summary>
    public partial class VouchersDisplay : Page
    {
        public Guest User = new();
        private VouchersViewModel _vouchersViewModel { get; set; }
        public VouchersDisplay(Guest user)
        {
            InitializeComponent();
            User = user;
            _vouchersViewModel = new(User);
            this.DataContext = _vouchersViewModel;
        }
    }
}
