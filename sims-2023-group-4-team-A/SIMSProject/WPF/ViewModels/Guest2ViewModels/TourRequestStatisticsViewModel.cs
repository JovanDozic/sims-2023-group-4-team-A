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
            _customTourRequestService = Injector.GetService<CustomTourRequestService>();
        }

        public void LoadByYear(string selectedYear)
        {
            if (selectedYear == "Oduvek")
            {
                AcceptedPercentage = Math.Round(_customTourRequestService.AllTimeAcceptedRequestPercentageByGuestId(_user.Id), 2).ToString() + "%";
                UnacceptedPercentage = (100 - Math.Round(_customTourRequestService.AllTimeAcceptedRequestPercentageByGuestId(_user.Id), 2)).ToString() + "%";
                AverageGuestNumber = Math.Round(_customTourRequestService.AllTimeAverageGuestsInAcceptedRequests(_user.Id), 2).ToString();
                
                TourLanguages = new ObservableCollection<string>(_customTourRequestService.GetTourLanguages(_user.Id));
                TourLocations = new ObservableCollection<string>(_customTourRequestService.GetTourLocations(_user.Id));

                LanguageCounts = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Broj zahteva",
                        Values = new ChartValues<int>(_customTourRequestService.GetAllTimeRequestCountByLanguage(_user.Id).Values.ToList())
                    }
                };

                LocationCounts = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Broj zahteva",
                        Values = new ChartValues<int>(_customTourRequestService.GetAllTimeRequestCountByLocation(_user.Id).Values.ToList())
                    }
                };

            }
            int year;
            if (int.TryParse(selectedYear, out year))
            {
                AcceptedPercentage = Math.Round(_customTourRequestService.AcceptedRequestPercentageByGuestId(_user.Id, year), 2).ToString() + "%";
                UnacceptedPercentage = (100 - Math.Round(_customTourRequestService.AcceptedRequestPercentageByGuestId(_user.Id, year), 2)).ToString() + "%";
                AverageGuestNumber = Math.Round(_customTourRequestService.AverageGuestsInAcceptedRequests(_user.Id, year), 2).ToString();

                TourLanguages = new ObservableCollection<string>(_customTourRequestService.GetTourLanguagesByYear(_user.Id, year));
                TourLocations = new ObservableCollection<string>(_customTourRequestService.GetTourLocationsByYear(_user.Id, year));

                LanguageCounts = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Broj zahteva",
                        Values = new ChartValues<int>(_customTourRequestService.GetRequestCountByLanguage(_user.Id, year).Values.ToList())
                    }
                };

                LocationCounts = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Broj zahteva",
                        Values = new ChartValues<int>(_customTourRequestService.GetRequestCountByLocation(_user.Id, year).Values.ToList())
                    }
                };
            }
        }

    }
}
