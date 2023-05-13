using Dynamitey.DynamicObjects;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
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

        public double AcceptedRequestPercentageByGuestId(int guestId, int year)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).FindAll(x=>x.RequestCreateDate.Year==year).Count;
            if (acceptedRequests == 0) return 0;
            return (acceptedRequests /= _customTourRequestRepo.GetAllByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Count)*100;
        }
        public double AllTimeAcceptedRequestPercentageByGuestId(int guestId)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).Count;
            if (acceptedRequests == 0) return 0;
            return (acceptedRequests /= _customTourRequestRepo.GetAllByGuestId(guestId).Count) * 100;
        }
        public double AverageGuestsInAcceptedRequests(int guestId, int year)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Count;
            double guestcount = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Sum(x => x.GuestCount);
            if(acceptedRequests == 0) return 0;
            return guestcount/acceptedRequests;
        }
        public double AllTimeAverageGuestsInAcceptedRequests(int guestId)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).Count;
            double guestcount = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).Sum(x => x.GuestCount);
            if (acceptedRequests == 0) return 0;
            return guestcount / acceptedRequests;
        }

        public void ApproveRequest(CustomTourRequest request)
        {
            CustomTourRequest old = _customTourRequestRepo.GetById(request.Id);
            old.RequestStatus = RequestStatus.ACCEPTED;
            _customTourRequestRepo.SaveAll(_customTourRequestRepo.GetAll());
        }

        public int CountRequests(Location location)
        {
            return _customTourRequestRepo.CountRequests(location);
        }

        public int CountRequests(Language language)
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
    }
}
