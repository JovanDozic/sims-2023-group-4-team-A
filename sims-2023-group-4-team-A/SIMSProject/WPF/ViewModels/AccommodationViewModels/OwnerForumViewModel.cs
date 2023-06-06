﻿using SIMSProject.Application.Services.AccommodationServices;
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

        public Forum Forum { get; set; } = new();

        public ObservableCollection<Comment> Comments { get; set; }

        public OwnerForumViewModel(User user, Forum forum)
        {
            _user = user;
            Forum = forum;
            _forumService = Injector.GetService<ForumService>();

            Comments = new(Forum.Comments);
        }
    }
}
