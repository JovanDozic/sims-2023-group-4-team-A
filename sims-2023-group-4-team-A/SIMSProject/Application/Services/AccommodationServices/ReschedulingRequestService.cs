﻿using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class ReschedulingRequestService
    {
        private readonly IReschedulingRequestRepo _requestRepo;
        private readonly IAccommodationReservationRepo _reservationRepo;

        public ReschedulingRequestService(IReschedulingRequestRepo requestRepo, IAccommodationReservationRepo reservationRepo)
        {
            _requestRepo = requestRepo;
            _reservationRepo = reservationRepo;
        }

        public List<ReschedulingRequest> GetAllByOwnerId(int ownerId)
        {
            return _requestRepo.GetAllByOwnerId(ownerId);
        }

        public List<ReschedulingRequest> GetAllByAccommodationId(int accommodationId)
        {
            return _requestRepo.GetAll().FindAll(x => x.Reservation.Accommodation.Id == accommodationId);
        }

        public List<ReschedulingRequest> GetAllOnWaitByAccommodationId(int accommodationId)
        {
            return _requestRepo.GetAll().FindAll(x => x.Reservation.Accommodation.Id == accommodationId && x.Status == ReschedulingRequestStatus.Waiting);
        }

        public List<ReschedulingRequest> GetAllByAccommodationId(int accommodationId, ReschedulingRequestStatus status)
        {
            return _requestRepo.GetAll().FindAll(x => x.Reservation.Accommodation.Id == accommodationId && 
                                                      x.Status == status && 
                                                      x.Reservation.StartDate > DateTime.Now);
        }

        public bool IsDateRangeAvailable(AccommodationReservation reservationToBeMoved, DateTime startDate, DateTime endDate)
        {
            foreach (var reservation in _reservationRepo.GetAllByAccommodationId(reservationToBeMoved.Accommodation.Id))
            {
                if (reservation.Id == reservationToBeMoved.Id) continue;
                if (startDate < reservation.EndDate && endDate > reservation.StartDate)
                    return false;
            }
            return true;
        }

        public void AcceptRequest(ReschedulingRequest request)
        {
            request.Status = ReschedulingRequestStatus.Accepted;
            request.Reservation.StartDate = request.NewStartDate;
            request.Reservation.EndDate = request.NewEndDate;
            request.OwnerComment = string.Empty;

            _requestRepo.Update(request);
            _reservationRepo.Update(request.Reservation);
        }

        public void RejectRequest(ReschedulingRequest request)
        {
            request.Status = ReschedulingRequestStatus.Rejected;

            _requestRepo.Update(request);
        }

        public void SaveRequest(ReschedulingRequest request)
        {
            _requestRepo.Save(request);
        }

        public bool CheckIfMatches(AccommodationReservation reservation)
        {
            return GetAllOnStandBy().Any(r => r.Id == reservation.Id);
        }
    
        public List<AccommodationReservation> GetAllOnStandBy()
        {
            return _requestRepo.GetAll().Where(req => req.Status == ReschedulingRequestStatus.Waiting).Select(req => req.Reservation).ToList();
        }

        public List<ReschedulingRequest> GetAllByGuestId(int id)
        {
            return _requestRepo.GetAllByGuestId(id);
        }
    }
}
