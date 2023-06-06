using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerForumViewModel
    {
        private User _user;
        private ForumService _forumService;
        private AccommodationService _accommodationService;

        public Forum Forum { get; set; } = new();
        public Comment Comment { get; set; } = new();
        public ObservableCollection<Comment> Comments { get; set; }

        public bool CanUserLeaveComment
        {
            get => _accommodationService.UserHasAccommodationsInLocation(_user, Forum.Location) 
                    && !Forum.IsClosed;
        }

        public OwnerForumViewModel(User user, Forum forum)
        {
            _user = user;
            Forum = forum;
            _forumService = Injector.GetService<ForumService>();
            _accommodationService = Injector.GetService<AccommodationService>();

            Comments = new(Forum.Comments);
        }
    }
}
