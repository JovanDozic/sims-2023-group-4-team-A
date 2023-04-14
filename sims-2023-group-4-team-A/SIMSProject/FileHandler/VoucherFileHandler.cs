using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Serializer;
using System.Collections.Generic;

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
