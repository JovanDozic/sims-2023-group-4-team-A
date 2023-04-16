using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class VoucherFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourvoucher.csv";
        private readonly Serializer<Voucher> _serializer;
        public VoucherFileHandler()
        {
            _serializer = new Serializer<Voucher>();
        }

        public List<Voucher> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Voucher> vouchers)
        {
            _serializer.ToCSV(FilePath, vouchers);
        }
    }
}
