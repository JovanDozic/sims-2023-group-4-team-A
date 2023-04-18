using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class ReschedulingRequestViewModel : ViewModelBase
    {
        private readonly User _user;
        private ReschedulingRequest _request = new();
        private ReschedulingRequestService _service;
        private ReschedulingRequest _selectedRequest = new();
        private AccommodationReservationViewModel _accommodationReservationViewModel;
        public ObservableCollection<ReschedulingRequest> Requests { get; set; } = new();
        public object RequestsCombo { get; private set; } = new();
        public AccommodationReservation Reservation
        {
            get => _request.Reservation;
            set
            {
                if (_request.Reservation == value) return;
                _request.Reservation = value;
                OnPropertyChanged();
            }
        }
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

        public ReschedulingRequestViewModel(User user, AccommodationReservation reservation)
        {
            _user = user;
            _service = Injector.GetService<ReschedulingRequestService>();
            _accommodationReservationViewModel = new(_user);
            Requests = new();
            Reservation = reservation;
            NewStartDate = DateTime.Today.AddDays(1);
        }

        public void LoadRequestsByOwner()
        {
            Requests = new(_service.GetAllByOwnerId(_user.Id ));
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

        public AccommodationReservation LoadReservation()
        {
            return _accommodationReservationViewModel.SelectedReservation;
        }

        public int AddDays()
        {
            return Reservation.NumberOfDays;
        }

        public int SubDays()
        {
            return -Reservation.NumberOfDays;
        }

        public string DisplayName()
        {
            return Reservation.Accommodation.Name;
        }

        public string DisplayDate()
        {
            return Reservation.StartDate.ToString("dd/MM/yyyy.") + " - "
                + Reservation.EndDate.ToString("dd/MM/yyyy.");
        }

        public void AddRequestsToCombo()
        {
            foreach(var req in _service.GetAllByGuestId(_user.Id))
            {
                if (req.Reservation == null)
                {
                    continue;
                }

                req.RequestDetails = String.Format("{0}, ({1} - {2})", req.Reservation.Accommodation.Name,
                    req.Reservation.StartDate.ToShortDateString(), req.Reservation.EndDate.ToShortDateString());
                Requests.Add(req);
            }
            MessageBox.Show(Requests.Count.ToString());
            RequestsCombo = Requests;
        }

        public bool IsSelected()
        {
            return SelectedRequest != null;
        }
        
    }
}
