using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerForumViewModel : ViewModelBase
    {
        public User User;
        private ForumService _forumService;
        private AccommodationService _accommodationService;
        private Comment _newComment = new();

        private Forum _forum = new();
        public Forum Forum { get; set; } = new();
        public Comment Comment { get; set; } = new();
        public Comment NewComment
        {
            get => _newComment;
            set
            {
                if (_newComment == value) return;
                _newComment = value;
                OnPropertyChanged(nameof(NewComment));
            }
        }

        private ObservableCollection<Comment> _comments = new();
        public ObservableCollection<Comment> Comments
        {
            get => _comments;
            set
            {
                if (_comments == value) return;
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public bool CanUserLeaveComment
        {
            get => _accommodationService.UserHasAccommodationsInLocation(User, Forum.Location)
                    && !Forum.IsClosed;
        }

        private Comment? _hoveredComment;
        public Comment? HoveredComment
        {
            get { return _hoveredComment; }
            set
            {
                if (_hoveredComment == value) return;
                _hoveredComment = value;
                OnPropertyChanged(nameof(HoveredComment));
            }
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
            NewComment = _forumService.AddNewComment(Forum, new Comment(NewComment));
            Comments.Add(new Comment(NewComment));
            NewComment = new Comment
            {
                User = User,
                CreationDate = DateTime.Now
            };
            _forumService.CheckAndUpdateUsability();
        }

        public bool DownvoteComment()
        {
            HoveredComment = _forumService.DownvoteComment(Forum.Comments.Find(x => x.Id == HoveredComment?.Id) ?? throw new Exception("Hovered comment not found"));
            var index = Comments.IndexOf(Comments.ToList().Find(x => x.Id == HoveredComment?.Id) ?? throw new Exception("Hovered comment not found"));
            Forum.Comments[index] = HoveredComment;
            Comments = new(Forum.Comments);
            return HoveredComment.UserDownvoted;
        }
    }
}
