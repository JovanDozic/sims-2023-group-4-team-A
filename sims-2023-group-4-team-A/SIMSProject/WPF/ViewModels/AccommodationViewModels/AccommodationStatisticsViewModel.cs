using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationStatisticsViewModel : ViewModelBase
    {
        private readonly AccommodationStatisticService _statService;
        private AccommodationStatistic _statistic = new();

        public Accommodation Accommodation { get; set; }
        public ObservableCollection<AccommodationStatistic> Statistics { get; set; } = new();
        public AccommodationStatistic Statistic
        {
            get => _statistic;
            set
            {
                if (value == _statistic) return;
                _statistic = value;
                OnPropertyChanged();
            }
        }

        public AccommodationStatisticsViewModel(Accommodation accommodation, AccommodationStatistic? statistic = null)
        {
            _statService = Injector.GetService<AccommodationStatisticService>();
            Accommodation = accommodation;
            if (statistic is not null) Statistic = statistic;
        }

        public void LoadYearlyStatistics()
        {
            Statistics = new(_statService.GetAllYearlyStatistics(Accommodation));
        }

        public void LoadMonthlyStatistics()
        {
            Statistics = new(_statService.GetAllMonthlyStatistics(Accommodation, Statistic.Year));

            _statService.CalculateOccupancyPercentage(Accommodation, 2023, 1);
            _statService.CalculateOccupancyPercentage(Accommodation, 2022);
        }

        public void GeneratePDF()
        {
            PDFService.GenerateAccommodationStatsPDF(Statistics.ToList());
        }
    }
}
