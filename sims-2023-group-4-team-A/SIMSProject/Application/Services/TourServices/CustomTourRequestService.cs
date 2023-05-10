using Dynamitey.DynamicObjects;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System.Collections.Generic;

namespace SIMSProject.Application.Services.TourServices
{
    public class CustomTourRequestService
    {
        private readonly ICustomTourRequestRepo _customTourRequestRepo;

        public CustomTourRequestService(ICustomTourRequestRepo customTourRequestRepo)
        {
            _customTourRequestRepo = customTourRequestRepo;
        }
        public void Save(CustomTourRequest customTourRequest)
        {
            _customTourRequestRepo.Save(customTourRequest);
        }
        public List<CustomTourRequest> GetAllByGuestId(int guestId)
        {
            return _customTourRequestRepo.GetAllByGuestId(guestId);
        }

        public List<CustomTourRequest> GetAll()
        {
            return _customTourRequestRepo.GetAll();
        }

        public List<Location> GetRequestsLocations()
        {
            return _customTourRequestRepo.GetRequestsLocations();
        }

        public List<string> GetRequestsLanguages()
        {
            return _customTourRequestRepo.GetRequestsLanguages();
        }
    }
}
