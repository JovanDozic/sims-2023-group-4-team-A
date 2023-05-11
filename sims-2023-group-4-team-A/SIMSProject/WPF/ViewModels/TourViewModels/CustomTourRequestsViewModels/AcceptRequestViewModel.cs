using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels
{
    public class AcceptRequestViewModel : ViewModelBase
    {
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly CustomTourRequestService _requestService;
        public List<DateTime> BusyDates { get; set; } = new();
        public CustomTourRequest? TourRequest { get; set; }

        private DateTime _date;
        public DateTime SelectedDate
        {
            get { return _date; }
            set
            {
                if (value == _date) return;
                _date = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }


        public AcceptRequestViewModel(CustomTourRequest customTour)
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _requestService = Injector.GetService<CustomTourRequestService>();

            BusyDates = _tourAppointmentService.GetBusyDates();
            TourRequest = customTour;
            SelectedDate = customTour.StartDate;

            AcceptCommand = new RelayCommand(AcceptCommandExecute, AcceptCommandCanExecute);
        }

        #region AcceptRequestCommand
        public ICommand AcceptCommand {  get; private set; }
        public bool AcceptCommandCanExecute()
        {
            return !BusyDates.Any(x => x.Date.Equals(SelectedDate.Date));
        }
        public void AcceptCommandExecute()
        {
            _requestService.ApproveRequest(TourRequest);
        }
        #endregion

    }
}
