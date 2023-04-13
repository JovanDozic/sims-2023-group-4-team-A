using SIMSProject.Controller;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model.UserModel;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
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
    public partial class VouchersDisplay : Window
    {
        public Guest User = new();
        public Voucher Voucher { get; set; }
        public VoucherController VoucherController = new();

        public ObservableCollection<Voucher> Vouchers { get; set; } = new ObservableCollection<Voucher>();
        public VouchersDisplay(Guest user)
        {
            InitializeComponent();

            this.DataContext = this;
            User = user;
            Vouchers = new ObservableCollection<Voucher>(VoucherController.GetVouchersByGuestId(User.Id));
        }
    }
}
