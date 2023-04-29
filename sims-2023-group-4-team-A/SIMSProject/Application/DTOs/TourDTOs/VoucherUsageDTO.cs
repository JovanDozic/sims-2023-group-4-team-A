using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.DTOs.TourDTOs
{
    public class VoucherUsageDTO
    {
        public double Used { get; set; }
        public double Unused { get; set; }

        public VoucherUsageDTO()
        {
        }

        public VoucherUsageDTO(int used, int unused)
        {
            int total = CalculateTotal(used, unused);
            Used = Math.Round((double)used / total, 2);
            Unused = Math.Round((double)unused / total, 2);
        }

        private static int CalculateTotal(int used, int unused)
        {
            return used + unused;
        }
    }
}
