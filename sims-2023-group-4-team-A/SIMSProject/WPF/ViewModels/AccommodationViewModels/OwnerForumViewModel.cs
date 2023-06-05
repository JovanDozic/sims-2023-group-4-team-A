using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerForumViewModel
    {
        private User _user;
        private ForumService _forumService;

        public ObservableCollection<Forum> Forums { get; set; }

        public OwnerForumViewModel(User user)
        {
            _user = user;
            _forumService = Injector.GetService<ForumService>();

            Forums = new(_forumService.GetAll());
            
        }
    }
}
