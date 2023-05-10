using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.UserModels;
using System;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class TourRequestStatisticsViewModel : ViewModelBase
    {
        private Guest _user;
        private CustomTourRequestService _customTourRequestService;
        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear == value) return;
                _selectedYear = value;
                OnPropertyChanged();
                LoadByYear(_selectedYear);
            }
        }
        private string _acceptedPercentage;
        public string AcceptedPercentage
        {
            get => _acceptedPercentage;
            set
            {
                if (_acceptedPercentage == value) return;
                _acceptedPercentage = value;
                OnPropertyChanged();
            }
        }
        private string _unacceptedPercentage;
        public string UnacceptedPercentage
        {
            get => _unacceptedPercentage;
            set
            {
                if (_unacceptedPercentage == value) return;
                _unacceptedPercentage = value;
                OnPropertyChanged();
            }
        }
        private double _averageGuestPercentage;
        public double AverageGuestPercentage
        {
            get => _averageGuestPercentage;
            set
            {
                if(value == _averageGuestPercentage) return;
                _averageGuestPercentage = value;
                OnPropertyChanged();
            }
        }
        public TourRequestStatisticsViewModel(Guest user)
        {
            _user = user;
            _customTourRequestService = Injector.GetService<CustomTourRequestService>();
        }

        public void LoadByYear(int year)
        {
            AcceptedPercentage = Math.Round(_customTourRequestService.AcceptedRequestPercentageByGuestId(_user.Id, year),2).ToString() + "%";
            UnacceptedPercentage = (100- Math.Round(_customTourRequestService.AcceptedRequestPercentageByGuestId(_user.Id, year), 2)).ToString() + "%";
            AverageGuestPercentage = Math.Round(_customTourRequestService.AverageGuestsInAcceptedRequests(_user.Id, year), 2);
        }

    }
}
