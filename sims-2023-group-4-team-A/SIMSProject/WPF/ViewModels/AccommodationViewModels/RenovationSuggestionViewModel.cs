using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.Generic;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class RenovationSuggestionViewModel: ViewModelBase
    {
        private RenovationSuggestion _renovation = new();
        private List<string> _levels;
        private RenovationSuggestionService _renovationService;
        public RenovationSuggestion Renovation
        {
            get => _renovation;
            set
            {
                if (_renovation == value) return;
                _renovation = value;
                OnPropertyChanged();
            }
        }
        public string Comment
        {
            get => _renovation.Comment;
            set
            {
                if (_renovation.Comment == value) return;
                _renovation.Comment = value;
                OnPropertyChanged();
            }
        }
        public string Level
        {
            get => _renovation.LevelOfEmergency;
            set
            {
                if (_renovation.LevelOfEmergency == value) return;
                _renovation.LevelOfEmergency = value;
                OnPropertyChanged();
            }
        }
        public List<string> Levels
         {
            get => _levels;
            set
            {
                _levels = value;
                OnPropertyChanged();
            }       
         }

        public RenovationSuggestionViewModel()
        {
            
            _renovationService = Injector.GetService<RenovationSuggestionService>();
            Levels = new List<string>
            {
            "Nivo 1 - bilo bi lepo renovirati neke sitnice, ali sve funkcioniše kako treba i bez toga",
            "Nivo 2 - male zamerke na smeštaj koje kada bi se uklonile bi ga učinile savršenim",
            "Nivo 3 - nekoliko stvari koje su baš zasmetale bi trebalo renovirati",
            "Nivo 4 - ima dosta loših stvari i renoviranje je stvarno neophodno",
            "Nivo 5 - smeštaj je u jako lošem stanju i ne vredi ga uopšte iznajmljivati ukoliko se ne renovira"
        };
        }

        public void SendRequest()
        {
            _renovationService.SendRequest(_renovation);
        }
    }
}
