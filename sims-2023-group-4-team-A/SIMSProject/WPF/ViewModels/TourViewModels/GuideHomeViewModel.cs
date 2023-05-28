using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class GuideHomeViewModel
    {
        private readonly GuideService _service;
        public static Guide Guide { get; private set; } = new();
        public string SuperGuideLanguages { get; private set; }
        public GuideHomeViewModel(Guide guide)
        {
            _service = Injector.GetService<GuideService>();
            Guide = guide;
            SuperGuideLanguages = _service.DisplaySuperGuideLanguages(guide);
        }
    }
}
