using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows.Controls;

namespace SIMSProject.View.Guest2
{
    /// <summary>
    /// Interaction logic for VouchersDisplay.xaml
    /// </summary>
    public partial class VouchersDisplay : Page
    {
        public VouchersDisplay(Domain.Models.UserModels.Guest2 user)
        {
            InitializeComponent();
            this.DataContext = new VouchersViewModel(user);
        }
    }
}
