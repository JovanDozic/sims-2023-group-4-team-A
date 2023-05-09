using Dynamitey.DynamicObjects;
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
        public double AverageGuestsInAcceptedRequests(int guestId, int year)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Count;
            double guestcount = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Sum(x => x.GuestCount);
            if(acceptedRequests == 0) return 0;
            return guestcount/acceptedRequests;
        }
    }
}
