using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Helpers;
using Dynamitey.DynamicObjects;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using SIMSProject.WPF.Messenger.Messages;
using SIMSProject.WPF.ViewModels.Messenger;

namespace SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels
{
    public class CustomTourRequestsStatisticsViewModel : ViewModelBase
    {
        private readonly CustomTourRequestService _requestService;

        public ObservableCollection<Location> RequestsLocations { get; set; }
        public ObservableCollection<Language> RequestsLanguages { get; set; }

        private readonly List<string> Months = new()
        {
            "Januar", "Februar", "Mart", "April", "Maj", "Jun",
            "Jul", "Avgust", "Septembar", "Oktobar", "Novembar", "Decembar"
        };

        private string _titleX = string.Empty;
        public string TitleX
        {
            get => _titleX;
            set
            {
                if (_titleX != value)
                {
                    _titleX = value;
                    OnPropertyChanged(nameof(TitleX));
                }
            }
        }
        private List<string> _labels = new();
        public List<string> Labels
        {
            get => _labels;
            set
            {
                if (value == _labels) return;
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        private SeriesCollection _series = new();
        public SeriesCollection Series
        {
            get => _series;
            set
            {
                if (value == _series) return;
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }
        private Location _location = new();
        public Location SelectedLocation
        {
            get => _location;
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(SelectedLocation));
                }
            }
        }

        private Language _language;
        public Language SelectedLanguage
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        private bool _rbLocationsIsChecked;
        public bool RbLocationsIsChecked
        {
            get => _rbLocationsIsChecked;
            set
            {
                if (value != _rbLocationsIsChecked)
                {
                    _rbLocationsIsChecked = value;
                    OnPropertyChanged(nameof(RbLocationsIsChecked));
                    CbLocationsIsEnabled = _rbLocationsIsChecked;
                }
            }
        }

        private bool _rbLanguagesIsChecked;
        public bool RbLanguagesIsChecked
        {
            get => _rbLanguagesIsChecked;
            set
            {
                if (_rbLanguagesIsChecked != value)
                {
                    _rbLanguagesIsChecked = value;
                    OnPropertyChanged(nameof(RbLanguagesIsChecked));
                    CbLanguagesIsEnabled = _rbLanguagesIsChecked;
                }
            }
        }

        private bool _rbAnnualyIsChecked;
        public bool RbAnnualyIsChecked
        {
            get => _rbAnnualyIsChecked;
            set
            {
                if (value != _rbAnnualyIsChecked)
                {
                    _rbAnnualyIsChecked = value;
                    OnPropertyChanged(nameof(RbAnnualyIsChecked));
                }
            }
        }

        private bool _rbYearIsChecked;
        public bool RbYearIsChecked
        {
            get => _rbYearIsChecked;
            set
            {
                if (value != _rbYearIsChecked)
                {
                    _rbYearIsChecked = value;
                    OnPropertyChanged(nameof(RbYearIsChecked));
                    TbYearIsEnabled = _rbYearIsChecked;
                }
            }
        }

        private bool _cbLocationsIsEnabled;
        public bool CbLocationsIsEnabled
        {
            get => _cbLocationsIsEnabled;
            set
            {
                if (_cbLocationsIsEnabled != value)
                {
                    _cbLocationsIsEnabled = value;
                    OnPropertyChanged(nameof(CbLocationsIsEnabled));
                }
            }
        }

        private bool _cbLanguagesIsEnabled;
        public bool CbLanguagesIsEnabled
        {
            get => _cbLanguagesIsEnabled;
            set
            {
                if (_cbLanguagesIsEnabled != value)
                {
                    _cbLanguagesIsEnabled = value;
                    OnPropertyChanged(nameof(CbLanguagesIsEnabled));
                }
            }
        }

