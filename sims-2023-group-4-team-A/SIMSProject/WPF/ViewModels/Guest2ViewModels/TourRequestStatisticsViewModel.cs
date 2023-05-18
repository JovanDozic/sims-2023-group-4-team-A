using LiveCharts;
using LiveCharts.Wpf;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class TourRequestStatisticsViewModel : ViewModelBase
    {
        private Guest _user;
        private CustomTourRequestStatisticsService _customTourRequestStatisticsService;
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
        
        private ObservableCollection<string> _tourLanguages;
        public ObservableCollection<string> TourLanguages
        {
            get => _tourLanguages;
            set
            {
                _tourLanguages = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _tourLocations;
        public ObservableCollection<string> TourLocations
        {
            get => _tourLocations;
            set
            {
                _tourLocations = value;
                OnPropertyChanged();
            }
        }
        private SeriesCollection _locationCounts;
        public SeriesCollection LocationCounts
        {
            get => _locationCounts;
            set
            {
                _locationCounts = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection _languageCounts;
        public SeriesCollection LanguageCounts
        {
            get => _languageCounts;
            set
            {
                _languageCounts = value;
                OnPropertyChanged();
            }
        }

        
        public TourRequestStatisticsViewModel(Guest user)
        {
            _user = user;
            _customTourRequestStatisticsService = Injector.GetService<CustomTourRequestStatisticsService>();
        }

        public void LoadByYear(string selectedYear)
        {
            
            int year = selectedYear == "Oduvek" ? -1 : int.TryParse(selectedYear, out year) ? year : 0;
            if (year == -1)
            {
                AcceptedPercentage = Math.Round(_customTourRequestStatisticsService.AllTimeAcceptedRequestPercentageByGuestId(_user.Id), 2).ToString() + "%";
                UnacceptedPercentage = Math.Round(100 - _customTourRequestStatisticsService.AllTimeAcceptedRequestPercentageByGuestId(_user.Id), 2).ToString() + "%";
                AverageGuestNumber = Math.Round(_customTourRequestStatisticsService.AllTimeAverageGuestsInAcceptedRequests(_user.Id), 2).ToString();
            }
            else
            {
                AcceptedPercentage = Math.Round(_customTourRequestStatisticsService.AcceptedRequestPercentageByGuestId(_user.Id, year), 2).ToString() + "%";
                UnacceptedPercentage = Math.Round(100 - _customTourRequestStatisticsService.AcceptedRequestPercentageByGuestId(_user.Id, year), 2).ToString() + "%";
                AverageGuestNumber = Math.Round(_customTourRequestStatisticsService.AverageGuestsInAcceptedRequests(_user.Id, year), 2).ToString();
            }

            TourLanguages = new ObservableCollection<string>(_customTourRequestStatisticsService.GetTourLanguages(_user.Id, year));
            TourLocations = new ObservableCollection<string>(_customTourRequestStatisticsService.GetTourLocations(_user.Id, year));

            LanguageCounts = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Broj zahteva",
                        Values = new ChartValues<int>(_customTourRequestStatisticsService.GetRequestCountByLanguage(_user.Id, year).Values.ToList())
                    }
                };

            LocationCounts = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Broj zahteva",
                        Values = new ChartValues<int>(_customTourRequestStatisticsService.GetRequestCountByLocation(_user.Id, year).Values.ToList())
                    }
                };

        }

    }
}
