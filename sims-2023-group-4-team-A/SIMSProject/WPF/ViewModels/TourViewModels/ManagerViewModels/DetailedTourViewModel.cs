using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.Messenger.Messages;
using SIMSProject.WPF.ViewModels.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels
{
    public class DetailedTourViewModel : ViewModelBase
    {
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly VoucherSevice _voucherService;
        private readonly TourGuestService _tourGuestService;

        private ObservableCollection<TourAppointment> tourAppointments = new();
        public ObservableCollection<TourAppointment> Appointments
        {
            get => tourAppointments;
            set
            {
                if (value == tourAppointments) return;
                tourAppointments = value;
                OnPropertyChanged(nameof(Appointments));
            }
        }

        private TourAppointment _selectedAppointment = new();
        public TourAppointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (value != _selectedAppointment)
                {
                    _selectedAppointment = value;
                    OnPropertyChanged(nameof(SelectedAppointment));
                }
            }
        }

        private Tour _tour = new();
        public Tour SelectedTour
        {
            get => _tour;
            set
            {
                if (value != _tour)
                {
                    _tour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        public DetailedTourViewModel() 
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _voucherService = Injector.GetService<VoucherSevice>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            
            //SelectedTour = tour;
            //GetAllAppointments();

            MessageBus.Subscribe<TourInfoMessage>(this, OpenMessage);
            
            CancelAppointmentCommand = new RelayCommand(CancelAppointmentExecute, CancelAppointmentCanExecute);
        }

        private void OpenMessage(TourInfoMessage message)
        {
            SelectedTour = message.Tour;
            Appointments = new(_tourAppointmentService.GetAllByTourId(SelectedTour.Id));
        }

        #region CancelCommand
        public ICommand CancelAppointmentCommand { get; private set; }
        public bool CancelAppointmentCanExecute()
        {
            return SelectedAppointment.TourStatus == Status.INACTIVE && Appointments.Count > 0;
        }
        public void CancelAppointmentExecute()
        {
            if (!_tourAppointmentService.CancelAppointment(SelectedAppointment))
            {
                return;
            }
            List<TourGuest> guests = _tourGuestService.GetGuests(SelectedAppointment);
            _voucherService.GiveVouchers(guests, ObtainingReason.APPOINTMENTCANCELED);
        }
        #endregion
    }
}
