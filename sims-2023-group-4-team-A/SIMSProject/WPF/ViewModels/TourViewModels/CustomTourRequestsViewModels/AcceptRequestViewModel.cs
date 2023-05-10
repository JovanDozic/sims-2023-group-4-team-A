using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels
{
    public class AcceptRequestViewModel: ViewModelBase
    {
        private readonly TourAppointmentService  _tourAppointmentService;
        public List<DateTime> BusyDates { get; set; } = new();
        public CustomTourRequest? TourRequest { get; set; }

        private DateTime _date;
        public DateTime SelectedDate
        {
            get { return _date; }
            set
            {
                if(value ==  _date) return;
                _date = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }


        public AcceptRequestViewModel(CustomTourRequest customTour)
        {
           _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            BusyDates = _tourAppointmentService.GetBusyDates();
            TourRequest = customTour;
        }
    }
}
