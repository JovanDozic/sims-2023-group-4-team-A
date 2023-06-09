using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.Guest1ViewModels
{
    public class ForumDisplayViewModel : ViewModelBase
    {
        private readonly User _user = new();
        private Forum _forum = new();
        private Comment _newComment = new();
        private ForumService _forumService;
        private CommentService _commentService;
        private ObservableCollection<Comment> _comments = new();
        public Forum Forum
        {
            get { return _forum; }
            set { _forum = value; }
        }

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

        public ForumDisplayViewModel(User user, Forum forum)
        {
            _forumService = Injector.GetService<ForumService>();
            _commentService = Injector.GetService<CommentService>();
            Forum = forum;
            Comments = new(Forum.Comments);
            _user = user;
            NewComment = new Comment
            {
                User = _user,
                CreationDate = DateTime.Today
            };
        }
        public void AddNewComment()
        {
            NewComment = _forumService.AddNewComment(Forum, new Comment(NewComment));
            Comments.Add(new Comment(NewComment));
            NewComment = new Comment
            {
                User = _user,
                CreationDate = DateTime.Today
            };
        }

        public void CloseForum()
        {
            _forumService.CloseForum(Forum);
        }

        public void CloseForumToast()
        {
            ToastNotificationService.ShowSuccess("Forum uspešno zatvoren");
        }

        public bool IsClosed()
        {
            return Forum.IsClosed;
        }
        public bool IsUseful()
        {
            return Forum.IsUseful;
        }

        public bool IsUserOwner()
        {
            return _user.Id == Forum.Comments.First().User.Id;
        }

        public void LeaveAComment()
        {
            _commentService.CreateComment(NewComment, Forum.Location);
        }
    }
}
