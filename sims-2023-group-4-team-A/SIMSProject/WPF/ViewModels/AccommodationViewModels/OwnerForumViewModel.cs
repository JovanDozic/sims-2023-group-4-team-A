using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerForumViewModel
    {
        public User User;
        private ForumService _forumService;
        private AccommodationService _accommodationService;

        public Forum Forum { get; set; } = new();
        public Comment Comment { get; set; } = new();
        public Comment NewComment { get; set; }
        public ObservableCollection<Comment> Comments { get; set; }

        public bool CanUserLeaveComment
        {
            get => _accommodationService.UserHasAccommodationsInLocation(User, Forum.Location) 
                    && !Forum.IsClosed;
        }

        public OwnerForumViewModel(User user, Forum forum)
        {
            User = user;
            Forum = forum;
            _forumService = Injector.GetService<ForumService>();
            _accommodationService = Injector.GetService<AccommodationService>();

            Comments = new(Forum.Comments);

            NewComment = new Comment 
            {
                User = User,
                CreationDate = DateTime.Now
            };
        }

        public void AddNewComment()
        {
            NewComment = _forumService.AddNewComment(Forum, NewComment);
            Comments.Add(NewComment);
        }
    }
}
