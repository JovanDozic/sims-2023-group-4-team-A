using SIMSProject.Model;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IReschedulingRequestRepo
    {
        public void Load();
        public ReschedulingRequest GetById(int requestId);
        public List<ReschedulingRequest> GetAllByOwnerId(int ownerId);
        public List<ReschedulingRequest> GetAll();
        public int NextId();
        public ReschedulingRequest Save(ReschedulingRequest request);
        public void SaveAll(List<ReschedulingRequest> requests);
        public void Update(ReschedulingRequest request);
    }
}
