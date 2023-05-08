﻿using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandlers.TourFileHandlers;
using SIMSProject.WPF.Views.Guest2Views;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.TourRepositories
{
    public class CustomTourRequestRepo : ICustomTourRequestRepo
    {
        public readonly CustomTourRequestFileHandler _fileHandler;
        private readonly IGuestRepo _guestRepo;
        private readonly ILocationRepo _locationRepo;
        private List<CustomTourRequest> _customTourRequests;

        public CustomTourRequestRepo(IGuestRepo guestRepo, ILocationRepo locationRepo)
        {
            _fileHandler = new CustomTourRequestFileHandler();
            _customTourRequests = _fileHandler.Load();
            _guestRepo = guestRepo;
            _locationRepo = locationRepo;
            MapGuests();
            MapLocations();
        }

        public List<CustomTourRequest> GetAll()
        {
            return _customTourRequests;
        }

        public List<CustomTourRequest> GetAllByGuestId(int guestId)
        {
            return _customTourRequests.FindAll(x => x.Guest.Id == guestId);
        }

        public int NextId()
        {
            return _customTourRequests.Count > 0 ? _customTourRequests.Max(x => x.Id) + 1 : 1; 
        }

        public CustomTourRequest Save(CustomTourRequest customTourRequest)
        {
            customTourRequest.Id = NextId();
            _customTourRequests.Add(customTourRequest);
            _fileHandler.Save(_customTourRequests);
            return customTourRequest;
        }

        public void SaveAll(List<CustomTourRequest> customTourRequests)
        {
            _fileHandler.Save(customTourRequests);
            _customTourRequests = customTourRequests;
        }

        private void MapGuests()
        {
            foreach(var customTourRequest  in _customTourRequests)
            {
                customTourRequest.Guest = _guestRepo.GetById(customTourRequest.Guest.Id);
            }
        }

        private void MapLocations()
        {
            foreach (var customTourRequest in _customTourRequests)
            {
                customTourRequest.Location = _locationRepo.GetById(customTourRequest.Location.Id);
            }
        }
    }
}
