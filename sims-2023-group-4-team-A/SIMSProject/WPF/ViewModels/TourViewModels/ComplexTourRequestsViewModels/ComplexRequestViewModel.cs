using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.Messenger;
using SIMSProject.WPF.Messenger.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ComplexTourRequestsViewModels
{
    public class ComplexRequestViewModel: ViewModelBase
    {
        private readonly ComplexTourRequestService _complexService;

        private ObservableCollection<ComplexTourRequest> _complexTourRequests = new();
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests
        {
            get => _complexTourRequests;
            set
            {
                if (value == _complexTourRequests) return;
                _complexTourRequests = value;
                OnPropertyChanged(nameof(ComplexTourRequests));
            }
        }
        private ComplexTourRequest _selectedRequest = new();
        public ComplexTourRequest SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                if (value == _selectedRequest) return;
                _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }
        private CustomTourRequest _selectedPart = new();
        public CustomTourRequest SelectedPart
        {
            get => _selectedPart;
            set
            {
                if (value == _selectedPart) return;
                _selectedPart = value;
                OnPropertyChanged(nameof(SelectedPart));
            }
        }

        public ComplexRequestViewModel()
        {
            _complexService = Injector.GetService<ComplexTourRequestService>();
            ComplexTourRequests = new(_complexService.GetAll());
            GetPotentialAppointmentsCommand = new RelayCommand(GetPotentialAppointmentsExecute, GetPotentialAppointmentsCanExecute);
        }

        #region GetPotentialAppointmentsCommand
        public ICommand GetPotentialAppointmentsCommand { get; private set; }
        public bool GetPotentialAppointmentsCanExecute()
        {
            return SelectedPart != null && SelectedPart.Id > 0;
        }
        public void GetPotentialAppointmentsExecute()
        {
            SendMessage();
        }

        private void SendMessage()
        {
            var message = new ScheduleRequestMessage(this, SelectedPart);
            MessageBus.Publish(message);
        }
        #endregion
    }
}
