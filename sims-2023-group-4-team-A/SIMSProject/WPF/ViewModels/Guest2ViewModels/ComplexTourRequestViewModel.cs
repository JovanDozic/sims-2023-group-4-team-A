using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class ComplexTourRequestViewModel : ViewModelBase
    {
        #region Polja
        private Guest2 _user;
        private CustomTourRequestService _customTourRequestService;
        private ObservableCollection<CustomTourRequest> _tourRequestParts = new();
        public ObservableCollection<CustomTourRequest> TourRequestParts
        {
            get => _tourRequestParts;
            set
            {
                if (value == _tourRequestParts) return;
                _tourRequestParts = value;
                OnPropertyChanged();
            }
        }
        private ComplexTourRequest _selectedComplexTourRequest = new();
        public ComplexTourRequest SelectedComplexTourRequest
        {
            get => _selectedComplexTourRequest;
            set
            {
                if (value == _selectedComplexTourRequest) return;
                _selectedComplexTourRequest = value;
                OnPropertyChanged(nameof(SelectedComplexTourRequest));
            }
        }
        public NavigationService NavService { get; set; }
        public RelayCommand GoBack { get; set; }
#endregion

        #region Konstruktori
        public ComplexTourRequestViewModel(Guest2 user, NavigationService navigationService, ComplexTourRequest complexTourRequest = null)
        {
            _user = user;
            NavService = navigationService;
            _customTourRequestService = Injector.GetService<CustomTourRequestService>();
            SelectedComplexTourRequest = complexTourRequest;
            GetParts();

            GoBack = new RelayCommand(GoBackExecute, CanExecute_Command);
        }
        #endregion

        #region Akcije
        private void GoBackExecute()
        {
            NavService.GoBack();
        }
        private bool CanExecute_Command()
        {
            return true;
        }
        public void GetParts()
        {
            TourRequestParts =new ObservableCollection<CustomTourRequest>(_customTourRequestService.GetAllComplexTourParts(SelectedComplexTourRequest.Id));
        }
        #endregion
    }
}
