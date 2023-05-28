using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.Injectors;
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
        private readonly GuideService _service;
        public string SuperGuideLanguages { get; private set; }
        public GuideHomeViewModel(Guide guide)
        {
            _voucherService = Injector.GetService<VoucherService>();
            _service = Injector.GetService<GuideService>();
            Guide = guide;
            SuperGuideLanguages = _service.DisplaySuperGuideLanguages(guide);
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
