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
        public static Guide Guide { get; set; } = new();
        public GuideHomeViewModel(Guide guide)
        {
            Guide = guide;
        }
    }
}
