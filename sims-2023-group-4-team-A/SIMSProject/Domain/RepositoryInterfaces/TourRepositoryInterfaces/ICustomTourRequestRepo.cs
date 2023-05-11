using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ICustomTourRequestRepo
    {
        public int NextId();
        public List<CustomTourRequest> GetAll();
        public List<CustomTourRequest> GetAllByGuestId(int guestId);
        public List<CustomTourRequest> GetAllAcceptedByGuestId(int guestId);
        public CustomTourRequest Save(CustomTourRequest customTourRequest);
        public void SaveAll(List<CustomTourRequest> customTourRequests);
        public List<Location> GetRequestsLocations();
        public List<Language> GetRequestsLanguages();
        public CustomTourRequest GetById(int id);
    }
}
