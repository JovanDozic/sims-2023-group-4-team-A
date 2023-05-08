using Dynamitey.DynamicObjects;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System;
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
        //    public void MakeGuestPresent(TourGuest tourGuest)
        //{
        //    tourGuest.JoiningPoint = tourGuest.TourAppointment.CurrentKeyPoint;
        //    tourGuest.GuestStatus = GuestAttendance.PRESENT;
        //    _repo.SaveAll(_repo.GetAll());
        //}
    }
}