        private bool _tbYearIsEnabled;
        public bool TbYearIsEnabled
        {
            get => _tbYearIsEnabled;
            set
            {
                if (_tbYearIsEnabled != value)
                {
                    _tbYearIsEnabled = value;
                    OnPropertyChanged(nameof(TbYearIsEnabled));
                }
            }
        }

        private string _desiredYear = String.Empty;
        public string DesiredYear
        {
            get => _desiredYear;
            set
            {
                if (value == _desiredYear) return;
                _desiredYear = value;
                OnPropertyChanged(nameof(DesiredYear));
            }
        }

        private Visibility _spVisibilityLanguages = Visibility.Collapsed;
        public Visibility SpVisibilityLanguages
        {
            get => _spVisibilityLanguages;
            set
            {
                if (value == _spVisibilityLanguages) return;
                _spVisibilityLanguages = value;
                OnPropertyChanged(nameof(SpVisibilityLanguages));
            }
        }
        private Visibility _spVisibilityLocations = Visibility.Collapsed;
        public Visibility SpVisibilityLocations
        {
            get => _spVisibilityLocations;
            set
            {
                if (value == _spVisibilityLocations) return;
                _spVisibilityLocations = value;
                OnPropertyChanged(nameof(SpVisibilityLocations));
            }
        }
        private Language _mostWantedLanguage;
        public Language MostWantedLanguage
        {
            get => _mostWantedLanguage;
            set
            {
                if (value == _mostWantedLanguage) return;
                _mostWantedLanguage = value;
                OnPropertyChanged(nameof(MostWantedLanguage));
            }
        }
        private Location _mostWantedLocation = new();
        public Location MostWantedLocation
        {
            get => _mostWantedLocation;
            set
            {
                if (value == _mostWantedLocation) return;
                _mostWantedLocation = value;
                OnPropertyChanged(nameof(MostWantedLocation));
            }
        }

        private ObservableCollection<Language> _mostWantedLanguages = new();
        public ObservableCollection<Language> MostWantedLanguages
        {
            get => _mostWantedLanguages;
            set
            {
                if (_mostWantedLanguages == value) return;
                _mostWantedLanguages = value;
                OnPropertyChanged(nameof(MostWantedLanguages));
            }
        }
        private ObservableCollection<Location> _mostWantedLocations = new();
        public ObservableCollection<Location> MostWantedLocations
        {
            get => _mostWantedLocations;
            set
            {
                if (value == _mostWantedLocations) return;
                _mostWantedLocations = value;
                OnPropertyChanged(nameof(MostWantedLocations));
            }
        }

        public CustomTourRequestsStatisticsViewModel()
        {
            _requestService = Injector.GetService<CustomTourRequestService>();

            RequestsLocations = new(_requestService.GetRequestsLocations());
            RequestsLanguages = new(_requestService.GetRequestsLanguages());
            MostWantedLocations = new(_requestService.GetMostWantedLocations());
            MostWantedLanguages = new(_requestService.GetMostWantedLanguages());

            CountLocationsCommand = new RelayCommand(CountLocationsExecute, CountLocationsCanExecute);
            CountLanguagesCommand = new RelayCommand(CountLanguagesExecute, CountLanguagesCanExecute);
            CountLocationsMonthlyCommand = new RelayCommand(CountLocationsMonthlyExecute, CountLocationsMonthlyCanExecute);
            CountLanguagesMonthlyCommand = new RelayCommand(CountLanguagesMonthlyExecute, CountLanguagesMonthlyCanExecute);
            CreateMostWantedCommand = new RelayCommand(CreateMostWantedExecute, CreateMostWantedCanExecute);
            ToggleVisibilityCommand = new RelayCommand(ToggleVisibilityExecute, ToggleVisibilityCanExecute);
        }

        #region Commands
        #region CountLocationsCommand
        public ICommand CountLocationsCommand { get; private set; }

