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
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class ForumViewModel: ViewModelBase
    {
        private User _user = new();
        private Forum _forum = new();
        private ForumService _forumService;
        private LocationService _locationService;

        private ObservableCollection<Forum> _forums = new();
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

        public string Comment
        {
            get => _forum.Comment;
            set
            {
                if (_forum.Comment == value) return;
                _forum.Comment = value;
                OnPropertyChanged();
            }
        }

        public ForumViewModel(User user)
        {
            _user = user;
            _forumService = Injector.GetService<ForumService>();
            _locationService = Injector.GetService<LocationService>();
            AllLocations = new(_locationService.FindAll());
            CreationDate = DateTime.Today;
            Forums = LoadAllForumsByUser();
        }

        public ObservableCollection<Forum> LoadAllForumsByUser()
        {
            return new ObservableCollection<Forum>(_forumService.GetAllByUser(_user));
        }
        public void CreateForum()
        {
            _forum.ForumOwner.Id = _user.Id;
            _forumService.CreateForum(_forum);
            ToastNotificationService.ShowSuccess("Forum uspešno kreiran");
        }

        public bool IsLocationSelected()
        {
            return SelectedLocation != null;
        }

        public bool IsCommentEmpty()
        {
            return Comment != String.Empty;
        }
    }
}
