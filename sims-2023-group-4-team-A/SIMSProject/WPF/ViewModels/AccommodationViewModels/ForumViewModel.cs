using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class ForumViewModel: ViewModelBase
    {
        private User _user = new();
        private Forum _forum = new();
        private ForumService _forumService;
        private LocationService _locationService;
        private Comment _newComment = new();
        private Forum _selectedForum = new();

        private ObservableCollection<Forum> _forums = new();
        private ObservableCollection<Forum> _allForums = new();
        public ObservableCollection<Location> AllLocations { get; set; } = new();
        public ObservableCollection<Forum> Forums
        {
            get => _forums;
            set
            {
                if (value == _forums) return;
                _forums = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Forum> AllForums
        {
            get => _allForums;
            set
            {
                if (value == _allForums) return;
                _allForums = value;
                OnPropertyChanged();
            }
        }

        public Forum Forum
        {
            get => _forum;
            set
            {
                if (_forum == value) return;
                _forum = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreationDate
        {
            get => _forum.CreationDate;
            set
            {
                if (_forum.CreationDate == value) return;
                _forum.CreationDate = value;
                OnPropertyChanged();
            }
        }
        public Location SelectedLocation
        {
            get => _forum.Location;
            set
            {
                if (_forum.Location == value) return;
                _forum.Location = value;
                OnPropertyChanged();
            }
        }
        public Forum SelectedForum
        {
            get => _selectedForum;
            set
            {
                if (_selectedForum == value) return;
                _selectedForum = value;
                OnPropertyChanged();
            }
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

        public ForumViewModel(User user)
        {
            _user = user;
            _forumService = Injector.GetService<ForumService>();
            _locationService = Injector.GetService<LocationService>();
            AllLocations = new(_locationService.GetAll());
            CreationDate = DateTime.Today;
            Forums = LoadAllForumsByUser();
            AllForums = LoadAllForums();

        }

        public ObservableCollection<Forum> LoadAllForumsByUser()
        {
            return new ObservableCollection<Forum>(_forumService.GetAllByUser(_user));
        }
        public ObservableCollection<Forum> LoadAllForums()
        {
            SelectedLocation = null;
            return new ObservableCollection<Forum>(_forumService.GetAll());
        }
        public void CreateForum()
        {
            NewComment.User = _user;
            _forumService.CreateForum(SelectedLocation, NewComment);
            ToastNotificationService.ShowSuccess("Forum uspešno kreiran");
        }

        public bool IsLocationSelected()
        {
            return SelectedLocation != null;
        }

        public bool IsCommentEmpty()
        {
            return NewComment.Text != string.Empty;
        }

        public void FilterForumsByLocation()
        {
            if (SelectedLocation == null)
            {
                AllForums = new ObservableCollection<Forum>(_forumService.GetAll());
            }
            else
            {
                AllForums = new ObservableCollection<Forum>(_forumService.GetAllByLocation(SelectedLocation));
            }
        }
    }
}
