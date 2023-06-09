using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface IComplexTourRequestRepo
    {
        public int NextId();
        public List<ComplexTourRequest> GetAll();
        public List<ComplexTourRequest> GetAllByGuestId(int guestId);
        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest);
        public void SaveAll(List<ComplexTourRequest> complexTourRequests);
    }
}
