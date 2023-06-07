using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerLocationForumsViewModel
    {
        private ForumService _forumService;
        public Location Location { get; set; }
        public Forum Forum { get; set; } = new();

        public ObservableCollection<Forum> Forums { get; set; }

        public OwnerLocationForumsViewModel(User user, Location location)
        {
            Location = location;
            _forumService = Injector.GetService<ForumService>();

            Forums = new(_forumService.GetAllByLocation(Location));
        }
    }
}
