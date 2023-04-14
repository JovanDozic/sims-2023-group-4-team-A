using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandler
{
    public class VoucherFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourvoucher.csv";
        private readonly Serializer<Voucher> serializer;

        public VoucherFileHandler()
        {
            serializer = new Serializer<Voucher>();
        }

        public List<Voucher> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<Voucher> voucher)
        {
            serializer.ToCSV(FilePath, voucher);
        }
    }
}
