using SIMSProject.Model.DAO;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Controller
{
    public class VoucherController
    {
        private readonly VoucherDAO _vouchers;
        public Voucher Voucher;

        public VoucherController()
        {
            _vouchers = new VoucherDAO();
            Voucher = new Voucher();
        }

        public List<Voucher> GetAll()
        {
            return _vouchers.GetAll();
        }

        public void SaveAll(List<Voucher> Vouchers)
        {
            _vouchers.SaveAll(Vouchers);
        }

        public Voucher Create(Voucher Voucher)
        {
            return _vouchers.Save(Voucher);
        }

        public Voucher GetByID(int id)
        {
            return _vouchers.Get(id);
        }
    }
}
