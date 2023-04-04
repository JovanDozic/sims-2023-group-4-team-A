using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels
{

    public class VoucherViewModel : INotifyPropertyChanged
    {
        private Voucher _model;

        public VoucherViewModel()
        {
            _model = new();
        }

        public int Id
        {
            get => _model.Id;
            set => _model.Id = value;
        }

        public int GuestId
        {
            get => _model.GuestId;
            set
            {
                if (value != _model.GuestId)
                {
                    _model.GuestId = value;
                    OnPropertyChanged(nameof(GuestId));
                }

            }
        }

        //public string Reason
        //{
        //    get => _model.Reason switch
        //    {
        //        ObtainingReason.APPOINTMENTCANCELED => "Termin otkazan",
        //        ObtainingReason.GUIDEQUIT => "Vodič dao otkaz",
        //        _ => "Osvojen"
        //    };
        //    set => _model.Reason = value switch
        //    {
        //        "Termin otkazan" => ObtainingReason.APPOINTMENTCANCELED,
        //        "Vodič dao otkaz" => ObtainingReason.GUIDEQUIT,
        //        _ => ObtainingReason.WON

        //    };
        //}
        public DateTime Expiration
        {
            get => _model.Expiration;
            set
            {
                if (_model.Expiration != value)
                {
                    _model.Expiration = value;
                    OnPropertyChanged(nameof(Expiration));
                }
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
