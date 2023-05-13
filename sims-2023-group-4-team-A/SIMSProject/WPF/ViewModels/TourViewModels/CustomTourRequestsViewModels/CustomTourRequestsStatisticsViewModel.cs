using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels
{
    public class CustomTourRequestsStatisticsViewModel : ViewModelBase
    {
        private readonly CustomTourRequestService _requestService;

        public ObservableCollection<Location> RequestsLocations { get; set; }
        public ObservableCollection<Language> RequestsLanguages { get; set; }

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
                if(value != _language)
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
                if(value !=  _rbLocationsIsChecked)
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
                if(value != _rbAnnualyIsChecked)
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
                if( value != _rbYearIsChecked)
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
                if(_cbLocationsIsEnabled != value)
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
                if(_cbLanguagesIsEnabled != value)
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
                if(_tbYearIsEnabled != value)
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
        public CustomTourRequestsStatisticsViewModel()
        {
            _requestService = Injector.GetService<CustomTourRequestService>();

            RequestsLocations = new(_requestService.GetRequestsLocations());
            RequestsLanguages = new(_requestService.GetRequestsLanguages());

            CountLocationsCommand = new RelayCommand(CountLocationsExecute, CountLocationsCanExecute);
            CountLanguagesCommand = new RelayCommand(CountLanguagesExecute, CountLanguagesCanExecute);
            CountLocationsMonthlyCommand = new RelayCommand(CountLocationsMonthlyExecute, CountLocationsMonthlyCanExecute);
            CountLanguagesMonthlyCommand = new RelayCommand(CountLanguagesMonthlyExecute, CountLanguagesMonthlyCanExecute);
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
            _requestService.CountRequests(SelectedLocation);
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
            _requestService.CountRequests(SelectedLanguage);
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
            _requestService.CountRequestsMonthly(SelectedLocation, int.Parse(DesiredYear));
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
            _requestService.CountRequestsMonthly(SelectedLanguage, int.Parse(DesiredYear));
        }
        #endregion

        #endregion



    }
}
