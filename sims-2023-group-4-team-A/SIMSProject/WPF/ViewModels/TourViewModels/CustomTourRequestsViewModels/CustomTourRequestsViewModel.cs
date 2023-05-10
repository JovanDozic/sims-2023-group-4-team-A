using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels
{
    public class CustomTourRequestsViewModel: ViewModelBase
    {
        private readonly CustomTourRequestService _requestService;

        private ObservableCollection<CustomTourRequest> _customTourRequests = new();
        public ObservableCollection<CustomTourRequest> CustomTourRequests
        {
            get => _customTourRequests;
            set
            {
                if(value == _customTourRequests) return;
                _customTourRequests = value;
                OnPropertyChanged(nameof(CustomTourRequests));
            }
        }
        private CustomTourRequest _selectedRequest = new();
        public CustomTourRequest SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                if(value == _selectedRequest) return;
                _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }

        public ObservableCollection<Location> RequestsLocations { get; set; } = new();
        public ObservableCollection<Language> RequestsLanguage { get; set; } = new();

        private Location _location = new();
        public Location SelectedLocation
        {
            get => _location;
            set
            {
                if(value == _location) return;
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private Language _language;
        public Language SelectedLanguage
        {
            get => _language;
            set
            {
                if(value == _language) return;
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        private int _guests;
        public int NumberOfGuests
        {
            get => _guests;
            set
            {
                if(_guests == value) return;
                _guests = value;
                OnPropertyChanged(nameof(NumberOfGuests));
            }
        }

        public CustomTourRequestsViewModel()
        {
            _requestService = Injector.GetService<CustomTourRequestService>();
            CustomTourRequests = new (_requestService.GetAll());
            _requestService.GetRequestsLanguages();
        }
    }
}
