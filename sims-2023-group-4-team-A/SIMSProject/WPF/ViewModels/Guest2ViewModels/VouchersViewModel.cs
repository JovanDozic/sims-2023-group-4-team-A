using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class VouchersViewModel : ViewModelBase
    {
        private readonly VoucherService _voucherService;
        private ObservableCollection<Voucher> _vouchers = new();
        public ObservableCollection<Voucher> Vouchers
        {
            get { return _vouchers; }
            set
            {
                if (value == _vouchers) return;
                _vouchers = value;
                OnPropertyChanged();
            }
        }
        
        public VouchersViewModel(Guest2 user)
        {
            _voucherService=Injector.GetService<VoucherService>();
            _voucherService.WinVoucher(user.Id);
            Vouchers = new(_voucherService.GetVouchersByGuestId(user.Id).Where(x => DateTime.Compare(x.Expiration, DateTime.Now) > 0));
        }
        public void Update(Voucher voucher)
        {
            _voucherService.Update(voucher);
        }

    }
}
