using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ComplexRequestViewModel()
        {
            _complexService = Injector.GetService<ComplexTourRequestService>();
            ComplexTourRequests = new(_complexService.GetAll());
        }
    }
}
