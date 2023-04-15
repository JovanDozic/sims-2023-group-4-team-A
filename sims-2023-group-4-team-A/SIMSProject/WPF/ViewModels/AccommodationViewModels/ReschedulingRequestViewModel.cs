using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class ReschedulingRequestViewModel : ViewModelBase
    {
        private readonly User _user;
        private ReschedulingRequest _request = new();
        private ReschedulingRequestService _service;
        private ReschedulingRequest _selectedRequest = new();

        public ObservableCollection<ReschedulingRequest> Requests { get; set; } = new();
        public ReschedulingRequest SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                if (value == _selectedRequest) return;
                _selectedRequest = value;
                OnPropertyChanged();
            }
        }
        public AccommodationReservation Reservation
        {
            get => _request.Reservation;
            set 
            {
                if (value == _request.Reservation) return;
                _request.Reservation = value;
                OnPropertyChanged();
            }
        }
        public DateTime NewStartDate
        {
            get => _request.NewStartDate;
            set
            {
                if (_request.NewStartDate == value) return;
                _request.NewStartDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime NewEndDate
        {
            get => _request.NewEndDate;
            set
            {
                if (_request.NewEndDate == value) return;
                _request.NewEndDate = value;
                OnPropertyChanged();
            }

        }
        public string OwnerComment
        {
            get => _request.OwnerComment;
            set
            {
                if (_request.OwnerComment == value) return;
                _request.OwnerComment = value;
                OnPropertyChanged();
            }
        }
        public ReschedulingRequestStatus Status
        {
            get => _request.Status;
            set
            {
                if (_request.Status == value) return;
                _request.Status = value;
                OnPropertyChanged();
            }
        }

        public ReschedulingRequestViewModel(User user)
        {
            _user = user;
            _service = Injector.GetService<ReschedulingRequestService>();

            Requests = new ObservableCollection<ReschedulingRequest>(_service.GetAllByOwnerId(_user.Id));
        }

        public bool IsDateRangeAvailable()
        {
            return _service.IsDateRangeAvailable(SelectedRequest.Reservation, SelectedRequest.NewStartDate, SelectedRequest.NewEndDate);
        }

        public void AcceptRequest()
        {
            _service.AcceptRequest(SelectedRequest);
            OnPropertyChanged(nameof(SelectedRequest));
        }

        public void RejectRequest()
        {
            _service.RejectRequest(SelectedRequest);
            OnPropertyChanged(nameof(SelectedRequest));
        }

        public bool IsRequestOnWaiting(DataGridRowEventArgs e)
        {
            return Requests[e.Row.GetIndex()].Status == ReschedulingRequestStatus.Waiting;
        }

        public void SendRequest()
        {
            _service.SaveRequest(_request);
        }
    }
}
