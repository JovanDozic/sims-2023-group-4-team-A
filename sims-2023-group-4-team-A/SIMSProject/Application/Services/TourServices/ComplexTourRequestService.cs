using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System.Collections.Generic;

namespace SIMSProject.Application.Services.TourServices
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestRepo _complexTourRequestRepo;

        public ComplexTourRequestService(IComplexTourRequestRepo complexTourRequestRepo)
        {
            _complexTourRequestRepo = complexTourRequestRepo;
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
    }
}
