using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows.Controls;

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
