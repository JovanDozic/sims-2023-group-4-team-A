using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class VoucherFileHandler: CSVManager<Voucher>
    {
        private const string FilePath = "../../../Resources/Data/tourvoucher.csv";

        public VoucherFileHandler(): base(FilePath)
        {
        }

        public List<Voucher> Load()
        {
            return FromCSV();
        }

        public void Save(List<Voucher> vouchers)
        {
            ToCSV(vouchers);
        }

        protected override Voucher ParseItemFromCSV(string[] values)
        {
            Voucher voucher = new()
            {
                Id = Convert.ToInt32(values[0]),
                GuestId = Convert.ToInt32(values[1]),
                Reason = (ObtainingReason)Enum.Parse(typeof(ObtainingReason), values[2]),
                Expiration = DateTime.Parse(values[3])
            };
            return voucher;
        }

        protected override string[] ParseItemToCsv(Voucher voucher)
        {
            string[] csvValues = { voucher.Id.ToString(), voucher.GuestId.ToString(), voucher.Reason.ToString(), voucher.Expiration.ToString() };
            return csvValues;
        }
    }
}
