using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Windows.Controls;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class TourRequestStatisticsViewModel : ViewModelBase
    {
        private Guest _user;
        private CustomTourRequestService _customTourRequestService;
        private string _selectedYear;
        public string SelectedYear
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
        private ComboBoxItem _selectedItem;
        public ComboBoxItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value) return;
                _selectedItem = value;
                OnPropertyChanged();
                SelectedYear = _selectedItem?.Content.ToString();
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
        private string _averageGuestNumber;
        public string AverageGuestNumber
        {
            get => _averageGuestNumber;
            set
            {
                if(value == _averageGuestNumber) return;
                _averageGuestNumber = value;
                OnPropertyChanged();
            }
        }
        public TourRequestStatisticsViewModel(Guest user)
        {
            _user = user;
            _customTourRequestService = Injector.GetService<CustomTourRequestService>();
        }

        public void LoadByYear(string selectedYear)
        {
            if (selectedYear == "Oduvek")
            {
                AcceptedPercentage = Math.Round(_customTourRequestService.AllTimeAcceptedRequestPercentageByGuestId(_user.Id), 2).ToString() + "%";
                UnacceptedPercentage = (100 - Math.Round(_customTourRequestService.AllTimeAcceptedRequestPercentageByGuestId(_user.Id), 2)).ToString() + "%";
                AverageGuestNumber = Math.Round(_customTourRequestService.AllTimeAverageGuestsInAcceptedRequests(_user.Id), 2).ToString();
            }
            int year;
            if (int.TryParse(selectedYear, out year))
            {
                AcceptedPercentage = Math.Round(_customTourRequestService.AcceptedRequestPercentageByGuestId(_user.Id, year), 2).ToString() + "%";
                UnacceptedPercentage = (100 - Math.Round(_customTourRequestService.AcceptedRequestPercentageByGuestId(_user.Id, year), 2)).ToString() + "%";
                AverageGuestNumber = Math.Round(_customTourRequestService.AverageGuestsInAcceptedRequests(_user.Id, year), 2).ToString();
            }
        }

    }
}
