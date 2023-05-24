using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class GuideHomeViewModel
    {
        private readonly VoucherService _voucherService;
        public static Guide Guide { get; set; } = new();
        public GuideHomeViewModel(Guide guide)
        {
            _voucherService = Injector.GetService<VoucherService>();
            Guide = guide;
            QuitCommand = new RelayCommand(QuitExecute, QuitCanExecute);
        }

        #region QuitCommand
        public ICommand QuitCommand { get; private set; }
        public bool QuitCanExecute()
        {
            return true;
        }
        public void QuitExecute()
        {
            _voucherService.GiveVouchersForQuitting(Guide, ObtainingReason.GUIDEQUIT);    
        }
        #endregion
    }
}
