using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationStatisticsViewModel : ViewModelBase
    {
        private readonly AccommodationStatisticService _statService;
        private AccommodationStatistic _statistics = new();

        public ObservableCollection<AccommodationStatistic> Statistics { get; set; }

        public AccommodationStatisticsViewModel(Accommodation accommodation)
        {
            _statService = Injector.GetService<AccommodationStatisticService>();
            Statistics = new(_statService.GetAllStatistics(accommodation));
        }


    }
}
