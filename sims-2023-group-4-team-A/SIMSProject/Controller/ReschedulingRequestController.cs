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

        public List<ReschedulingRequest> GetAllByOwnerId(int ownerId)
        {
            return GetAll().FindAll(x => x.AccommodationReservation.Accommodation.Owner.Id == ownerId);
        }

        public void SaveAll(List<ReschedulingRequest> requests)
        {
            _reschedulingRequestDAO.SaveAll(requests);
        }

        public ReschedulingRequest Create(ReschedulingRequest request)
        {
            return _reschedulingRequestDAO.Save(request);
        }

        public void UpdateExisting(ReschedulingRequest updatedRequest)
        {
            // TODO: implement: something like this:
            //// Dohvati objekt klase Request prema ID-u
            //Request requestToUpdate = _requestDAO.GetAll().FirstOrDefault(r => r.Id == requestId);

            //// Provjeri postoji li objekt
            //if (requestToUpdate == null)
            //{
            //    throw new ArgumentException("Ne postoji Request s ID-om " + requestId);
            //}

            //// Ažuriraj svojstvo objekta
            //PropertyInfo propertyToUpdate = requestToUpdate.GetType().GetProperty(fieldName);
            //if (propertyToUpdate != null && propertyToUpdate.CanWrite)
            //{
            //    propertyToUpdate.SetValue(requestToUpdate, newValue, null);
            //}
            //else
            //{
            //    throw new ArgumentException("Ne postoji svojstvo " + fieldName + " u klasi Request ili nije moguće ažurirati.");
            //}

            //// Spremi sve promjene
            //_requestDAO.SaveAll();

            // There is no need to track index. You can GetAll(), than find one you need and edit it inside of the list, and than SaveAll() where you can forward whole list.

        }


    }
}
