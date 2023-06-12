using Microsoft.VisualStudio.Services.Common;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System;
using System.ComponentModel;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    internal class OwnerReschedulingRequestViewModel : ViewModelBase, IDataErrorInfo
    {
        private User _user;
        private App _app = (App)System.Windows.Application.Current;
        private ReschedulingRequestService _requestService;
        private OwnerReschedulingRequestView _view;

        public ReschedulingRequest Request { get; set; }
        public string OwnerComment
        {
            get => Request.OwnerComment;
            set
            {
                if (Request.OwnerComment == value) return;
                Request.OwnerComment = value;
                OnPropertyChanged(nameof(OwnerComment));
            }
        }

        public bool IsAvailable { get; set; } = false;
        public bool IsInDeclineMode { get; set; } = false;
        public GridLength IsInDeclineModeRowHeight { get => IsInDeclineMode ? new GridLength(100) : new GridLength(0); }

        public RelayCommand AcceptRequestCommand { get; set; }
        public RelayCommand DeclineRequestCommand { get; set; }
        public RelayCommand SendDeclineCommand { get; set; }


        public OwnerReschedulingRequestViewModel(User user, ReschedulingRequest request, OwnerReschedulingRequestView view)
        {
            _user = user;
            Request = request;
            _view = view;
            _requestService = Injector.GetService<ReschedulingRequestService>();

            AcceptRequestCommand = new RelayCommand(AcceptRequest, CanAcceptRequest);
            DeclineRequestCommand = new RelayCommand(DeclineRequest, CanDeclineRequest);
            SendDeclineCommand = new RelayCommand(SendDecline, CanSendDecline);

            IsAvailable = _requestService.IsDateRangeAvailable(Request.Reservation, Request.NewStartDate, Request.NewEndDate);
        }

        private bool CanSendDecline()
        {
            return true;
        }


        private bool CanDeclineRequest()
        {
            return true;
        }

        private bool CanAcceptRequest()
        {
            return true;
        }

        private void SendDecline()
        {
            if (_app.CurrentLanguage == "en-US")
            {
                if (MessageBox.Show("Are you sure you want to decline this request?", "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            }
            else if (MessageBox.Show("Da li ste sigurni da želite da odbijete ovaj zahtev?", "Potvrda",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) != MessageBoxResult.Yes) return;

            _requestService.RejectRequest(Request);
            _view.GoBackAndReload();
        }

        private void DeclineRequest()
        {
            IsInDeclineMode = true;
            OnPropertyChanged(nameof(IsInDeclineMode));
            OnPropertyChanged(nameof(IsInDeclineModeRowHeight));
        }


        private void AcceptRequest()
        {
            IsInDeclineMode = false;
            OnPropertyChanged(nameof(IsInDeclineMode));
            OnPropertyChanged(nameof(IsInDeclineModeRowHeight));

            if (_app.CurrentLanguage == "en-US")
            {
                if (MessageBox.Show("Are you sure you want to accept this request?", "Are you sure?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            }
            else if (MessageBox.Show("Da li ste sigurni da želite da prihvatite ovaj zahtev?", "Da li ste sigurni?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) != MessageBoxResult.Yes) return;

            _requestService.AcceptRequest(Request);
            _view.GoBackAndReload();

            if (_app.CurrentLanguage == "en-US")
            {
                if (MessageBox.Show("Request declined.", "Confirmation",
                MessageBoxButton.OK,
                MessageBoxImage.Information) != MessageBoxResult.Yes) return;
            }
            else if (MessageBox.Show("Zahtev odbijen.", "Potvrda",
                MessageBoxButton.OK,
                MessageBoxImage.Information) != MessageBoxResult.Yes) return;
        }

        public string this[string columnName]
        {
            get
            {
                string? error = null;
                string requiredMessage = "Obavezno polje";
                switch (columnName)
                {
                    case nameof(OwnerComment):
                        if (string.IsNullOrEmpty(OwnerComment)) error = requiredMessage;
                        else if (OwnerComment.Length < 3) error = "Komentar mora biti duzi od 10 karaktera";
                        else if (OwnerComment.Contains("|")) error = "Komentar ne sme da sadrzi '|'";
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error => null;
        public bool IsNewOwnerCommentValid
        {
            get
            {
                foreach (var property in new string[] {
                    nameof(OwnerComment)
                })
                {
                    if (this[property] != null) return false;
                }
                return true;
            }
        }

    }
}
