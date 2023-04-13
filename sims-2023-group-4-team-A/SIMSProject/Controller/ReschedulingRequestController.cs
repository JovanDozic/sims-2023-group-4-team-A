using SIMSProject.Model;
using SIMSProject.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Controller
{
    public class ReschedulingRequestController
    {
        private readonly ReschedulingRequestDAO _reschedulingRequestDAO;
        public ReschedulingRequest ReschedulingRequest { get; set; }

        public ReschedulingRequestController()
        {
            _reschedulingRequestDAO = new();
            ReschedulingRequest = new();
        }

        public List<ReschedulingRequest> GetAll()
        {
            return _reschedulingRequestDAO.GetAll();
        }

        public List<ReschedulingRequest> GetAllOnWaitByOwnerId(int ownerId)
        {
            return _reschedulingRequestDAO.GetAll()
                .FindAll(x => x.Reservation.Accommodation.Owner.Id == ownerId /*&&
                              x.Status == "Na čekanju"*/);
        }

        public List<ReschedulingRequest> GetAllByOwnerId(int ownerId)
        {
            return GetAll().FindAll(x => x.Reservation.Accommodation.Owner.Id == ownerId);
        }

        public void SaveAll(List<ReschedulingRequest> requests)
        {
            _reschedulingRequestDAO.SaveAll(requests);
        }

        public ReschedulingRequest Create(ReschedulingRequest request)
        {
            return _reschedulingRequestDAO.Save(request);
        }
        
        public void Update(ReschedulingRequest updatedRequest)
        {
            var requests = _reschedulingRequestDAO.GetAll();
            int index = requests.FindIndex(x => x.Id == updatedRequest.Id);
            if (index != -1)
            {
                requests[index] = updatedRequest;
            }
            SaveAll(requests);
        }
    }
}
