﻿using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerForumViewModel : ViewModelBase, IDataErrorInfo
    {
        public User User;
        private ForumService _forumService;
        private AccommodationService _accommodationService;
        private Comment _newComment = new();
        private ObservableCollection<Comment> _comments = new();
        private Comment? _hoveredComment;

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
                OnPropertyChanged(nameof(IsNewCommentValid));
                OnPropertyChanged(nameof(NewComment.Text));
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
        public bool CanUserLeaveComment
        {
            get => _accommodationService.UserHasAccommodationsInLocation(User, Forum.Location)
                    && !Forum.IsClosed;
        }

        public RelayCommand DownvoteCommentCommand { get; set; }


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

            DownvoteCommentCommand = new RelayCommand(DownvoteComment);
        }

        public bool AddNewComment()
        {
            if (!IsNewCommentValid) return false;
            NewComment = _forumService.AddNewComment(Forum, new Comment(NewComment));
            Comments.Add(new Comment(NewComment));
            NewComment = new Comment
            {
                User = User,
                CreationDate = DateTime.Now
            };
            _forumService.CheckAndUpdateUsability();
            return true;
        }

        public void DownvoteComment()
        {
            HoveredComment = _forumService.DownvoteComment(Forum.Comments.Find(x => x.Id == HoveredComment?.Id) ?? throw new Exception("Hovered comment not found"));
            var index = Comments.IndexOf(Comments.ToList().Find(x => x.Id == HoveredComment?.Id) ?? throw new Exception("Hovered comment not found"));
            Forum.Comments[index] = HoveredComment;
            Comments = new(Forum.Comments);
            OnPropertyChanged(nameof(HoveredComment.UserDownvotedIcon));
        }



        public string this[string columnName]
        {
            get
            {
                string? error = null;
                string requiredMessage = "Obavezno polje";
                switch (columnName)
                {
                    case nameof(NewComment.Text):
                        if (string.IsNullOrEmpty(NewComment.Text)) error = requiredMessage;
                        else if (NewComment.Text.Length < 3) error = "Komentar mora biti duzi od 10 karaktera";
                        else if (NewComment.Text.Contains("|")) error = "Komentar ne sme da sadrzi '|'";
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error => null;
        public bool IsNewCommentValid
        {
            get
            {
                foreach (var property in new string[] {
                    nameof(NewComment.Text) 
                })
                {
                    if (this[property] != null) return false;
                }
                return true;
            }
        }
    }
}
