using LiveCharts;
using LiveCharts.Wpf;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.Guest1ViewModels
{
    public class ReservationsStatisticsViewModel : ViewModelBase
    {
        private readonly User _user = new();
        private readonly AccommodationReservationService _reservationService;
        private Dictionary<string, int> _monthlyReservations;
        private int _previousYearReservations;
        private int _currentYearReservations;
        private List<string> _months;
        private SeriesCollection _reservationsSeries;
        private ChartValues<int> _prevValue;
        private ChartValues<int> _currValue;

        public ChartValues<int> PreviousYearValue
        {
            get { return _prevValue; }
            set
            {
                _prevValue = value;
                OnPropertyChanged();
            }
        }
         public ChartValues<int> CurrentYearValue
        {
            get { return _currValue; }
            set
            {
                _currValue = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection ReservationsSeries
        {
            get { return _reservationsSeries; }
            set
            {
                _reservationsSeries = value;
                OnPropertyChanged("ReservationsSeries");
            }
        }
        public Dictionary<string, int> MonthlyReservations
        {
            get { return _monthlyReservations; }
            set
            {
                _monthlyReservations = value;
                OnPropertyChanged(nameof(MonthlyReservations));
                UpdateChart();
            }
        }
        public int PreviousYearReservations
        {
            get { return _previousYearReservations; }
            set
            {
                _previousYearReservations = value;
                OnPropertyChanged(nameof(PreviousYearReservations));
            }
        }
        public int CurrentYearReservations
        {
            get { return _currentYearReservations; }
            set
            {
                _currentYearReservations = value;
                OnPropertyChanged(nameof(CurrentYearReservations));
            }
        }       
        public List<string> Months
        {
            get { return _months; }
            set
            {
                _months = value;
                OnPropertyChanged();
            }
        }

        public ReservationsStatisticsViewModel(User user)
        {
            _user = user;
            _reservationService = Injector.GetService<AccommodationReservationService>();
            UpdateChart();
        }

        private void UpdateChart()
        {
            ReservationsSeries = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Broj rezervacija",
                        Values = new ChartValues<int>(_reservationService.CountReservationsByMonth(_user).Values.ToList())
                    }
                };

            Months = SetMonths(_reservationService.CountReservationsByMonth(_user).Keys.ToList());
            SetValues();
        }

        public List<string> SetMonths(List<string> numbersOfMonths)
        {

            List<string> monthNames = new List<string>();

            foreach (string monthNumber in numbersOfMonths.OrderBy(m => int.Parse(m)))
            {
                int monthIndex;
                if (int.TryParse(monthNumber, out monthIndex) && monthIndex >= 1 && monthIndex <= 12)
                {
                    string monthName = CultureInfo.GetCultureInfo("sr-Latn-RS").DateTimeFormat.GetMonthName(monthIndex);
                    monthNames.Add(monthName);
                }
            }
            return monthNames;
        }
        public void SetValues()
        {
            PreviousYearValue = new ChartValues<int>();
            CurrentYearValue = new ChartValues<int>();
            int value1 = _reservationService.CalculateReservationsCountPreviousYear(_user);
            int value2 = _reservationService.CalculateReservationsCountCurrentYear(_user);

            PreviousYearValue.Add(value1);
            CurrentYearValue.Add(value2);
        }
    }
}