        public bool CountLocationsCanExecute()
        {
            return RbLocationsIsChecked && RbAnnualyIsChecked;
        }
        public void CountLocationsExecute()
        {
            Series = new SeriesCollection()
            {
                new ColumnSeries
                {
                    Title = "Lokacije",
                    Values = new ChartValues<int>(_requestService.CountRequests(SelectedLocation).AsChartValues())
                }
            };
            TitleX = $"Broj zahteva na lokaciji {SelectedLocation}\nna nivou godina";
            Labels.Clear();
            Labels.AddRange(_requestService.GetRequestsYears().ConvertAll(x => x.ToString()));
        }
        #endregion
        #region CountLanguagesCommand
        public ICommand CountLanguagesCommand { get; private set; }

        public bool CountLanguagesCanExecute()
        {
            return RbLanguagesIsChecked && RbAnnualyIsChecked;
        }
        public void CountLanguagesExecute()
        {
            Series = new SeriesCollection
           {
               new ColumnSeries
               {
                   Title = "Jezici",
                   Values = new ChartValues<int>(_requestService.CountRequests(SelectedLanguage).AsChartValues())
               }
           };
            TitleX = $"Broj zahteva na jeziku {SelectedLanguage}\nna nivou godina";
            Labels.Clear();
            Labels.AddRange(_requestService.GetRequestsYears().ConvertAll(x => x.ToString()));
        }
        #endregion
        #region CountLocationsMonthlyCommand
        public ICommand CountLocationsMonthlyCommand { get; private set; }
        public bool CountLocationsMonthlyCanExecute()
        {
            return RbLocationsIsChecked && RbYearIsChecked && int.TryParse(DesiredYear, out _);
        }
        public void CountLocationsMonthlyExecute()
        {
            Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Lokacija u godini",
                    Values = new ChartValues<int>(_requestService.CountRequestsMonthly(SelectedLocation, int.Parse(DesiredYear)).AsChartValues())
                }
            };
            TitleX = $"Broj zahteva na lokaciji {SelectedLocation}\nna nivou meseci u {DesiredYear}.";
            Labels.Clear();
            Labels.AddRange(Months);
        }
        #endregion
        #region CountLanguagesMonthlyCommand
        public ICommand CountLanguagesMonthlyCommand { get; private set; }
        public bool CountLanguagesMonthlyCanExecute()
        {
            return RbLanguagesIsChecked && RbYearIsChecked && int.TryParse(DesiredYear, out _);
        }
        public void CountLanguagesMonthlyExecute()
        {
            Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Jezika u godini",
                    Values = new ChartValues<int>(_requestService.CountRequestsMonthly(SelectedLanguage, int.Parse(DesiredYear)).AsChartValues())
                }
            };
            TitleX = $"Broj zahteva na jeziku {SelectedLanguage}\nna nivou meseci u {DesiredYear}.";
            Labels.Clear();
            Labels.AddRange(Months);
        }
        #endregion
        #region MostWantedCommand
        public ICommand CreateMostWantedCommand { get; private set; }
        public bool CreateMostWantedCanExecute()
        {
            return MostWantedLocation.Id > 0 || MostWantedLanguage > 0;
        }
        public void CreateMostWantedExecute()
        {
            SendMessage();
        }
        public void SendMessage()
        {
            var message =  RbLocationsIsChecked ? new CreateMostWantedMessage(this, MostWantedLocation) : new CreateMostWantedMessage(this, MostWantedLanguage);
            MessageBus.Publish(message);
        }
        #endregion
        #region ToggleVisibilityCommand
        public ICommand ToggleVisibilityCommand { get; private set; }
        public bool ToggleVisibilityCanExecute()
        {
            return RbLanguagesIsChecked || RbLocationsIsChecked;
        }
        public void ToggleVisibilityExecute()
        {
            if(RbLocationsIsChecked)
            {
                SpVisibilityLocations = SpVisibilityLocations == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
            if(RbLanguagesIsChecked)
            {
                SpVisibilityLanguages = SpVisibilityLanguages == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        #endregion
        #endregion
    }
}
