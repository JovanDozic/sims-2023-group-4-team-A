﻿using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerForumLocationsViewModel
    {
        private User _user;
        private LocationService _locationService;
        private ForumService _forumService;

        public ObservableCollection<Location> Locations { get; set; }
        public Location Location { get; set; } = new();

        public OwnerForumLocationsViewModel(User user)
        {
            _user = user;
            _locationService = Injector.GetService<LocationService>();
            _forumService = Injector.GetService<ForumService>();

            Locations = new(_forumService.GetAllLocations());
        }
    }
}
