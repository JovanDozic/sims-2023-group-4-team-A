using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
using System;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    internal class OwnerAllReschedulingRequestsViewModel : ViewModelBase
    {
        private User _user;
        private ReschedulingRequestService _requestService;

        public Accommodation Accommodation { get; set; }

        public ObservableCollection<ReschedulingRequest> Requests { get; set; }
        public ReschedulingRequest Request { get; set; } = new();

        public OwnerAllReschedulingRequestsViewModel(User user, Accommodation accommodation)
        {
            _user = user;
            Accommodation = accommodation;

            _requestService = Injector.GetService<ReschedulingRequestService>();

            Requests = new(_requestService.GetAllOnWaitByAccommodationId(Accommodation.Id));
        }

        internal void ReloadRequests()
        {
            Requests = new(_requestService.GetAllOnWaitByAccommodationId(Accommodation.Id));
            OnPropertyChanged(nameof(Requests));
        }
    }
}
