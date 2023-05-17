using Dynamitey.DynamicObjects;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Language> GetRequestsLanguages()
        {
            return _customTourRequestRepo.GetRequestsLanguages();
        }

        public List<CustomTourRequest> FilterRequests(Location location, Language language, int numOfGuests, DateTime start, DateTime end)
        {
            List<CustomTourRequest> requests = new(_customTourRequestRepo.GetAll());
            if (location.Id != 0)
            {
                requests.RemoveAll(x => x.Location.Id != location.Id);
            }
            if (language > 0)
            {
                requests.RemoveAll(x => !x.TourLanguage.Equals(language));
            }
            if (numOfGuests > 0)
            {
                requests.RemoveAll(x => x.GuestCount != numOfGuests);
            }
            if(DateTime.Compare(start, end) != 0)
            {
                requests.RemoveAll(x => DateTime.Compare(x.StartDate, start) < 0 || DateTime.Compare(x.EndDate, end) > 0);
            }
            return requests;
        }
        public void CheckRequestValidity(List<CustomTourRequest> customTourRequests)
        {
            foreach (var customRequest in customTourRequests)
            {
                if((customRequest.StartDate-DateTime.Now).TotalHours <= 48)
                {
                    customRequest.RequestStatus = RequestStatus.INVALID;
                }
            }
            _customTourRequestRepo.SaveAll(_customTourRequestRepo.GetAll());
        }

        public void ApproveRequest(CustomTourRequest request)
        {
            CustomTourRequest old = _customTourRequestRepo.GetById(request.Id);
            old.RequestStatus = RequestStatus.ACCEPTED;
            _customTourRequestRepo.SaveAll(_customTourRequestRepo.GetAll());
        }

        public List<int> CountRequests(Location location)
        {
            return _customTourRequestRepo.CountRequests(location);
        }

        public List<int> CountRequests(Language language)
        {
            return _customTourRequestRepo.CountRequests(language);
        }
        public List<int> CountRequestsMonthly(Location location, int desiredYear)
        {
            return _customTourRequestRepo.CountRequestsMonthly(location, desiredYear);
        }
        public List<int> CountRequestsMonthly(Language language, int desiredYear)
        {
            return _customTourRequestRepo.CountRequestsMonthly(language, desiredYear);
        }

        public List<int> GetRequestsYears()
        {
            return _customTourRequestRepo.GetAll().Select(x => x.StartDate.Year).Distinct().OrderBy(x => x).ToList();
        }

        public List<Location> GetMostWantedLocations()
        {
            return _customTourRequestRepo.GetMostWantedLocations();
        }

        public List<Language> GetMostWantedLanguages()
        {
            return _customTourRequestRepo.GetMostWantedLanguages();
        }

        public List<Guest> GetGuestsWithSimilarRequests(Tour tour)
        {
            return _customTourRequestRepo.GetAllSimilarRequests(tour).Select(x => x.Guest).Distinct().ToList();
        }
        
    }
}
