using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Application.Services.TourServices
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestRepo _complexTourRequestRepo;
        private readonly ICustomTourRequestRepo _customTourRequestRepo;

        public ComplexTourRequestService(IComplexTourRequestRepo complexTourRequestRepo, ICustomTourRequestRepo customTourRequestRepo)
        {
            _complexTourRequestRepo = complexTourRequestRepo;
            _customTourRequestRepo = customTourRequestRepo;
        }

        public void Save(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequestRepo.Save(complexTourRequest);
        }

        public List<ComplexTourRequest> GetAllByGuestId(int guestId)
        {
            return _complexTourRequestRepo.GetAllByGuestId(guestId);
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequestRepo.GetAll();
        }

        public int NextId()
        {
            return _complexTourRequestRepo.NextId();
        }
        public void CheckComplexRequestValidity(List<ComplexTourRequest> complexTourRequests, int guestId)
        {
            foreach (var complexRequest in complexTourRequests)
            {
                List<CustomTourRequest> complexRequestParts = _customTourRequestRepo.GetAllComplexTourPartsByGuestId(guestId).FindAll(x => x.ComplexTourId == complexRequest.Id);
                foreach (var complexRequestPart in complexRequestParts)
                {
                    if ((complexRequestPart.StartDate - DateTime.Now).TotalHours <= 48 && (complexRequestPart.RequestStatus == RequestStatus.ONHOLD))
                    {
                        complexRequestPart.RequestStatus = RequestStatus.INVALID;
                        complexRequest.Status = RequestStatus.INVALID;
                    }
                }
            }
            _customTourRequestRepo.SaveAll(_customTourRequestRepo.GetAll());
            _complexTourRequestRepo.SaveAll(_complexTourRequestRepo.GetAll());
        }

        public void CheckComplexRequestAcceptance(List<ComplexTourRequest> complexTourRequests, int guestId)
        {
            foreach (var complexRequest in complexTourRequests)
            {
                List<CustomTourRequest> complexRequestParts = _customTourRequestRepo.GetAllComplexTourPartsByGuestId(guestId).FindAll(x => x.ComplexTourId == complexRequest.Id);
                if(complexRequestParts.FindAll(x => x.RequestStatus == RequestStatus.ACCEPTED).Count() == complexRequestParts.Count())
                {
                    complexRequest.Status = RequestStatus.ACCEPTED;
                }
            }
            _complexTourRequestRepo.SaveAll(_complexTourRequestRepo.GetAll());
        }

    }
}
