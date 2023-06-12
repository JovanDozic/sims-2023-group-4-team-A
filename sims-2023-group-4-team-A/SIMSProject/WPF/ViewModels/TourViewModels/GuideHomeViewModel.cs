using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIMSProject.Application.Services;
using System.Windows.Navigation;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class GuideHomeViewModel
    {
        private readonly VoucherService _voucherService;
        private readonly GuideService _service;
        public static NavigationService? NavigationService;
        public static Guide Guide { get; set; } = new();
        public string SuperGuideLanguages { get; private set; }
        public GuideHomeViewModel(Guide guide, NavigationService navigationService)
        {
            NavigationService = navigationService;
            _voucherService = Injector.GetService<VoucherService>();
            _service = Injector.GetService<GuideService>();
            Guide = guide;
            SuperGuideLanguages = _service.DisplaySuperGuideLanguages(guide);
            QuitCommand = new RelayCommand(QuitExecute, QuitCanExecute);
            GeneratePDFCommand = new RelayCommand(GenerateExecuted, GenerateCanExecute);
            NavigateTodaysCommand = new RelayCommand(NavigateTodaysExecute, NavigationCanExecute);
            NavigateAllCommand = new RelayCommand(NavigateAllExecute, NavigationCanExecute);
            NavigateCustomRequestsCommand = new RelayCommand(NavigateCustomRequestsExecute, NavigationCanExecute);
            NavigateComplexRequestsCommand = new RelayCommand(NavigateComplexRequestsExecute, NavigationCanExecute);
            NavigateStatisticsCommand = new RelayCommand(NavigateStatisticsExecute, NavigationCanExecute);
            NavigateRequestsStatisticsCommand = new RelayCommand(NavigateRequestsStatisticsExecute, NavigationCanExecute);
            NavigateReviewsCommand = new RelayCommand(NavigateReviewsExecute, NavigationCanExecute);
        }
        #region GeneratePDFReportCommand
        public ICommand GeneratePDFCommand { get; private set; }
        private bool GenerateCanExecute()
        {
            return true;
        }
        public void GenerateExecuted()
        {
            PDFService.GenerateCustomRequestsPDF();
        }
        #endregion
        #region Navigation
        private bool NavigationCanExecute()
        {
            return true;
        }
        #region NavigateTodaysCommand
        public ICommand NavigateTodaysCommand { get; private set; }
        
        public void NavigateTodaysExecute()
        {
            NavigationService.Navigate(
                new Uri("/WPF/Views/TourViews/GuideViews/TourManager/TodaysToursPage.xaml", UriKind.Relative));
        }
        #endregion
        #region NavigateAllCommand
        public ICommand NavigateAllCommand { get; private set; }

        public void NavigateAllExecute()
        {
            NavigationService.Navigate(
                new Uri("/WPF/Views/TourViews/GuideViews/TourManager/AllToursPage.xaml", UriKind.Relative));
        }
        #endregion
        #region NavigateCustomRequestsCommand
        public ICommand NavigateCustomRequestsCommand { get; private set; }

        public void NavigateCustomRequestsExecute()
        {
            NavigationService.Navigate(
                new Uri("/WPF/Views/TourViews/GuideViews/CustomTourRequests/CustomRequestsPage.xaml", UriKind.Relative));
        }
        #endregion
        #region NavigateComplexRequestsCommand
        public ICommand NavigateComplexRequestsCommand { get; private set; }

        public void NavigateComplexRequestsExecute()
        {
            NavigationService.Navigate(
                new Uri("/WPF/Views/TourViews/GuideViews/ComplexTourRequests/ComplexTourRequestsPage.xaml", UriKind.Relative));
        }
        #endregion
        #region NavigateStatisticsCommand
        public ICommand NavigateStatisticsCommand { get; private set; }

        public void NavigateStatisticsExecute()
        {
            NavigationService.Navigate(
                new Uri("/WPF/Views/TourViews/GuideViews/TourStatistics/TourStatisticsPage.xaml", UriKind.Relative));
        }
        #endregion
        #region NavigateRequestsStatisticsCommand
        public ICommand NavigateRequestsStatisticsCommand { get; private set; }

        public void NavigateRequestsStatisticsExecute()
        {
            NavigationService.Navigate(
                new Uri("/WPF/Views/TourViews/GuideViews/CustomTourRequests/CustomRequestsStatisticsPage.xaml", UriKind.Relative));
        }
        #endregion
        #region NavigateReviewsCommand
        public ICommand NavigateReviewsCommand { get; private set; }

        public void NavigateReviewsExecute()
        {
            NavigationService.Navigate(
                new Uri("/WPF/Views/TourViews/GuideViews/TourReviews/TourReviewsPage.xaml", UriKind.Relative));
        }
        #endregion
        #endregion
        #region QuitCommand
        public ICommand QuitCommand { get; private set; }
        public bool QuitCanExecute()
        {
            return true;
        }
        public void QuitExecute()
        {
            _voucherService.GiveVouchersForQuitting(Guide, ObtainingReason.GUIDEQUIT);
            _service.Quit(Guide.Id);
        }
        #endregion
    }
}
